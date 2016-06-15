using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobRecruitDetailService : IApplicationServiceContract
    {
        JobRecruitDetailModel GetObjectByID(int id);

        PagedResultModel<JobRecruitDetailModel> GetDailyRecruitList(GetDailyRecruitRequestParam param);
    }
}
