using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobClassifyService : IApplicationServiceContract
    {
        JobClassifyObject GetObjectByID(int id);

        PagedResult<JobClassifyObject> GetObjectByPaged(int pageIndex);

        int AddObject(JobClassifyObject obj);

        int RemoveObject(int id);

        int UpdateObject(JobClassifyObject obj);
    }
}
