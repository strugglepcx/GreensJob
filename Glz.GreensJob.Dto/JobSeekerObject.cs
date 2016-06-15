using System;

namespace Glz.GreensJob.Dto
{
    public class JobSeekerObject
    {
        public int ID { get; set; }
        public string nickName { get; set; }
        public string virtualImage { get; set; }
        public string mobile { get; set; }
        public string wechatToken { get; set; }
        public string payWechatAccount { get; set; }
        public string weiboToken { get; set; }
        public Guid SID { get; set; }
        public DateTime lastLoginDate { get; set; }
        public DateTime createDate { get; set; }
    }
}
