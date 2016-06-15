using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobSchduleService : IApplicationServiceContract
    {
        JobSchduleObject GetObjectByID(int id);

        PagedResult<JobSchduleObject> GetObjectByPaged(int pageIndex);

        int AddObject(JobSchduleObject obj);

        int RemoveObject(int id);

        int UpdateObject(JobSchduleObject obj);
    }
}
