using System.Collections.Generic;

namespace Glz.Infrastructure
{
    /// <summary>
    /// 分页结果集Model
    /// </summary>
    public class PagedResultModel<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int? TotalRecords { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int? TotalPages { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int? PageNumber { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public IList<T> Data { get; set; }
    }
}
