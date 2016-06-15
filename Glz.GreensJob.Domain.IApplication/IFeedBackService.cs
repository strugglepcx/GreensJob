using Glz.Infrastructure;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IFeedBackService : IApplicationServiceContract
    {
        int AddObject(FeedBackObject obj);
    }
}
