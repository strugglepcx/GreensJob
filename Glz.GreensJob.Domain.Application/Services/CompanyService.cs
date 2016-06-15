using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using AutoMapper.Internal;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.Domain.Application.Services
{
    /// <summary>
    /// 公司服务
    /// </summary>
    public class CompanyService : ApplicationService, ICompanyService
    {
        private readonly IRepository<int, Company> _companyRepository;
        private readonly IRepository<int, City> _cityeRepository;
        private readonly IRepository<int, Publisher> _publisherRepository;

        /// <summary>
        /// 创建一个 <code>CompanyService</code> 类型实例
        /// </summary>
        /// <param name="context"></param>
        /// <param name="companyRepository"></param>
        /// <param name="cityeRepository"></param>
        /// <param name="publisherRepository"></param>
        public CompanyService(IRepositoryContext context, IRepository<int, Company> companyRepository,
            IRepository<int, City> cityeRepository, IRepository<int, Publisher> publisherRepository) : base(context)
        {
            _companyRepository = companyRepository;
            _cityeRepository = cityeRepository;
            _publisherRepository = publisherRepository;
        }

        public int PutCompany(CompanyActionRequestParam requestParam)
        {
            try
            {
                if (requestParam.userId > 0)
                {
                    var publisher = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == requestParam.userId));
                    if (publisher.isAdmin)
                    {
                        var company = new Company()
                        {
                            //ID = requestParam.companyId,//统一设置为6
                            ID = requestParam.companyId == 0 ? 6 : requestParam.companyId,
                            cityID = requestParam.cityID,
                            name = requestParam.companyName ?? "",
                            image = requestParam.companyImage ?? "",
                            addr = requestParam.companyAddr ?? "",
                            introduce = requestParam.companyIntroduce ?? "",
                            contact = requestParam.companyContact ?? "",
                            tel = requestParam.companyTel ?? "",
                            certificates = requestParam.certificate ?? "",
                            license = requestParam.license ?? "",
                            status = requestParam.status
                        };
                        if (company.ID > 0)
                        {
                            _companyRepository.Update(company);
                            _companyRepository.Context.Commit();
                            return 1;
                        }
                        else
                        {
                            _companyRepository.Add(company);
                            // 更新管理员用户的所属企业
                            _companyRepository.Context.Commit();

                            publisher.companyID = company.ID;
                            _publisherRepository.Update(publisher);
                            _publisherRepository.Context.Commit();

                            return company.ID;
                        }
                    }
                    else
                        return 403;  //无效权限
                }
                else
                    return 403;  //无效的用户
            }
            catch (Exception ex)
            {
                _companyRepository.Context.Rollback();
                return 0;
            }
        }

        //public int AddObject(CompanyObject obj)
        //{
        //    try
        //    {
        //        var company = new Company()
        //        {
        //            ID = obj.id,
        //            cityID = obj.cityID,
        //            name = obj.name,
        //            image = obj.image,
        //            addr = obj.addr,
        //            status = obj.status,
        //            certificates = obj.certificates,
        //            createDate = DateTime.Now
        //        };
        //        _companyRepository.Add(company);
        //        _companyRepository.Context.Commit();
        //        return company.ID;
        //    }
        //    catch
        //    {
        //        _companyRepository.Context.Rollback();
        //    }
        //    return 0;
        //}

        public CompanyObject GetObjectByID(int id)
        {
            try
            {
                var company = _companyRepository.Find(Specification<Company>.Eval(x => x.ID == id));
                if (company != null)
                {
                    var companyObject = Mapper.Instance.Map<CompanyObject>(company);
                    City city = _cityeRepository.Find(Specification<City>.Eval(x => x.ID == companyObject.id));
                    if (city != null)
                    {
                        companyObject.cityID = city.ID;
                        companyObject.cityName = city.name;
                    }
                    return companyObject;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                _companyRepository.Remove(new Company() { ID = id });
                _companyRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _companyRepository.Context.Rollback();
            }
            return 0;
        }

        //public int UpdateObject(CompanyObject obj)
        //{
        //    try
        //    {
        //        _companyRepository.Update(new Company()
        //        {
        //            ID = obj.id,
        //            cityID = obj.cityID,
        //            name = obj.name,
        //            image = obj.image,
        //            addr = obj.addr,
        //            status = obj.status,
        //            certificates = obj.certificates
        //        });
        //        _companyRepository.Context.Commit();
        //        return 1;
        //    }
        //    catch
        //    {
        //        _companyRepository.Context.Rollback();
        //    }
        //    return 0;
        //}
    }
}
