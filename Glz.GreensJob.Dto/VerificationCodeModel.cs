using System;
using Newtonsoft.Json;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 验证码对象
    /// </summary>
    public class VerificationCodeModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { get; set; }

        /// <summary>
        /// 有效性期
        /// </summary>
        public int validDuration { get; } = 120;
        /// <summary>
        /// 间隔到期时间
        /// </summary>
        [JsonIgnore]
        public DateTime DurationDateTime { get; set; }
    }
}