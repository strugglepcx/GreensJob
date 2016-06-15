using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Dto;
using Glz.GreensJob.WebApi.Models;
using Glz.Infrastructure;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.WebApi.Controllers
{
    [RoutePrefix("api/publisher")]
    public class PublisherController : ApiBaseController
    {
        private readonly IPublisherService _publisher;
        public PublisherController(IPublisherService publisher)
        {
            _publisher = publisher;
        }

        /*
        [Route("V1/getUserInfo")]
        [HttpPost]
        public OutPublisherModel GetUserInfo([FromBody]InPublisherModel appKey)
        {
            try
            {
                var model = CreateResult<OutPublisherModel>(1, "");
                var list = _publisher.GetUserInof(appKey.userID);
                if (list == null)
                {
                    return CreateResult<OutPublisherModel>(1000, "请刷新页面");
                }
                model.userRecords = new PublisherModel();
                model.userRecords.userID = appKey.userID;
                model.userRecords.administor = list.First(x => x.id == appKey.userID).isAdmin;
                model.userRecords.uerRecord =
                    list.Select
                    (input => new UserRecord
                    {
                        addUserRight = input.PublisherRight.AddUser,
                        deleteUserRight = input.PublisherRight.DeleteUser,
                        finicialRight = input.PublisherRight.Finicial,
                        importEmployeeRight = input.PublisherRight.ImportEmployee,
                        modifyUserRight = input.PublisherRight.ModifyUser,
                        releaseJobRight = input.PublisherRight.ReleaseJob,
                        userMobileNumber = input.mobile,
                        userName = input.name
                    }).ToList();

                return model;
            }
            catch (Exception)
            {
                return CreateResult<OutPublisherModel>(1000, "请刷新页面");
            }
        }

        [Route("V1/saveUserInfo")]
        [HttpPost]
        public ResultBase saveUserInfo([FromBody]InSaveUserInfoModel appKey)
        {
            try
            {
                var model = new PublisherObject
                {
                    id = appKey.userID,
                    name = appKey.userName,
                    password = appKey.password,
                    mobile = appKey.userMobile,
                    PublisherRight = new PublisherRightObject
                    {
                        AddUser = appKey.addUserRight,
                        DeleteUser = appKey.deleteUserRight,
                        Finicial = appKey.finicialRight,
                        ID = appKey.userID,
                        ImportEmployee = appKey.importEmployeeRight,
                        ModifyUser = appKey.modifyUserRight,
                        ReleaseJob = appKey.releaseJobRight
                    }
                };
                int result = _publisher.UpdateObject(model);
                if (result == 1)
                {
                    return CreateResult<ResultBase>(1, "");
                }
                else
                {
                    return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
                }
            }
            catch (Exception)
            {
                return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
            }
        }

        [Route("V1/deleteUser")]
        [HttpPost]
        public ResultBase deleteUser([FromBody] InDeleteUserModel appKey)
        {
            try
            {
                int result = _publisher.RemoveObject(appKey.userID);
                if (result == 1)
                {
                    return CreateResult<ResultBase>(1, "");
                }
                else
                {
                    return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
                }
            }
            catch (Exception)
            {
                return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
            }
        }

        [Route("V1/addUser")]
        [HttpPost]
        public ResultBase addUser([FromBody]InSaveUserInfoModel appKey)
        {
            try
            {
                var model = new PublisherObject
                {
                    name = appKey.userName,
                    password = appKey.password,
                    mobile = appKey.userMobile,
                    PublisherRight = new PublisherRightObject
                    {
                        AddUser = appKey.addUserRight,
                        DeleteUser = appKey.deleteUserRight,
                        Finicial = appKey.finicialRight,
                        ID = appKey.userID,
                        ImportEmployee = appKey.importEmployeeRight,
                        ModifyUser = appKey.modifyUserRight,
                        ReleaseJob = appKey.releaseJobRight
                    }
                };
                int result = _publisher.AddObject(model);
                if (result >0)
                {
                    return CreateResult<ResultBase>(1, "");
                }
                else
                {
                    return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
                }
            }
            catch (Exception)
            {
                return CreateResult<ResultBase>(1000, "后台操作失败，请重试");
            }
        }
        */

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getUserInfo")]

        public OutPublisherModel GetUserInfo([FromBody]IdentityRequestParam appKey)
        {
            try
            {
                var model = CreateResult<OutPublisherModel>(StatusCodes.Success, "");
                var list = _publisher.GetUserInof(appKey.userId);
                if (list == null)
                {
                    return CreateResult<OutPublisherModel>(StatusCodes.Failure, "请刷新页面");
                }
                model.userRecords = new PublisherModel();
                model.userRecords.userID = appKey.userId;
                model.userRecords.administor = list.First(x => x.id == appKey.userId).isAdmin;
                model.userRecords.uerRecord =
                    list.Select
                    (input => new UserRecord
                    {
                        id = input.id,
                        addUserRight = input.PublisherRight.AddUser,
                        deleteUserRight = input.PublisherRight.DeleteUser,
                        finicialRight = input.PublisherRight.Finicial,
                        importEmployeeRight = input.PublisherRight.ImportEmployee,
                        modifyUserRight = input.PublisherRight.ModifyUser,
                        releaseJobRight = input.PublisherRight.ReleaseJob,
                        userMobileNumber = input.mobile,
                        userName = input.name
                    }).ToList();

                return model;
            }
            catch (Exception ex)
            {
                return CreateResult<OutPublisherModel>(StatusCodes.Failure, "请刷新页面");
            }
        }

        [HttpPost]
        [Route("v1/GetUserDetail")]
        public OutPublisherModel GetUserDetail([FromBody]PublisherActionRequestParam appKey)
        {
            var obj = _publisher.GetObjectByID(appKey.id);
            if (obj != null)
            {
                var model = CreateResult<OutPublisherModel>(StatusCodes.Success, "");
                model.userRecords = new PublisherModel();
                model.userRecords.userID = obj.id;
                model.userRecords.administor = obj.isAdmin;
                model.userRecords.uerRecord = new List<UserRecord>();
                model.userRecords.uerRecord.Add(new UserRecord()
                {
                    id = obj.id,
                    addUserRight = obj.PublisherRight.AddUser,
                    deleteUserRight = obj.PublisherRight.DeleteUser,
                    finicialRight = obj.PublisherRight.Finicial,
                    importEmployeeRight = obj.PublisherRight.ImportEmployee,
                    modifyUserRight = obj.PublisherRight.ModifyUser,
                    releaseJobRight = obj.PublisherRight.ReleaseJob,
                    userMobileNumber = obj.mobile,
                    userName = obj.name
                });
                return model;
            }
            else
                return CreateResult<OutPublisherModel>(StatusCodes.Failure, "请刷新页面");
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/saveUserInfo")]
        public ResultBase saveUserInfo([FromBody]PublisherActionRequestParam appKey)
        {
            try
            {
                var model = _publisher.GetObjectByID(appKey.id);
                if (model != null)
                {
                    model.name = appKey.userName;
                    model.mobile = appKey.userMobile;
                    // 如果密码没有传递，那么则不修改用户密码
                    if (!string.IsNullOrEmpty(appKey.password))
                        model.password = DES.MD5Encode(appKey.password);
                    model.PublisherRight.AddUser = appKey.addUserRight;
                    model.PublisherRight.DeleteUser = appKey.deleteUserRight;
                    model.PublisherRight.Finicial = appKey.finicialRight;
                    model.PublisherRight.ID = appKey.id;
                    model.PublisherRight.ImportEmployee = appKey.importEmployeeRight;
                    model.PublisherRight.ModifyUser = appKey.modifyUserRight;
                    model.PublisherRight.ReleaseJob = appKey.releaseJobRight;
                }
                int result = _publisher.UpdateObject(model);
                if (result == 1)
                {
                    return CreateResult<ResultBase>(StatusCodes.Success, "");
                }
                else
                {
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
                }
            }
            catch (Exception)
            {
                return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/deleteUser")]
        public ResultBase deleteUser([FromBody] PublisherActionRequestParam appKey)
        {
            try
            {
                int result = _publisher.RemoveObject(appKey.id);
                if (result == 1)
                {
                    return CreateResult<ResultBase>(StatusCodes.Success, "");
                }
                else
                {
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
                }
            }
            catch (Exception)
            {
                return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/addUser")]
        public ResultBase addUser([FromBody]PublisherActionRequestParam appKey)
        {
            try
            {
                var publisher = _publisher.GetObjectByMobile(appKey.userMobile);
                if (publisher != null)
                {
                    if (appKey.userMobile == publisher.mobile)
                    {
                        return CreateResult<ResultBase>(StatusCodes.Failure, "手机号已经注册！");
                    }
                }

                if (string.IsNullOrEmpty(appKey.password))
                    appKey.password = "123456";
                var model = new PublisherObject
                {
                    name = appKey.userName,
                    password = DES.MD5Encode(appKey.password),
                    mobile = appKey.userMobile,
                    companyID = appKey.companyId,
                    //isAdmin= appKey.isAdmin,
                    PublisherRight = new PublisherRightObject
                    {
                        AddUser = appKey.addUserRight,
                        DeleteUser = appKey.deleteUserRight,
                        Finicial = appKey.finicialRight,
                        ID = appKey.id,
                        ImportEmployee = appKey.importEmployeeRight,
                        ModifyUser = appKey.modifyUserRight,
                        ReleaseJob = appKey.releaseJobRight
                    }       

            };
                int result = _publisher.AddObject(model);
                if (result > 0)
                {
                    return CreateResult<ResultBase>(StatusCodes.Success, "");
                }
                else
                {
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
                }
            }
            catch (Exception ex)
            {
                return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

    }
}
