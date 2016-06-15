using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class CompanyActionRequestParam: IdentityRequestParam
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        //public int userID { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        //public int companyID { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        public int cityID { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 企业logo
        /// </summary>
        public string companyImage { get; set; }

        /// <summary>
        /// 企业介绍
        /// </summary>
        public string companyIntroduce { get; set; }

        /// <summary>
        /// 企业地址
        /// </summary>
        public string companyAddr { get; set; }
        
        /// <summary>
        /// 营业执照
        /// </summary>
        public string license { get; set; }

        /// <summary>
        /// 人力资源证书
        /// </summary>
        public string certificate { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string companyContact { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string companyTel { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int status { get; set; }
    }
}
