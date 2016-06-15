using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Locking
{
    /// <summary>
    /// 表示由此特性所描述的方法，能够获得基础结构层所提供的锁功能。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class LockingAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个新的<c>LockingAttribute</c>类型。
        /// </summary>
        /// <param name="lockingResource">锁定资源类型</param>
        public LockingAttribute(LockingResource lockingResource)
        {
            LockingResource = lockingResource;
        }

        /// <summary>
        /// 获取或设置锁定资源。
        /// </summary>
        public LockingResource LockingResource { get; set; }
    }
}
