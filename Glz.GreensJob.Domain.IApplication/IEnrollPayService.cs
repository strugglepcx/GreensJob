using System.Collections.Generic;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IEnrollPayService : IApplicationServiceContract
    {
        int AddObject(EnrollPayRequestParam requestParam, string orderId, string chargeId);

        int UpdateObject(PayCallBackRequestParam requestParam);

        void ImportDetail(ImportPayDetailRequestParam requestParam);

        PagedResultModel<EnrollPayDetailModel> GetPayDetail(GetPayDetailRequestParam requestParam);

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="requestParam"></param>
        string Payment(PaymentRequestParam requestParam);

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="requestParam"></param>
        void PaymentSuccess(PaymentSuccessRequestParam requestParam);

        /// <summary>
        /// 获取成功支付列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        PagedResultModel<EnrollPayDetailModel> GetSuccessPayDetail(GetSuccessPayDetailRequestParam requestParam);

        /// <summary>
        /// 获取申请列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        IEnumerable<ExtractApplyModel> GetExtractApplys(GetExtractApplysRequestParam requestParam);

        /// <summary>
        /// 完成申请列表
        /// </summary>
        /// <param name="requestParam"></param>
        void CompleteExtractApplys(CompleteExtractApplysRequestParam requestParam);
    }
}
