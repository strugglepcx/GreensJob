using System;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class AgencyRecruitJobService : ApplicationService, IAgencyRecruitJobService
    {
        private readonly IRepository<int, AgencyRecruitJob> _agencyRecruitJobRepository = null;

        public AgencyRecruitJobService(IRepositoryContext context, IRepository<int, AgencyRecruitJob> AgencyRecruitJobRepository) : base(context)
        {
            _agencyRecruitJobRepository = AgencyRecruitJobRepository;
        }

        public int AddObject(AgencyRecruitJobObject obj)
        {
            try
            {
                var agencyRecruitJob = new AgencyRecruitJob()
                {
                    ID = 0,
                    name = obj.name,
                    addr = obj.addr,
                    contact = obj.contact,
                    phone = obj.phone,
                    payUnit = obj.payUnit,
                    recruitNum = obj.recruitNum,
                    salary = obj.salary,
                    startDate = obj.startDate,
                    endDate = obj.endDate
                };
                _agencyRecruitJobRepository.Add(agencyRecruitJob);
                _agencyRecruitJobRepository.Context.Commit();
                return agencyRecruitJob.ID;
            }
            catch (Exception e)
            {
                _agencyRecruitJobRepository.Context.Rollback();
                return 0;
            }
        }

        public AgencyRecruitJobObject GetObjectByID(int id)
        {
            try
            {
                var agencyRecruitJob = _agencyRecruitJobRepository.Find(Specification<AgencyRecruitJob>.Eval(x => x.ID == id));
                var agencyRecruitJobObject = AutoMapper.Mapper.Map<AgencyRecruitJobObject>(agencyRecruitJob);
                return agencyRecruitJobObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<AgencyRecruitJobObject> GetObjectPaged(int pageIndex)
        {
            try
            {
                var list = _agencyRecruitJobRepository.FindAll(Specification<AgencyRecruitJob>.Eval(x => x.status == 0), x => x.createDate, SortOrder.Descending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<AgencyRecruitJobObject>>(list);
                return pageResult;
            }
            catch
            {
                return null;
            }
        }
    }
}
