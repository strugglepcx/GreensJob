using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Redlock.CSharp;

namespace Glz.Infrastructure.Locking
{
    public class LockManager : ILocking
    {
        private readonly ILocking _locking;
        private static readonly LockManager instance = new LockManager();

        private LockManager()
        {
            _locking = ServiceLocator.Instance.GetService<ILocking>();
        }
        /// <summary>
        /// 获取<c>LockManager</c>类型的单件（Singleton）实例。
        /// </summary>
        public static LockManager Instance
        {
            get { return instance; }
        }

        public bool Lock(string resourceName, out Lock @lock)
        {
            return _locking.Lock(resourceName, out @lock);
        }

        public void UnLock(Lock @lock)
        {
            _locking.UnLock(@lock);
        }
    }
}
