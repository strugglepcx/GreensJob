using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IJobQueryService
    {
        [Caching(CachingMethod.Get)]
        PagedResultModel<GetJobsModel> GetJobsOrderByRelaseDate(GetJobsOrderByRelaseDateRequestParam requestParam);
    }
}
