namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 用户信息模型
    /// </summary>
    public class MobileUserInfoModel
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
        /// 性别
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// 钱包余额
        /// </summary>
        public decimal walletBalance { get; set; }
        /// <summary>
        /// 提现状态
        /// </summary>
        public int presentState { get; set; }
        /// <summary>
        /// 是否有录用信息未读取
        /// </summary>
        public int haveNotReadEnrollInfo { get; set; }
        /// <summary>
        /// 是否有录用信息未读取
        /// </summary>
        public int haveNotReadAccountInfo { get; set; }
        /// <summary>
        /// 是否绑定
        /// </summary>
        public int isBinding { get; set; }
        /// <summary>
        /// 飞单次数
        /// </summary>
        public int giveUpCount { get; set; }
        /// <summary>
        /// 会话Id
        /// </summary>
        public string sessionId { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 是否设置了账号密码
        /// </summary>
        public int isSetPassword { get; set; }
        /// <summary>
        /// 是否设置了支付微信账号
        /// </summary>
        public int isSetPayWechatAccount { get; set; }
        /// <summary>
        /// 是否相同支付微信账号
        /// </summary>
        public int isSameWechatAccount { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 支付微信账号
        /// </summary>
        public string payWechatAccount { get; set; }
    }
}