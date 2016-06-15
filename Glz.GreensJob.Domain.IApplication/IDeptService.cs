using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IDeptService : IApplicationServiceContract
    {
        DeptObject GetObjectByID(int id);

        void AddObject(DeptObject dept);

        void RemoveObject(int id);

        void UptObject(DeptObject dept);
    }
}
