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

namespace Glz.GreensJob.Domain.Application
{
    public class JobDraftService : ApplicationService, IJobDraftService
    {
        private readonly IRepository<int, JobDraft> _jobDraftRepository;
        public JobDraftService(IRepositoryContext context, IRepository<int, JobDraft> jobDraftRepository) : base(context)
        {
            _jobDraftRepository = jobDraftRepository;
        }

        JobDraftObject IJobDraftService.GetObjectByID(int id)
        {
            try
            {
                var jobDraft = _jobDraftRepository.Find(Specification<JobDraft>.Eval(x => x.ID == id));
                var jobDraftObject = AutoMapper.Mapper.Map<JobDraftObject>(jobDraft);
                return jobDraftObject;
            }
            catch
            {
                return null;
            }
        }

        public int AddObject(AddJobDraftRequestParam pram)
        {
            try
            {
                _jobDraftRepository.Add(new JobDraft()
                {
                    Contents = pram.Contents,
                    PublisherID = pram.userId,
                    CompanyID = pram.companyId,
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now
                });
                _jobDraftRepository.Context.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                _jobDraftRepository.Context.Rollback();
                return 0;
            }
        }

        public PagedResultModel<JobDraftObject> GetObjectByPaged(GetJobDraftListRequestParam param)
        {
            try
            {
                var skip = (param.PageIndex - 1) * param.PageSize;
                var take = param.PageSize;
                var queryJobDrafts = _jobDraftRepository.FindAll(x => x.ID,x => x.Contents);
                var pagedGroupDescendingReleaseDate = queryJobDrafts.Where(x=>x.PublisherID==param.userId)
                            .OrderBy(x => x.ID)
                            .Skip(skip)
                            .Take(take)
                            .GroupBy(p => new { Total = queryJobDrafts.Count() })
                            .FirstOrDefault();

                if (pagedGroupDescendingReleaseDate == null)
                    return null;
                var queryPagedResultJobs = new PagedResult<JobDraft>(pagedGroupDescendingReleaseDate.Key.Total,
                    (pagedGroupDescendingReleaseDate.Key.Total + param.PageSize - 1) / param.PageSize,
                    param.PageSize, param.PageIndex,
                    pagedGroupDescendingReleaseDate.Select(p => p).ToList());
                return Mapper.Instance.Map<PagedResultModel<JobDraftObject>>(queryPagedResultJobs);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                _jobDraftRepository.Remove(new JobDraft() { ID = id });
                _jobDraftRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobDraftRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
