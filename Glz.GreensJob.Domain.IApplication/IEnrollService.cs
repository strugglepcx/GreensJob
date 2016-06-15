using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using System;
using Glz.Infrastructure.Locking;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IEnrollService : IApplicationServiceContract
    {
        PagedResult<EnrollObject> GetObjectByPaged(int jobSeekerID, int pageIndex);

        int Count(int jobSeekerID, bool status);

        int AddObject(EnrollObject obj);

        int RemoveObject(int id);

        PagedResultModel<EnrollObject> GetList(int jobGroupID, string name, int pageIndex, int pageSize);

        PagedResultModel<EnrollObject> GetEmployeeInfoList(int jobGroupId, int[] showMethod, int pageIndex, int pageSize);

        [Locking(LockingResource.Job)]
        void ApplyJob(ApplyJobRequestParam requestParam);

        [Locking(LockingResource.Job)]
        void CancelApply(CancelApplyRequestParam requestParam);

        [Locking(LockingResource.Job)]
        void ModifyApply(ModifyApplyRequestParam requestParam);

        [Locking(LockingResource.Job)]
        void ConfirmApply(ConfirmApplyRequestParam requestParam);

        [Locking(LockingResource.Job)]
        void Employ(EmployRequestParam requestParam);

        [Locking(LockingResource.Job)]
        void UnEmploy(EmployRequestParam requestParam);

        int EditEnrollDates(int enrollId, IEnumerable<DateTime> enrollDates);

        /// <summary>
        /// 自动取消
        /// </summary>
        /// <param name="enrollId"></param>
        /// <param name="jobSeekerId"></param>
        /// <param name="hour"></param>
        void AutoCancelApply(int enrollId, int jobSeekerId, double hour);

        /// <summary>
        /// 取消，（需要修改参数，继承ILockingJob接口，以提供锁的支持）
        /// </summary>
        /// <param name="enrollId"></param>
        /// <param name="jobSeekerId"></param>
        void CancelApply(int enrollId, int jobSeekerId);
    }
}
