using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Apworks.Repositories;
using AutoMapper;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobQueryService : ApplicationService, IJobQueryService
    {
        private readonly IRepository<int, Job> _jobRepository;
        public JobQueryService(IRepositoryContext context, IRepository<int, Job> jobRepository) : base(context)
        {
            _jobRepository = jobRepository;
        }


        public PagedResultModel<GetJobsModel> GetJobsOrderByRelaseDate(GetJobsOrderByRelaseDateRequestParam requestParam)
        {
            var skip = (requestParam.pageIndex - 1) * requestParam.pageSize;
            var take = requestParam.pageSize;
            var queryJobs = _jobRepository.FindAll(
                x => x.PayCategory,
                x => x.PayUnit,
                x => x.JobClassify,
                x => x.JobCategory,
                x => x.District);
            var pagedGroupDescendingReleaseDate = queryJobs.Where(x =>
                        (string.IsNullOrEmpty(requestParam.keyword) || x.name.Contains(requestParam.keyword)) &&
                        (requestParam.@class == 0 || x.jobClassifyID == requestParam.@class) &&
                        (requestParam.schedule == 0 || x.jobCategoryID == requestParam.schedule) &&
                        (requestParam.payMethod == 0 || x.payCategoryID == requestParam.payMethod) &&
                        (requestParam.district == 0 || x.District_ID == requestParam.district) &&
                        (x.City_ID == requestParam.city) &&
                        (x.canEnroll == 1) &&
                        (x.status == JobStatus.Employ))
                        .OrderByDescending(x => x.releaseDate)
                        .Skip(skip)
                        .Take(take)
                        .GroupBy(p => new { Total = queryJobs.Count() })
                        .FirstOrDefault();

            if (pagedGroupDescendingReleaseDate == null)
                return null;
            var queryPagedResultJobs = new PagedResult<Job>(pagedGroupDescendingReleaseDate.Key.Total,
                (pagedGroupDescendingReleaseDate.Key.Total + requestParam.pageSize - 1) / requestParam.pageSize,
                requestParam.pageSize, requestParam.pageIndex,
                pagedGroupDescendingReleaseDate.Select(p => p).ToList());
            return Mapper.Instance.Map<PagedResultModel<GetJobsModel>>(queryPagedResultJobs);
        }
    }
}
