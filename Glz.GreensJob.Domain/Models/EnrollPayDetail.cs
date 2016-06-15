using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    //T_EnrollPayDetail
    public class EnrollPayDetail : IAggregateRoot<int>
    {

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// EnrollPay_ID
        /// </summary>
        public int? EnrollPay_ID { get; set; }
        /// <summary>
        /// 录用记录Id
        /// </summary>
        public int Enroll_ID { get; set; }
        /// <summary>
        /// 求职者Id
        /// </summary>
        public int JobSeeker_ID { get; set; }
        /// <summary>
        /// 求职者用户名（手机号）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 求职者姓名
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// 职位组Id
        /// </summary>
        public int JobGroup_ID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 工作开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 工作结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 工作天数
        /// </summary>
        public int WorkDays { get; set; }
        /// <summary>
        /// 付款总金额
        /// </summary>
        public decimal AmountSalary { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasePay { get; set; }
        /// <summary>
        /// 奖金
        /// </summary>
        public decimal Bonus { get; set; }
        /// <summary>
        /// 扣款
        /// </summary>
        public decimal Debit { get; set; }
        /// <summary>
        /// 日薪
        /// </summary>
        public decimal DaySalary { get; set; }
        /// <summary>
        /// 发布者Id
        /// </summary>
        public int? Publisher_ID { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public int Company_ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EnrollPayState State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 职位组名称
        /// </summary>
        public string JobGroupName { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 职位支付记录
        /// </summary>
        public virtual EnrollPay EnrollPay { get; set; }
    }
}