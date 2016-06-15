using System.Collections.Generic;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class ProvinceService : ApplicationService, IProvinceService
    {
        private readonly IRepository<int, Province> _provinceRepository;

        public ProvinceService(IRepositoryContext context, IRepository<int, Province> ProvinceRepository) : base(context)
        {
            _provinceRepository = ProvinceRepository;
        }

        public int AddObject(ProvinceObject obj)
        {
            try
            {
                var province = new Province()
                {
                    ID = obj.id,
                    name = obj.name
                };
                _provinceRepository.Add(province);
                _provinceRepository.Context.Commit();
                return province.ID;
            }
            catch
            {
                _provinceRepository.Context.Rollback();
                return 0;
            }
        }

        public ProvinceObject GetObjectByID(int id)
        {
            try
            {
                var province = _provinceRepository.Find(Specification<Province>.Eval(x => x.ID == id));
                var provinceObject = AutoMapper.Mapper.Map<ProvinceObject>(province);
                return provinceObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<ProvinceObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                var list = _provinceRepository.FindAll(x => x.ID, SortOrder.Ascending, 1, 1);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<ProvinceObject>>(list);
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
                _provinceRepository.Remove(new Province() { ID = id });
                _provinceRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _provinceRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(ProvinceObject obj)
        {
            try
            {
                _provinceRepository.Update(new Province()
                {
                    ID = obj.id,
                    name = obj.name
                });
                _provinceRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _provinceRepository.Context.Rollback();
                return 0;
            }
        }

        public IEnumerable<ProvinceObject> GetCities(GetCitiesRequestParam requestParam)
        {
            return Mapper.Instance.Map<IEnumerable<ProvinceObject>>(_provinceRepository.FindAll(m => m.Cities));
        }
    }
}
