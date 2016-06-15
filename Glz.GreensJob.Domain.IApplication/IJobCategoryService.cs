using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobCategoryService : IApplicationServiceContract
    {
        JobCategoryObject GetObjectByID(int id);

        PagedResult<JobCategoryObject> GetObjectByPaged(int pageIndex);

        int AddObject(JobCategoryObject obj);

        int RemoveObject(int id);

        int UpdateObject(JobCategoryObject obj);
    }
}
