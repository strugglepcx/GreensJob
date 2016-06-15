using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IAgencyRecruitJobService : IApplicationServiceContract
    {
        AgencyRecruitJobObject GetObjectByID(int id);

        PagedResult<AgencyRecruitJobObject> GetObjectPaged(int pageIndex);

        int AddObject(AgencyRecruitJobObject obj);
    }
}
