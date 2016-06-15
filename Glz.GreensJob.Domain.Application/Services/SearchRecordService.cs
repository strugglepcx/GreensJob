using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class SearchRecordService : ApplicationService, ISearchRecordService
    {
        private readonly IRepository<int, SearchRecord> _searchRecordRepository;
        public SearchRecordService(IRepositoryContext context, IRepository<int, SearchRecord> searchRecordRepository) : base(context)
        {
            _searchRecordRepository = searchRecordRepository;
        }

        public PagedResultModel<SearchRecordModel> PrefetchSearch(PrefetchSearchRequestParam requestParam)
        {
            return
                Mapper.Instance.Map<PagedResultModel<SearchRecordModel>>(
                    _searchRecordRepository.FindAll(
                        Specification<SearchRecord>.Eval(searchRecord => searchRecord.OpenId == requestParam.openId),
                        searchRecord => searchRecord.CreateTime, SortOrder.Descending, requestParam.pageIndex,
                        requestParam.pageSize));
        }
    }
}
