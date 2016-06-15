using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IResumeService: IApplicationServiceContract
    {
        int UpdateObject(ResumeObject obj);

        ResumeObject GetObjectByUserID(int id);
    }
}
