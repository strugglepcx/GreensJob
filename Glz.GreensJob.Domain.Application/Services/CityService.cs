using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;
using Glz.Infrastructure.Maps;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class CityService : ApplicationService, ICityService
    {
        private readonly IRepository<int, City> _cityRepository;
        private readonly IRepository<int, OpenCity> _openCityRepository;
        private readonly IRepository<int, Province> _provinceRepository;
        private readonly IMapPlace _mapPlace;
        private readonly ICache _cache;

        public CityService(IRepositoryContext context, IRepository<int, City> cityRepository,
            IRepository<int, Province> provinceRepository, IMapPlace mapPlace, ICache cache,
            IRepository<int, OpenCity> openCityRepository) : base(context)
        {

            _cityRepository = cityRepository;

            _provinceRepository = provinceRepository;
            _mapPlace = mapPlace;
            _cache = cache;
            _openCityRepository = openCityRepository;
        }

        public int AddObject(CityObject obj)
        {
            try
            {
                var city = new City()
                {
                    ID = obj.id,
                    name = obj.name,
                    provinceID = obj.provinceID
                };
                _cityRepository.Add(city);
                _cityRepository.Context.Commit();
                return city.ID;
            }
            catch
            {
                _cityRepository.Context.Rollback();
                return 0;
            }
        }

        public CityObject GetObjectByID(int id)
        {
            try
            {
                var city = _cityRepository.Find(Specification<City>.Eval(x => x.ID == id));
                var cityObject = AutoMapper.Mapper.Instance.Map<CityObject>(city);
                if (city != null && city.provinceID > 0)
                {
                    var province = _provinceRepository.Find(Specification<Province>.Eval(x => x.ID == city.provinceID));
                    if (province != null)
                        cityObject.provinceName = province.name;
                }

                return cityObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<CityObject> GetObjectByPaged(int parent, int pageIndex)
        {
            try
            {
                var list = _cityRepository.FindAll(Specification<City>.Eval(x => x.provinceID == parent), x => x.ID, SortOrder.Ascending, pageIndex, 10);
                var pageResult = AutoMapper.Mapper.Map<PagedResult<CityObject>>(list);
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
                _cityRepository.Remove(new City() { ID = id });
                _cityRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _cityRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(CityObject obj)
        {
            try
            {
                _cityRepository.Update(new City()
                {
                    ID = obj.id,
                    name = obj.name,
                    provinceID = obj.provinceID
                });
                _cityRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _cityRepository.Context.Rollback();
                return 0;
            }
        }

        public CityModel GetCityForCoordinate(GetCityForCoordinateRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));

            var location = DbGeography.FromText($"POINT({requestParam.lng} {requestParam.lat})");
            string cityName;
            OpenCity city;
            var cityCoordinates = _cache.Get<List<CityCoordinateModel>>(Const.CityCoordinateCacheKey);

            if (cityCoordinates != null)
            {
                foreach (var cityCoordinateModel in cityCoordinates)
                {
                    cityCoordinateModel.Location = DbGeography.FromText($"POINT({cityCoordinateModel.Lng} {cityCoordinateModel.Lat})");
                }
                var cityCoordinate =
                    cityCoordinates.FirstOrDefault(x => x.Location.Distance(location) <= Const.CitySearchRadius);
                if (cityCoordinate != null)
                {
                    city = _openCityRepository.Find(Specification<OpenCity>.Eval(x => x.Name == cityCoordinate.Name));
                    if (city == null)
                    {
                        throw new GreensJobException(0, "很抱歉，该地区不能为您提供服务");
                    }
                    return Mapper.Instance.Map<CityModel>(city);
                }
                cityName = _mapPlace.GetCity(requestParam.lat, requestParam.lng, Const.CitySearchRadius);
                if (string.IsNullOrEmpty(cityName))
                    throw new GreensJobException(0, "很抱歉，该地区不能为您提供服务");
                cityCoordinates.Add(new CityCoordinateModel { Name = cityName, Lat = requestParam.lat, Lng = requestParam.lng, Location = location });
                city = _openCityRepository.Find(Specification<OpenCity>.Eval(x => x.Name == cityName));
                if (city == null)
                {
                    throw new GreensJobException(0, "很抱歉，该地区不能为您提供服务");
                }
                // 保存缓存
                _cache.Set(Const.CityCoordinateCacheKey, cityCoordinates);
                return Mapper.Instance.Map<CityModel>(city);

            }

            cityName = _mapPlace.GetCity(requestParam.lat, requestParam.lng, Const.CitySearchRadius);
            if (string.IsNullOrEmpty(cityName))
                throw new GreensJobException(0, "很抱歉，该地区不能为您提供服务");
            city = _openCityRepository.Find(Specification<OpenCity>.Eval(x => x.Name == cityName));
            if (city == null)
            {
                throw new GreensJobException(0, "很抱歉，该地区不能为您提供服务");
            }

            cityCoordinates = new List<CityCoordinateModel>
            {
                new CityCoordinateModel {Name = cityName, Lat = requestParam.lat, Lng = requestParam.lng, Location = location}
            };
            // 保存缓存
            _cache.Set(Const.CityCoordinateCacheKey, cityCoordinates);
            return Mapper.Instance.Map<CityModel>(city);
        }

        public IEnumerable<CityModel> GetOpenCities(GetOpenCitiesRequestParam requestParam)
        {
            var result = _openCityRepository.FindAll().OrderByDescending(city => city.Name);
            return Mapper.Instance.Map<IEnumerable<CityModel>>(result);
        }
    }
}
