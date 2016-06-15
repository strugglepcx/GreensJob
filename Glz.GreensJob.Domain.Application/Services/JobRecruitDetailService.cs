using Glz.GreensJob.Domain.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Dto;
using Apworks.Repositories;
using Glz.GreensJob.Domain.Models;
using Apworks.Specifications;
using Glz.GreensJob.Dto.RequestParams;
using Apworks;
using Apworks.Storage;
using Glz.Infrastructure;
using AutoMapper;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobRecruitDetailService : ApplicationService, IJobRecruitDetailService
    {
        private readonly IRepository<int, JobRecruitDetail> _jobRecruitDetailRepository;
        public JobRecruitDetailService(IRepositoryContext context, IRepository<int, JobRecruitDetail> jobRecruitDetailRepository) : base(context)
        {
            _jobRecruitDetailRepository = jobRecruitDetailRepository;
        }

        JobRecruitDetailModel IJobRecruitDetailService.GetObjectByID(int id)
        {
            try
            {
                var jobRecruitDetail = _jobRecruitDetailRepository.Find(Specification<JobRecruitDetail>.Eval(x => x.ID == id));
                var jobRecruitDetailObject = AutoMapper.Mapper.Map<JobRecruitDetailModel>(jobRecruitDetail);
                return jobRecruitDetailObject;
            }
            catch
            {
                return null;
            }
        }

        PagedResultModel<JobRecruitDetailModel> IJobRecruitDetailService.GetDailyRecruitList(GetDailyRecruitRequestParam param)
        {
            try
            {
                var skip = (param.PageIndex - 1) * param.PageSize;
                var take = param.PageSize;

                var queryjobRecruitDetails = _jobRecruitDetailRepository.FindAll().Where(x => x.JobGroup_ID == param.JobGroupId && (x.ApplicantNum > 0 || x.EmploymentNum > 0))
                    .GroupBy(u => new { u.RecruitDate, u.JobGroup_ID, u.RecruitNum, u.ApplicantNum }).Select(g => new JobRecruitDetailModel { Date = g.Key.RecruitDate, ApplicantNum = g.Sum(x=>x.ApplicantNum), RecruitNum = g.Key.RecruitNum, EmploymentNum = g.Sum(x => x.EmploymentNum) });

                var pagedGroupDescendingReleaseDate = queryjobRecruitDetails
                            .OrderBy(p => p.Date)
                            .Skip(skip)
                            .Take(take)
                            .GroupBy(p => new { Total = queryjobRecruitDetails.Count() })
                            .FirstOrDefault();

                if (pagedGroupDescendingReleaseDate == null)
                    return null;

                var queryPagedResultjobRecruitDetails = new PagedResult<JobRecruitDetailModel>(pagedGroupDescendingReleaseDate.Key.Total,
                    (pagedGroupDescendingReleaseDate.Key.Total + param.PageSize - 1) / param.PageSize,
                    param.PageSize, param.PageIndex, pagedGroupDescendingReleaseDate.Select(p => p).ToList());

                return Mapper.Instance.Map<PagedResultModel<JobRecruitDetailModel>>(queryPagedResultjobRecruitDetails);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
