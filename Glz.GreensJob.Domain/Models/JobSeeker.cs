using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 求职者
    /// </summary>
    public class JobSeeker : IAggregateRoot<int>
    {
        /// <summary>
        /// 创建一个 <c>JobSeeker</c> 类型实例。
        /// </summary>
        public JobSeeker()
        {
            Collects = new HashSet<Collect>();
            ExtractApplys = new HashSet<ExtractApply>();
            JobSeekerMessages = new HashSet<JobSeekerMessage>();
        }
        public int ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string virtualImage { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 微信凭证
        /// </summary>
        public string wechatToken { get; set; }
        /// <summary>
        /// 支付微信账号
        /// </summary>
        public string payWechatAccount { get; set; }
        /// <summary>
        /// 微博凭证
        /// </summary>
        public string weiboToken { get; set; }
        /// <summary>
        /// SID
        /// </summary>
        public Guid SID { get; set; }
        /// <summary>
        /// 邀请人Id
        /// </summary>
        public int? invitation { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime lastLoginDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// baidu推送管道Id
        /// </summary>
        public string channelId { get; set; }
        /// <summary>
        /// 收藏列表
        /// </summary>
        public virtual ICollection<Collect> Collects { get; set; }
        /// <summary>
        /// 求职者配置
        /// </summary>
        public virtual JobSeekerConfig JobSeekerConfig { get; set; }
        /// <summary>
        /// 求职者钱包
        /// </summary>
        public virtual JobSeekerWallet JobSeekerWallet { get; set; }
        /// <summary>
        /// 简历
        /// </summary>
        public virtual Resume Resume { get; set; }
        /// <summary>
        /// 提现申请列表
        /// </summary>
        public virtual ICollection<ExtractApply> ExtractApplys { get; set; }
        /// <summary>
        /// 消息列表
        /// </summary>
        public virtual ICollection<JobSeekerMessage> JobSeekerMessages { get; set; }
        /// <summary>
        /// 关注公司列表
        /// </summary>
        public virtual ICollection<Company> Companys { get; set; }
    }
}
