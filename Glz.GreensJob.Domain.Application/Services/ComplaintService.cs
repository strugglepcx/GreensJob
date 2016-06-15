using System;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class ComplaintService : ApplicationService, IComplaintService
    {
        private readonly IRepository<int, Complaint> _complaintRepository = null;

        public ComplaintService(IRepositoryContext context, IRepository<int, Complaint> ComplaintRepository) : base(context)
        {
            _complaintRepository = ComplaintRepository;
        }

        public int AddObject(ComplaintObject obj)
        {
            try
            {
                var complaint = new Complaint()
                {
                    jobID = obj.jobID,
                    jobSeekerID = obj.jobSeekerID,
                    category = obj.category,
                    content = obj.content,
                    createDate = obj.createDate
                };
                _complaintRepository.Add(complaint);
                _complaintRepository.Context.Commit();
                return complaint.ID;
            }
            catch (Exception ex)
            {
                _complaintRepository.Context.Rollback();
                return 0;
            }
        }

        public ComplaintObject GetObjectByID(int id)
        {
            try
            {
                var complaint = _complaintRepository.Find(Specification<Complaint>.Eval(x => x.ID == id));
                var complaintObject = Mapper.Map<ComplaintObject>(complaint);
                return complaintObject;
            }
            catch
            {
                return null;
            }
        }

        public PagedResult<ComplaintObject> GetObjectByPaged(int pageIndex)
        {
            try
            {
                return null;
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
                _complaintRepository.Remove(new Complaint() { ID = id });
                _complaintRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _complaintRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
