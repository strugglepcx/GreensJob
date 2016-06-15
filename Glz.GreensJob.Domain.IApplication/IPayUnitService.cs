using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IPayUnitService : IApplicationServiceContract
    {
        PayUnitObject GetObjectByID(int id);

        PagedResult<PayUnitObject> GetObjectByPaged(int pageIndex);

        int AddObject(PayUnitObject obj);

        int RemoveObject(int id);

        int UpdateObject(PayUnitObject obj);
    }
}
