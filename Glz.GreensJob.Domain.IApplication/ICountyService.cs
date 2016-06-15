using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface ICountyService : IApplicationServiceContract
    {
        CountyObject GetObjectByID(int id);

        PagedResult<CountyObject> GetObjectByPaged(int parent, int pageIndex);

        int AddObject(CountyObject obj);

        int RemoveObject(int id);

        int UpdateObject(CountyObject obj);
    }
}
