using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobService : IApplicationServiceContract
    {
        JobObject GetObjectByID(int id);

        PagedResultModel<GetJobsModel> GetJobs(GetJobsRequestParam requestParam);

        int AddObject(List<JobObject> list);

        int RemoveObject(int id);

        int UpdateObject(JobObject obj);

        [Caching(CachingMethod.Remove, "GetJobsOrderByRelaseDate", "GetJobGroupsByCompany")]
        void ReleaseJob(ReleaseJobRequestParam requestParam);

        PagedResultModel<JobInfo> GetJobInfo(GetJobInfoRequestParam requestParam);

        void EditJobInfo(EditJobInfoRequestParam requestParam);

        JobGroupObject GetJobGroupDetail(GetJobGroupDetailRequestParam requestParam);

        JobModel GetJobDetail(GetJobDetailRequestParam requestParam);

        PagedResultModel<EmployeeJobInfo> GetEmployeeJobs(GetEmployeeJobsRequestParam requestParam);

        PagedResultModel<GetJobsModel> SearchJobs(SearchJobsRequestParam requestParam);

        [Caching(CachingMethod.Remove, "GetJobsOrderByRelaseDate", "GetJobGroupsByCompany")]
        void UpdateExpiredJobStatus();

        //[Caching(CachingMethod.Get)]
        IEnumerable<GetJobGroupsByCompanyModel> GetJobGroupsByCompany(GetJobGroupsByCompanyRequestParam requestParam);
    }
}
