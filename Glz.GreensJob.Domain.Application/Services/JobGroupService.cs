using System;
using System.Collections.Generic;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper.Internal;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobGroupService : ApplicationService, IJobGroupService
    {
        private readonly IRepository<int, JobGroup> _jobGroupRepository;
        private readonly IRepository<int, Job> _jobRepository;

        public JobGroupService(IRepositoryContext context, IRepository<int, JobGroup> JobGroupRepository, IRepository<int, Job> JobRepository) : base(context)
        {
            _jobGroupRepository = JobGroupRepository;
            _jobRepository = JobRepository;
        }

        public JobGroupObject GetObjectByID(int id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<JobGroupObject> GetObjectByPaged(int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int RemoveObject(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateObject(JobGroupObject obj)
        {
            throw new NotImplementedException();
        }

        public int RefreshJob(int jobGroupId)
        {
            try
            {
                var model = _jobGroupRepository.Find(Specification<JobGroup>.Eval(x => x.ID == jobGroupId));
                model.releaseDate = DateTime.Now;
                _jobGroupRepository.Update(model);

                var list = _jobRepository.FindAll(Specification<Job>.Eval(x => x.groupID == jobGroupId));
                list.Each(x => x.releaseDate = DateTime.Now);
                list.Each(x => _jobRepository.Update(x));
                Context.Commit();
                return 1;
            }
            catch (Exception)
            {
                _jobGroupRepository.Context.Rollback();
                return 0;
            }
        }

        public int DeleteJob(int jobGroupId)
        {
            var model = _jobGroupRepository.Find(Specification<JobGroup>.Eval(x => x.ID == jobGroupId));
            if (model == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "该职位已经被删除");
            }
            if (model.status == JobStatus.Employ || model.status == JobStatus.Stop || model.status == JobStatus.Expired)
            {
                throw new GreensJobException(StatusCodes.Failure, "该职位已经发布，无法删除");
            }
            _jobGroupRepository.Remove(model);
            _jobGroupRepository.Context.Commit();
            return 1;
        }

        public int StopJob(int jobGroupId)
        {
            try
            {
                var model = _jobGroupRepository.Find(Specification<JobGroup>.Eval(x => x.ID == jobGroupId));
                model.status = JobStatus.Stop;
                _jobGroupRepository.Update(model);

                var list = _jobRepository.FindAll(Specification<Job>.Eval(x => x.groupID == jobGroupId));
                list.Each(x => x.status = JobStatus.Stop);
                list.Each(x => _jobRepository.Update(x));
                Context.Commit();
                return 1;
            }
            catch (Exception)
            {
                _jobGroupRepository.Context.Rollback();
                return 0;
            }
        }

    }
}
