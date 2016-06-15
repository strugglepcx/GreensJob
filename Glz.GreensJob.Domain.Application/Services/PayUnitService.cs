using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class PayUnitService : ApplicationService, IPayUnitService
    {
        private readonly IRepository<int, PayUnit> _payUnitRepository = null;

        public PayUnitService(IRepositoryContext context, IRepository<int, PayUnit> PayUnitRepository) : base(context)
        {
            _payUnitRepository = PayUnitRepository;
        }

        public int AddObject(PayUnitObject obj)
        {
            try
            {
                var payUnit = new PayUnit()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _payUnitRepository.Add(payUnit);
                _payUnitRepository.Context.Commit();
                return payUnit.ID;
            }
            catch
            {
                _payUnitRepository.Context.Rollback();
                return 0;
            }
        }

        public PayUnitObject GetObjectByID(int id)
        {
            try
            {
                var payUnit = _payUnitRepository.Find(Specification<PayUnit>.Eval(x => x.ID == id));
                var payUnitObject = AutoMapper.Mapper.Map<PayUnitObject>(payUnit);
                return payUnitObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<PayUnitObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                var list = _payUnitRepository.FindAll(x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<PayUnitObject>>(list);
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
                _payUnitRepository.Remove(new PayUnit()
                {
                    ID = id
                });
                _payUnitRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _payUnitRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(PayUnitObject obj)
        {
            try
            {
                _payUnitRepository.Update(new PayUnit()
                {
                    ID = obj.id,
                    name = obj.name
                });
                _payUnitRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _payUnitRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
