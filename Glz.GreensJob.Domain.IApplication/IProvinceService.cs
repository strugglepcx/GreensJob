using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IProvinceService : IApplicationServiceContract
    {
        ProvinceObject GetObjectByID(int id);

        PagedResult<ProvinceObject> GetObjectByPaged(int pageIndex);

        int AddObject(ProvinceObject obj);

        int RemoveObject(int id);

        int UpdateObject(ProvinceObject obj);

        IEnumerable<ProvinceObject> GetCities(GetCitiesRequestParam requestParam);
    }
}
