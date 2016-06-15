using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Dept : IAggregateRoot<int>
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
    }
}
