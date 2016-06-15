namespace Glz.Infrastructure.Locking
{
    /// <summary>
    /// 锁定职位接口
    /// </summary>
    public interface ILockingJob
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        int jobId { get; set; }
    }
}