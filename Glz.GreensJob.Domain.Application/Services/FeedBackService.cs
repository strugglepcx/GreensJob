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
    public class FeedBackService : ApplicationService, IFeedBackService
    {
        private readonly IRepository<int, FeedBack> _feedBackRepository;

        public FeedBackService(IRepositoryContext context, IRepository<int, FeedBack> feedBackRepository) : base(context)
        {
            _feedBackRepository = feedBackRepository;
        }

        public int AddObject(FeedBackObject obj)
        {
            try
            {
                var feedBack = new FeedBack()
                {
                    Category = obj.Category,
                    MemberID = obj.MemberID,
                    MemberCategory = obj.MemberCategory,
                    Contact = obj.Contact,
                    Content = obj.Content,
                    Status = 0,
                    CreateDate = DateTime.Now
                };
                _feedBackRepository.Add(feedBack);
                _feedBackRepository.Context.Commit();
                return feedBack.ID;
            }
            catch
            {
                _feedBackRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
