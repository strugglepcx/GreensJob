using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class PayCategoryService : ApplicationService, IPayCategoryService
    {
        private readonly IRepository<int, PayCategory> _payCategoryRepository = null;

        public PayCategoryService(IRepositoryContext context, IRepository<int, PayCategory> PayCategoryRepository) : base(context)
        {
            _payCategoryRepository = PayCategoryRepository;
        }

        public int AddObject(PayCategoryObject obj)
        {
            try
            {
                var payCategory = new PayCategory()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _payCategoryRepository.Add(payCategory);
                _payCategoryRepository.Context.Commit();
                return payCategory.ID;
            }
            catch
            {
                _payCategoryRepository.Context.Rollback();
                return 0;
            }
        }

        public PayCategoryObject GetObjectByID(int id)
        {
            try
            {
                var payCategory = _payCategoryRepository.Find(Specification<PayCategory>.Eval(x => x.ID == id));
                var payCategoryObject = AutoMapper.Mapper.Map<PayCategoryObject>(payCategory);
                return payCategoryObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<PayCategoryObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                var list = _payCategoryRepository.FindAll(x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<PayCategoryObject>>(list);
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
                _payCategoryRepository.Remove(new PayCategory()
                {
                    ID = id
                });
                _payCategoryRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _payCategoryRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(PayCategoryObject obj)
        {
            try
            {
                _payCategoryRepository.Update(new PayCategory()
                {
                    ID = obj.id,
                    name = obj.name
                });
                _payCategoryRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _payCategoryRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
