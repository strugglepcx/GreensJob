using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobSchduleService : ApplicationService, IJobSchduleService
    {
        private readonly IRepository<int, JobSchdule> _jobSchduleRepository = null;

        public JobSchduleService(IRepositoryContext context, IRepository<int, JobSchdule> JobSchduleRepository) : base(context)
        {
            _jobSchduleRepository = JobSchduleRepository;
        }

        public int AddObject(JobSchduleObject obj)
        {
            try
            {
                var jobSchdule = new JobSchdule()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _jobSchduleRepository.Add(jobSchdule);
                _jobSchduleRepository.Context.Commit();
                return jobSchdule.ID;
            }
            catch
            {
                _jobSchduleRepository.Context.Rollback();
                return 0;
            }
        }

        public JobSchduleObject GetObjectByID(int id)
        {
            try
            {
                var jobSchdule = _jobSchduleRepository.Find(Specification<JobSchdule>.Eval(x => x.ID == id));
                var jobSchduleObject = AutoMapper.Mapper.Map<JobSchduleObject>(jobSchdule);
                return jobSchduleObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<JobSchduleObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                var list = _jobSchduleRepository.FindAll(x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<JobSchduleObject>>(list);
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
                _jobSchduleRepository.Add(new JobSchdule()
                {
                    ID = id
                });
                _jobSchduleRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobSchduleRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(JobSchduleObject obj)
        {
            try
            {
                _jobSchduleRepository.Update(new JobSchdule()
                {
                    ID = obj.id,
                    name = obj.name
                });
                _jobSchduleRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobSchduleRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
