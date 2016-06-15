using System.Threading;
using Glz.Infrastructure.Properties;

namespace Glz.Infrastructure
{
    public class ResourceExtend
    {
        /// <summary>
        /// 根据资源名称获取资源配置的值
        /// </summary>
        /// <param Name="name">资源名称</param>
        /// <returns>资源值</returns>
        public static string GetResourceForName(string name)
        {
            return Resources.ResourceManager.GetString(name, Thread.CurrentThread.CurrentCulture);
        }
    }
}