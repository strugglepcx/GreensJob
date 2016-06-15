using System;
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
    public class UniversityService : ApplicationService, IUniversityService
    {
        private readonly IRepository<int, University> _universityRepository;

        public UniversityService(IRepositoryContext context, IRepository<int, University> UniversityRepository) : base(context)
        {
            _universityRepository = UniversityRepository;
        }

        public int AddObject(UniversityObject obj)
        {
            try
            {
                var university = new University()
                {
                    cityID = obj.cityID,
                    name = obj.name
                };
                _universityRepository.Add(university);
                _universityRepository.Context.Commit();
                return university.ID;
            }
            catch
            {
                _universityRepository.Context.Rollback();
                return 0;
            }
        }

        public UniversityObject GetObjectByID(int id)
        {
            try
            {
                var university = _universityRepository.Find(Specification<University>.Eval(x => x.ID == id));
                var universityObject = AutoMapper.Mapper.Map<UniversityObject>(university);
                return universityObject;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<UniversityObject> GetUniversitys(GetUniversitysRequestParam requestParam)
        {
            return
                Mapper.Instance.Map<IEnumerable<UniversityObject>>(
                    _universityRepository.FindAll(Specification<University>.Eval(x => x.cityID == requestParam.cityId)));
        }

        public int RemoveObject(int id)
        {
            try
            {
                _universityRepository.Add(new University() { ID = id });
                _universityRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _universityRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(UniversityObject obj)
        {
            try
            {
                _universityRepository.Add(new University()
                {
                    ID = obj.ID,
                    cityID = obj.cityID,
                    name = obj.name
                });
                _universityRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _universityRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
