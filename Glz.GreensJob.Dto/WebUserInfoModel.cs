using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// B端用户信息
    /// </summary>
    public class WebUserInfoModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public int companyName { get; set; }
        /// <summary>
        /// 自动登陆
        /// </summary>
        public bool saveLogin { get; set; }
        /// <summary>
        /// 会话Id
        /// </summary>
        public string sessionId { get; set; }
    }
}
