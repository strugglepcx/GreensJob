using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure.Locking;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;
using Redlock.CSharp;

namespace Glz.Infrastructure.InterceptionBehaviors
{
    /// <summary>
    ///  表示用于方法上锁功能的拦截行为。
    /// </summary>
    public class LockingBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// 锁定职位Key前缀
        /// </summary>
        private const string LockingJobPrefix = "greensjob_lock_job_";
        public LockingBehavior()
        {
        }

        /// <summary>
        /// 获取锁定职位Key
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetLockingJobKey(IMethodInvocation input)
        {
            if (input.Arguments != null &&
                input.Arguments.Count > 0)
            {
                for (var i = 0; i < input.Arguments.Count; i++)
                {
                    var lockingJob = input.Arguments[i] as ILockingJob;
                    if (lockingJob != null)
                    {
                        return LockingJobPrefix + lockingJob.jobId;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据指定的<see cref="LockingAttribute"/>以及<see cref="IMethodInvocation"/>实例，
        /// 获取与某一特定参数值相关的键名。
        /// </summary>
        /// <param name="input"><see cref="LockingAttribute"/>实例。</param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var method = input.MethodBase;
            var key = method.Name;
            if (method.IsDefined(typeof(LockingAttribute), false))
            {
                var lockingAttribute =
                    (LockingAttribute)method.GetCustomAttributes(typeof(LockingAttribute), false)[0];
                switch (lockingAttribute.LockingResource)
                {
                    case LockingResource.Job:
                        var isLocked = false;
                        Lock @lock = null;
                        try
                        {
                            var lockingJobKey = GetLockingJobKey(input);
                            if (string.IsNullOrEmpty(lockingJobKey))
                            {
                                return getNext().Invoke(input, getNext);
                            }
                            // 上锁
                            isLocked = LockManager.Instance.Lock(lockingJobKey, out @lock);
                            if (!isLocked)
                            {
                                return new VirtualMethodReturn(input, new LockingJobException("职位锁定异常"));
                            }
                            var methodReturn = getNext().Invoke(input, getNext);
                            // 解锁
                            LockManager.Instance.UnLock(@lock);
                            return methodReturn;
                        }
                        catch (Exception ex)
                        {
                            if (isLocked)
                            {
                                LockManager.Instance.UnLock(@lock);
                            }
                            return new VirtualMethodReturn(input, ex);
                        }
                    default:
                        break;
                }
            }

            return getNext().Invoke(input, getNext);
        }

        /// <summary>
        /// 获取当前行为需要拦截的对象类型接口。
        /// </summary>
        /// <returns>所有需要拦截的对象类型接口。</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示当前拦截行为被调用时，是否真的需要执行
        /// 某些操作。
        /// </summary>
        public bool WillExecute => true;
    }
}
