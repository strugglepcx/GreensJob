using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    /// <summary>
    /// 求职者服务接口
    /// </summary>
    public interface IJobSeekerService : IApplicationServiceContract
    {
        /// <summary>
        /// 是否存在该手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool ExistsByMobile(string mobile);

        /// <summary>
        /// 是否存在该openId
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        bool ExistsByOpenId(string openId);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        MobileUserInfoModel Login(LoginRequestParam requestParam);

        /// <summary>
        /// 绑定手机
        /// </summary>
        /// <param name="requestParam"></param>
        void BindingMobile(BindingMobileRequestParam requestParam);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        MobileUserInfoModel GetUserInfo(GetUserInfoRequestParam requestParam);

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        ConfigurationModel GetConfiguration(GetConfigurationRequestParam requestParam);

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="requestParam"></param>
        void ConfigurationSet(ConfigurationSetRequestParam requestParam);

        /// <summary>
        /// 提现密码设置
        /// </summary>
        /// <param name="requestParam"></param>
        void AccountPwdSet(AccountPwdSetRequestParam requestParam);

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="requestParam"></param>
        void TransferMoneyOut(TransferMoneyOutRequestParam requestParam);

        /// <summary>
        /// 钱包交易记录
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        PagedResultModel<JobSeekerWalletActionLogModel> DspTransactionInfo(
            DspTransactionInfoRequestParam requestParam);

        /// <summary>
        /// 消息列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        PagedResultModel<JobSeekerMessageModel> DspMsgList(
            DspMsgListRequestParam requestParam);

        /// <summary>
        /// 获取简历
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        ResumeObject GetResume(GetResumeRequestParam requestParam);

        /// <summary>
        /// 获取简历（商铺）
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        ResumeObject GetResume(GetResumeBRequestParam requestParam);

        /// <summary>
        /// 设置简历
        /// </summary>
        /// <param name="requestParam"></param>
        void SetResume(SetResumeRequestParam requestParam);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        MobileUserInfoModel Register(RegisterRequestParam requestParam);

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="requestParam"></param>
        void RetrievePassword(RetrievePasswordRequestParam requestParam);

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="requestParam"></param>
        void Logout(LogoutRequestParam requestParam);

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="requestParam"></param>
        void Attention(AttentionRequestParam requestParam);
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="requestParam"></param>
        void CancelAttention(CancelAttentionRequestParam requestParam);
    }
}
