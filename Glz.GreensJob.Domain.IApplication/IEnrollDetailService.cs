using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IEnrollDetailService : IApplicationServiceContract
    {
        int AddObject(List<EnrollDetailObject> obj);

        int UpdateObject(EnrollDetailObject obj);

        PagedResult<EnrollDetailObject> GetObjectByPaged(int enrollID);

        int RemoveObjectByParent(int parentID);
    }
}
