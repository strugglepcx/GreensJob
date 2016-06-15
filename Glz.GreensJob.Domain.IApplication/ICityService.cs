using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface ICityService : IApplicationServiceContract
    {
        CityObject GetObjectByID(int id);

        PagedResult<CityObject> GetObjectByPaged(int parent, int pageIndex);

        int AddObject(CityObject obj);

        int RemoveObject(int id);

        int UpdateObject(CityObject obj);

        CityModel GetCityForCoordinate(GetCityForCoordinateRequestParam requestParam);
        IEnumerable<CityModel> GetOpenCities(GetOpenCitiesRequestParam requestParam);
    }
}
