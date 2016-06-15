using System;
using System.Collections.Generic;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using System.Linq;
using Apworks.Storage;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class CollectService : ApplicationService, ICollectService
    {
        private readonly IRepository<int, Collect> _collectRepository;
        private readonly IRepository<int, Job> _jobRepository;
        private readonly IRepository<int, JobGroup> _jobGroupRepository;
        private readonly IRepository<int, JobSeeker> _jobSeekerRepository;

        public CollectService(IRepositoryContext context, IRepository<int, Collect> collectRepository,
            IRepository<int, Job> jobRepository, IRepository<int, JobGroup> jobGroupRepository,
            IRepository<int, JobSeeker> jobSeekerRepository) : base(context)
        {
            _collectRepository = collectRepository;
            _jobRepository = jobRepository;
            _jobGroupRepository = jobGroupRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }

        public int Count(int jobSeekerID, bool status)
        {
            try
            {
                var query = _collectRepository.FindAll(Specification<Collect>.Eval(x => x.jobSeekerID == jobSeekerID && x.status == status));
                return query.Count<Collect>();
            }
            catch
            {
                return 0;
            }
        }

        public int AddObject(CollectObject obj)
        {
            try
            {
                // 先查询求职者对职位的收藏情况
                var collect = _collectRepository.Find(Specification<Collect>.Eval(x => x.jobID == obj.jobID && x.jobSeekerID == obj.jobSeekerID));

                // 如果没有找到相关记录 则新增
                if (collect == null)
                {
                    collect = new Collect()
                    {
                        jobID = obj.jobID,
                        jobSeekerID = obj.jobSeekerID
                    };
                    _collectRepository.Add(collect);
                    // 更新职位记录
                    var job = _jobRepository.GetByKey(obj.jobID);
                    if (job != null)
                    {
                        job.collectNum++;
                        _jobRepository.Update(job);
                    }
                    _collectRepository.Context.Commit();
                    return collect.ID;
                }
                else
                {
                    return -1;  //已收藏
                }
            }
            catch
            {
                _collectRepository.Context.Rollback();
                return 0;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                _collectRepository.Remove(new Collect() { ID = id });
                _collectRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _collectRepository.Context.Rollback();
                return 0;
            }
        }

        public void CancelCollect(CancelCollectRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var collect =
                _collectRepository.Find(
                    Specification<Collect>.Eval(
                        x => x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID));
            if (collect == null)
            {
                throw new GreensJobException(0, "你还没有收藏该职位");
            }
            job.collectNum--;
            _collectRepository.Remove(collect);
            _jobRepository.Update(job);
            Context.Commit();
        }

        public void FavoriteJob(FavoriteJobRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var collect =
                _collectRepository.Find(
                    Specification<Collect>.Eval(
                        x => x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID));
            if (collect != null)
            {
                throw new GreensJobException(0, "你已经收藏该职位");
            }
            job.collectNum++;
            _collectRepository.Add(new Collect
            {
                jobID = job.ID,
                jobSeekerID = jobSeeker.ID,
                status = false
            });
            _jobRepository.Update(job);
            Context.Commit();
        }

        public PagedResult<CollectObject> GetObjectByPaged(int jobSeekerID, int pageIndex)
        {
            try
            {
                var results = _collectRepository.FindAll(Specification<Collect>.Eval(x => x.jobSeekerID == jobSeekerID), x => x.ID, SortOrder.Descending, pageIndex, 10);
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
