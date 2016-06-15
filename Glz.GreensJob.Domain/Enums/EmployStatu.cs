using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Enums
{
    /// <summary>
    /// 录用状态
    /// </summary>
    public enum EmployStatu
    {
        /// <summary>
        /// 报名
        /// </summary>
        SignUp = 1,
        /// <summary>
        /// 录用未确认
        /// </summary>
        EmployNotConfirmed = 5,
        /// <summary>
        ///  录用
        /// </summary>
        Employ = 10,
        /// <summary>
        ///  不录用
        /// </summary>
        UnEmploy = 11,
        /// <summary>
        ///  付款 
        /// </summary>
        Payed = 20,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 500
    }
}
