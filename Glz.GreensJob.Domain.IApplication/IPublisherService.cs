using System.Collections.Generic;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IPublisherService : IApplicationServiceContract
    {
        PublisherObject GetObjectByID(int id);

        PublisherObject GetObjectByMobile(string mobile);

        int AddObject(PublisherObject obj);

        int RemoveObject(int id);

        int UpdateObject(PublisherObject obj);

        bool IsAdmin(int id);

        List<PublisherObject> GetUserInof(int userId);

        WebUserInfoModel LoginAction(LoginActionRequestParam requestParam);
    }
}
