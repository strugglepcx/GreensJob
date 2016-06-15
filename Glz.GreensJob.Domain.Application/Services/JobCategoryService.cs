using System;
using Apworks;
using Apworks.Repositories;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobCategoryService : ApplicationService, IJobCategoryService
    {
        private readonly IRepository<int, JobCategory> _jobCategoryRepository = null;

        public JobCategoryService(IRepositoryContext context, IRepository<int, JobCategory> JobCategoryRepository) : base(context)
        {
            _jobCategoryRepository = JobCategoryRepository;
        }

        public int AddObject(JobCategoryObject obj)
        {
            try
            {
                var jobCategory = new JobCategory()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _jobCategoryRepository.Add(jobCategory);
                _jobCategoryRepository.Context.Commit();
                return jobCategory.ID;
            }
            catch
            {
                _jobCategoryRepository.Context.Rollback();
                return 0;
            }
        }

        public JobCategoryObject GetObjectByID(int id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<JobCategoryObject> GetObjectByPaged(int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int RemoveObject(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateObject(JobCategoryObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
