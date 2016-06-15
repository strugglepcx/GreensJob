using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IPayCategoryService : IApplicationServiceContract
    {
        PayCategoryObject GetObjectByID(int id);

        PagedResult<PayCategoryObject> GetObjectByPaged(int pageIndex);

        int AddObject(PayCategoryObject obj);

        int RemoveObject(int id);

        int UpdateObject(PayCategoryObject obj);
    }
}
