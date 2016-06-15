using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobGroupService : IApplicationServiceContract
    {
        JobGroupObject GetObjectByID(int id);

        PagedResult<JobGroupObject> GetObjectByPaged(int pageIndex);

        int RemoveObject(int id);

        int UpdateObject(JobGroupObject obj);

        [Caching(CachingMethod.Remove, "GetJobsOrderByRelaseDate")]
        int RefreshJob(int jobGroupId);

        [Caching(CachingMethod.Remove, "GetJobsOrderByRelaseDate")]
        int DeleteJob(int jobGroupId);

        [Caching(CachingMethod.Remove, "GetJobsOrderByRelaseDate")]
        int StopJob(int jobGroupId);
    }
}
