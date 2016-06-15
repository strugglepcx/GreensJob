using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 会员登录
    /// </summary>
    public class LoginActionRequestParam : RequestParamBase
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string userMobileNumber { get; set; }
        /// <summary>
        /// 是否保存登陆状态，缺省一个小时
        /// </summary>
        public bool saveLogin { get; set; }
    }
}