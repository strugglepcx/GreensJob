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
    public class ResumeService : ApplicationService, IResumeService
    {
        private readonly IRepository<int, Resume> _resumeRepository;
        private readonly IRepository<int, University> _universityRepository;
        private readonly IRepository<int, City> _cityRepository;
        private readonly IRepository<int, Province> _provinceRepository;

        public ResumeService(IRepositoryContext context, IRepository<int, Resume> ResumeRepository, IRepository<int, University> UniversityRepository, IRepository<int, City> CityRepository, IRepository<int, Province> ProvinceRepository) : base(context)
        {
            _resumeRepository = ResumeRepository;
            _universityRepository = UniversityRepository;
            _cityRepository = CityRepository;
            _provinceRepository = ProvinceRepository;
        }

        public ResumeObject GetObjectByUserID(int id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var resume = _resumeRepository.Find(Specification<Resume>.Eval(x => x.ID == id));
            //    if (resume != null)
            //    {
            //        var resumeObject = AutoMapper.Mapper.Instance.Map<ResumeObject>(resume);
            //        // 大学
            //        if (resume.University_ID > 0)
            //        {
            //            var university = _universityRepository.Find(Specification<University>.Eval(x => x.ID
            //            == resume.University_ID));
            //            if (university != null)
            //                resumeObject.universityName = university.name;
            //        }
            //        // 地市
            //        if (resume.City_ID > 0)
            //        {
            //            var city = _cityRepository.Find(Specification<City>.Eval(x => x.ID == resume.ID));
            //            if (city != null)
            //            {
            //                resumeObject.cityName = city.name;

            //                var province = _provinceRepository.Find(Specification<Province>.Eval(x => x.ID == city.provinceID));
            //                if (province != null)
            //                {
            //                    resumeObject.provinceID = province.ID;
            //                    resumeObject.provinceName = province.name;
            //                }
            //            }
            //        }
            //        return resumeObject;
            //    }
            //    else
            //        return null;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public int UpdateObject(ResumeObject obj)
        {
            //try
            //{
            //    _resumeRepository.Update(new Resume()
            //    {
            //        City_ID = obj.cityID,
            //        University_ID = obj.universityID,
            //        Major = obj.major,
            //        Education = obj.education,
            //        Gender = obj.genderLimit,
            //        Height = obj.height,
            //        Weight = obj.weight,
            //        HealthCertificate = obj.healthCertificate,
            //        IDNumber = obj.idNumber,
            //        Name = obj.name,
            //        Image = obj.image
            //    });
            //    _resumeRepository.Context.Commit();
            //    return 1;
            //}
            //catch
            //{
            //    _resumeRepository.Context.Rollback();
            //    return 0;
            //}
            throw new NotImplementedException();
        }

    }
}
