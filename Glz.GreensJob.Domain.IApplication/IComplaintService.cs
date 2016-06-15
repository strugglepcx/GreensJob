using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IComplaintService : IApplicationServiceContract
    {
        int AddObject(ComplaintObject obj);

        ComplaintObject GetObjectByID(int id);

        PagedResult<ComplaintObject> GetObjectByPaged(int pageIndex);

        int RemoveObject(int id);
    }
}
