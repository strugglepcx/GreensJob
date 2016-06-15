using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redlock.CSharp;

namespace Glz.Infrastructure.Locking
{
    /// <summary>
    /// 锁操作接口
    /// </summary>
    public interface ILocking
    {
        /// <summary>
        /// 锁
        /// </summary>
        /// <returns></returns>
        bool Lock(string resourceName, out Lock @lock);
        /// <summary>
        /// 解锁
        /// </summary>
        void UnLock(Lock @lock);
    }
}
