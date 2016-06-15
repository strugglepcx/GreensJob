using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobDraftService: IApplicationServiceContract
    {
        JobDraftObject GetObjectByID(int id);
        int AddObject(AddJobDraftRequestParam param);
        PagedResultModel<JobDraftObject> GetObjectByPaged(GetJobDraftListRequestParam parm);
        int RemoveObject(int id);

    }
}
