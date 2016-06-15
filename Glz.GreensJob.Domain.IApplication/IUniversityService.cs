using System.Collections.Generic;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IUniversityService : IApplicationServiceContract
    {

        int AddObject(UniversityObject obj);

        int UpdateObject(UniversityObject obj);

        int RemoveObject(int id);

        UniversityObject GetObjectByID(int id);
        IEnumerable<UniversityObject> GetUniversitys(GetUniversitysRequestParam requestParam);
    }
}
