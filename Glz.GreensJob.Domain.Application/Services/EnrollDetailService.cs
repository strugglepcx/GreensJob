using System.Collections.Generic;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class EnrollDetailService : ApplicationService, IEnrollDetailService
    {
        private readonly IRepository<int, EnrollDetail> _enrollDetailRepository;

        public EnrollDetailService(IRepositoryContext context, IRepository<int, EnrollDetail> EnrollDetailRepository) : base(context)
        {
            _enrollDetailRepository = EnrollDetailRepository;
        }

        public int AddObject(List<EnrollDetailObject> list)
        {
            try
            {
                foreach (EnrollDetailObject obj in list)
                    _enrollDetailRepository.Add(new EnrollDetail()
                    {
                        enrollID = obj.enrollID,
                        date = obj.date,
                        start = obj.start,
                        end = obj.end,
                        isEmploy = obj.isEmploy,
                        isRetired = obj.isRetired
                    });
                _enrollDetailRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _enrollDetailRepository.Context.Rollback();
                return 0;
            }
        }

        public PagedResult<EnrollDetailObject> GetObjectByPaged(int enrollID)
        {
            try
            {
                var list = _enrollDetailRepository.FindAll(Specification<EnrollDetail>.Eval(x => x.enrollID == enrollID), x => x.date, SortOrder.Ascending, 1, 100);
                if (list != null)
                {
                    var pageResult = AutoMapper.Mapper.Map<PagedResult<EnrollDetailObject>>(list);
                    return pageResult;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public int UpdateObject(EnrollDetailObject obj)
        {
            try
            {

                _enrollDetailRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _enrollDetailRepository.Context.Rollback();
                return 0;
            }
        }

        public int RemoveObjectByParent(int parentID) {
            try
            {
                _enrollDetailRepository.Remove(new EnrollDetail() { enrollID = parentID });
                _enrollDetailRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _enrollDetailRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
