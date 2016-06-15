using System;

namespace Glz.GreensJob.Dto
{
    public class ResumeObject
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
        /// 学校名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 大学Id
        /// </summary>
        public int University_ID { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string UniversityName { get; set; }
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
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
    }
}
