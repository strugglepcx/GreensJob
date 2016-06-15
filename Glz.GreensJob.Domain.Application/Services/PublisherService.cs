using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Apworks.Repositories;
using Apworks.Specifications;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class PublisherService : ApplicationService, IPublisherService
    {
        private readonly IRepository<int, Publisher> _publisherRepository;
        private readonly ICache _cache;
        public PublisherService(IRepositoryContext context, IRepository<int, Publisher> PublisherRepository, ICache cache)
            : base(context)
        {
            _publisherRepository = PublisherRepository;
            _cache = cache;
        }

        public int AddObject(PublisherObject obj)
        {
            var publisher = new Publisher
            {
                companyID = obj.companyID,
                name = obj.name,
                mobile = obj.mobile,
                isAdmin = obj.isAdmin,
                password = string.IsNullOrEmpty(obj.password) ? "123456" : obj.password,
                lastLoginDate = Convert.ToDateTime("1990-01-01 00:00:00"),
                createDate = DateTime.Now
            };
            if (obj.PublisherRight != null)
            {
                publisher.PublisherRight = Mapper.Instance.Map<PublisherRight>(obj.PublisherRight);
            }
            _publisherRepository.Add(publisher);
            Context.Commit();
            return publisher.ID;
        }

        public List<PublisherObject> GetUserInof(int userId)
        {
            try
            {
                var result = new List<PublisherObject>();
                var publisher = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == userId));
                if (publisher.isAdmin)
                {
                    var list = _publisherRepository.FindAll(Specification<Publisher>.Eval(x => x.companyID == publisher.companyID && !x.isAdmin)).ToList();
                    result.AddRange(AutoMapper.Mapper.Map<List<PublisherObject>>(list));
                }
                result.Add(AutoMapper.Mapper.Map<PublisherObject>(publisher));
                return result;
            }
            catch (Exception)
            {
                _publisherRepository.Context.Rollback();
                return null;
            }
        }

        public WebUserInfoModel LoginAction(LoginActionRequestParam requestParam)
        {
            var obj = GetObjectByMobile(requestParam.userMobileNumber);
            if (obj == null)
            {
                throw new GreensJobException(0, "用户名或密码错误！");
            }
            if (!obj.password.Equals(DES.MD5Encode(requestParam.password)))
            {
                throw new GreensJobException(0, "密码错误");
            }
            // TODO: 记录登陆信息

            // 记录缓存
            var sessionId = Guid.NewGuid().ToString();
            var cacheKey = Const.UserSessionCodeCacheKey + obj.id;
            var userInfo = new WebUserInfoModel { id = obj.id, CompanyID = obj.companyID, saveLogin = requestParam.saveLogin, sessionId = sessionId };

            _cache.Set(cacheKey, userInfo, TimeSpan.FromHours(Const.UserSessionsLidingExpireTime));

            return userInfo;
        }

        public PublisherObject GetObjectByID(int id)
        {
            try
            {
                var publisher = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == id));
                var publisherobject = AutoMapper.Mapper.Map<PublisherObject>(publisher);
                return publisherobject;
            }
            catch
            {
                return null;
            }
        }

        public PublisherObject GetObjectByMobile(string mobile)
        {
            try
            {
                if (_publisherRepository.Exists(Specification<Publisher>.Eval(x => x.mobile == mobile)))
                {
                    var publisher = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.mobile == mobile));
                    var publisherobject = AutoMapper.Mapper.Map<PublisherObject>(publisher);
                    return publisherobject;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                var model = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == id));
                if (model == null)
                    return 0;
                _publisherRepository.Remove(model);
                _publisherRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _publisherRepository.Context.Rollback();
            }
            return 0;
        }

        public int UpdateObject(PublisherObject obj)
        {
            try
            {

                var model = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == obj.id));
                if (model == null)
                {
                    return 0;
                }
                model.password = obj.password;
                model.name = obj.name;
                model.mobile = obj.mobile;
                model.isAdmin = obj.isAdmin;
                model.PublisherRight.AddUserRight = obj.PublisherRight.AddUser;
                model.PublisherRight.DeleteUserRight = obj.PublisherRight.DeleteUser;
                model.PublisherRight.FinicialRight = obj.PublisherRight.Finicial;
                model.PublisherRight.ImportEmployeeRight = obj.PublisherRight.ImportEmployee;
                model.PublisherRight.ModifyUserRight = obj.PublisherRight.ModifyUser;
                model.PublisherRight.ReleaseJobRight = obj.PublisherRight.ReleaseJob;
                _publisherRepository.Update(model);
                _publisherRepository.Context.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                _publisherRepository.Context.Rollback();
                return 0;
            }
            return 0;
        }

        public bool IsAdmin(int id)
        {
            try
            {
                return _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == id)).isAdmin;
            }
            catch
            {
                return false;
            }
        }
    }
}
