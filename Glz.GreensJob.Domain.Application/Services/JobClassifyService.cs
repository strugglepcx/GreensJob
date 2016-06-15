using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobClassifyService : ApplicationService, IJobClassifyService
    {
        private readonly IRepository<int, JobClassify> _jobClassifyRepository = null;

        public JobClassifyService(IRepositoryContext context, IRepository<int, JobClassify> JobClassifyRepository) : base(context)
        {
            _jobClassifyRepository = JobClassifyRepository;
        }

        public int AddObject(JobClassifyObject obj)
        {
            try
            {
                var jobClassify = new JobClassify()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _jobClassifyRepository.Add(jobClassify);
                _jobClassifyRepository.Context.Commit();
                return jobClassify.ID;
            }
            catch
            {
                _jobClassifyRepository.Context.Rollback();
                return 0;
            }
        }

        public JobClassifyObject GetObjectByID(int id)
        {
            try
            {
                var jobClassify = _jobClassifyRepository.Find(Specification<JobClassify>.Eval(x => x.ID == id));
                var jobClassifyObject = AutoMapper.Mapper.Map<JobClassifyObject>(jobClassify);
                return jobClassifyObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<JobClassifyObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                var list = _jobClassifyRepository.FindAll(x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<JobClassifyObject>>(list);
                return pageResult;
            }
            catch
            {
                return null;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                _jobClassifyRepository.Remove(new JobClassify() { ID = id });
                _jobClassifyRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobClassifyRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(JobClassifyObject obj)
        {
            try
            {
                _jobClassifyRepository.Update(new JobClassify()
                {
                    ID = obj.id,
                    name = obj.name
                });
                _jobClassifyRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobClassifyRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
