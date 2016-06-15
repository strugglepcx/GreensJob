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
    public class CountyService : ApplicationService, ICountyService
    {
        private readonly IRepository<int, County> _countyRepository = null;
        private readonly IRepository<int, City> _cityRepository = null;
        private readonly IRepository<int, Province> _provinceRepository = null;

        public CountyService(IRepositoryContext context, IRepository<int, County> CountyRepository, IRepository<int, City> CityRepository, IRepository<int, Province> ProvinceRepository) : base(context)
        {
            _countyRepository = CountyRepository;

            _cityRepository = CityRepository;

            _provinceRepository = ProvinceRepository;
        }

        public int AddObject(CountyObject obj)
        {
            try
            {
                var county = new County()
                {
                    ID = obj.id,
                    name = obj.name,
                    cityID = obj.cityID
                };
                _countyRepository.Add(county);
                _countyRepository.Context.Commit();
                return county.ID;
            }
            catch
            {
                _countyRepository.Context.Rollback();
                return 0;
            }
        }

        public CountyObject GetObjectByID(int id)
        {
            try
            {
                var county = _countyRepository.Find(Specification<County>.Eval(x => x.ID == id));
                var countyObject = AutoMapper.Mapper.Instance.Map<CountyObject>(county);
                if (county != null && county.cityID > 0)
                {
                    var city = _cityRepository.Find(Specification<City>.Eval(x => x.ID == county.cityID));
                    if (city != null && city.provinceID > 0)
                    {
                        countyObject.cityName = city.name;
                        var province = _provinceRepository.Find(Specification<Province>.Eval(x => x.ID == city.provinceID));
                        if (province != null)
                        {
                            countyObject.provinceID = province.ID;
                            countyObject.provinceName = province.name;
                        }
                    }
                }
                return countyObject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PagedResult<CountyObject> GetObjectByPaged(int parent, int pageIndex)
        {
            try
            {
                var list = _countyRepository.FindAll(Specification<County>.Eval(x => x.cityID == parent), x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<CountyObject>>(list);
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
                _countyRepository.Remove(new County() { ID = id });
                _countyRepository.Context.Commit();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateObject(CountyObject obj)
        {
            try
            {
                _countyRepository.Update(new County()
                {
                    ID = obj.id,
                    name = obj.name,
                    cityID = obj.cityID
                });
                _countyRepository.Context.Commit();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
