using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Enums
{
    /// <summary>
    /// 录用动作
    /// </summary>
    public enum EnrollAction
    {
        /// <summary>
        /// 报名
        /// </summary>
        SignUp = 1,
        /// <summary>
        /// 确认
        /// </summary>
        Confirm = 5,
        /// <summary>
        /// 录用
        /// </summary>
        Employ = 10,
        /// <summary>
        /// 修改录用
        /// </summary>
        UpdateEmploy = 20,
        /// <summary>
        /// 不录用
        /// </summary>
        UnEmploy = 30,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 500
    }
}
