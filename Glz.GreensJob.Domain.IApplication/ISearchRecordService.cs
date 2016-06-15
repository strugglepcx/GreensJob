using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface ISearchRecordService : IApplicationServiceContract
    {
        PagedResultModel<SearchRecordModel> PrefetchSearch(PrefetchSearchRequestParam requestParam);
    }
}
