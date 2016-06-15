using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Locking
{
    /// <summary>
    /// 锁定职位异常
    /// </summary>
    public class LockingJobException : Exception
    {
        public LockingJobException(string message)
            : base(message)
        {

        }
    }
}
