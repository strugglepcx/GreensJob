using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    /// <summary>
    /// 验证码服务接口定义
    /// </summary>
    public interface IVerificationCodeService : IApplicationServiceContract
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        VerificationCodeModel GetVerifyCode(GetVerifyCodeRequestParam requestParam);
    }
}
