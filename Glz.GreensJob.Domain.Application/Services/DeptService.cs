using Apworks.Repositories;
using Apworks.Specifications;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class DeptService : ApplicationService, IDeptService
    {
        private readonly IRepository<int, Dept> _deptRepository;

        public DeptService(IRepositoryContext context, IRepository<int, Dept> DeptRepository) : base(context)
        {
            _deptRepository = DeptRepository;
        }

        public DeptObject GetObjectByID(int id)
        {
            try
            {
                var dept = _deptRepository.Find(Specification<Dept>.Eval(x => x.ID == id));
                var deptobject = AutoMapper.Mapper.Map<DeptObject>(dept);
                return deptobject;
            }
            catch
            {
                return null;
            }
        }

        public void AddObject(DeptObject dept)
        {
            try
            {
                _deptRepository.Add(new Dept()
                {
                    name = dept.name
                });
                _deptRepository.Context.Commit();
            }
            catch { }
        }

        public void RemoveObject(int id)
        {
            try
            {
                _deptRepository.Remove(new Dept() { ID = id });
                _deptRepository.Context.Commit();
            }
            catch { }
        }

        public void UptObject(DeptObject dept)
        {
            try
            {
                _deptRepository.Update(new Dept() { ID = dept.id, name = dept.name });
                _deptRepository.Context.Commit();
            }
            catch { }
        }
    }
}
