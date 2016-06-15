using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface ICollectService : IApplicationServiceContract
    {
        PagedResult<CollectObject> GetObjectByPaged(int jobSeekerID, int pageIndex);

        int Count(int jobSeekerID, bool status);

        int AddObject(CollectObject obj);

        int RemoveObject(int id);

        void CancelCollect(CancelCollectRequestParam requestParam);

        void FavoriteJob(FavoriteJobRequestParam requestParam);
    }
}
