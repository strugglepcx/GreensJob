using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_Resume
    public class Resume : IAggregateRoot<int>
    {
        /// <summary>
		/// 简历Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int City_ID { get; set; }
        /// <summary>
        /// 大学Id
        /// </summary>
        public int University_ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 身高（cm）
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 体重（kg）
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 是否有健康证
        /// </summary>
        public bool HealthCertificate { get; set; }
        /// <summary>
        /// 身份证编号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 经历描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 求职者
        /// </summary>
        public virtual JobSeeker JobSeeker { get; set; }
        public virtual University University { get; set; }
        public virtual OpenCity OpenCity { get; set; }
    }
}