using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetJobInfoRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 1 全部（点击“职位管理”）2 草稿 3 待审核 4 招聘/录用 5 过期
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 搜索职位名
        /// </summary>
        public string keyWordJobName { get; set; }
    }
}
