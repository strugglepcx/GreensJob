using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.GreensJob.Domain.Enums;
using Glz.Infrastructure;
using Newtonsoft.Json;
using NPOI.Extension;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class EnrollPayService : ApplicationService, IEnrollPayService
    {
        private readonly IRepository<int, EnrollPay> _enrollPayRepository;
        private readonly IRepository<int, EnrollPayDetail> _enrollPayDetailRepository;
        private readonly IRepository<int, JobSeeker> _jobSeekerRepository;
        private readonly IRepository<int, Job> _jobRepository;
        private readonly IRepository<int, JobGroup> _jobGroupRepository;
        private readonly IRepository<int, Enroll> _enrollRepository;
        private readonly IRepository<int, JobSeekerWalletActionLog> _jobSeekerWalletActionLogrRepository;
        private readonly IRepository<int, ExtractApply> _extractApplyrRepository;

        public EnrollPayService(IRepositoryContext context, IRepository<int, EnrollPay> enrollPayRepository,
            IRepository<int, EnrollPayDetail> enrollPayDetailRepository, IRepository<int, JobSeeker> jobSeekerRepository,
            IRepository<int, Job> jobRepository, IRepository<int, JobGroup> jobGroupRepository,
            IRepository<int, Enroll> enrollRepository,
            IRepository<int, JobSeekerWalletActionLog> jobSeekerWalletActionLogrRepository,
            IRepository<int, ExtractApply> extractApplyrRepository)
            : base(context)
        {
            _enrollPayRepository = enrollPayRepository;
            _enrollPayDetailRepository = enrollPayDetailRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _jobRepository = jobRepository;
            _jobGroupRepository = jobGroupRepository;
            _enrollRepository = enrollRepository;
            _jobSeekerWalletActionLogrRepository = jobSeekerWalletActionLogrRepository;
            _extractApplyrRepository = extractApplyrRepository;
        }

        /// <summary>
        /// 获取新的订单号
        /// </summary>
        /// <returns></returns>
        private string NewOrderId()
        {
            return "P" + DateTime.Now.ToString("yyyyMMddhhmmssfffff") + Guid.NewGuid().ToString().Substring(0, 8);
        }

        private string GetChannelByPayType(int payType)
        {
            switch (payType)
            {
                case 1:
                    return "alipay_pc_direct";
                case 2:
                    return "wx_pub_qr";
                default:
                    return string.Empty;
            }
        }

        private int GetPayTypeByChannel(string channel)
        {
            switch (channel)
            {
                case "alipay_pc_direct":
                    return 1;
                case "wx_pub_qr":
                    return 2;
                default:
                    return -1;
            }
        }

        private string GetPayTypeNameByPayType(int payType)
        {
            switch (payType)
            {
                case 1:
                    return "支付宝";
                case 2:
                    return "微信";
                default:
                    return string.Empty;
            }
        }

        private string GetPayTypeNameByChannel(string channel)
        {
            switch (channel)
            {
                case "alipay_pc_direct":
                    return "支付宝";
                case "wx_pub_qr":
                    return "微信";
                default:
                    return string.Empty;
            }
        }

        public int AddObject(EnrollPayRequestParam requestParam, string orderId, string chargeId)
        {
            try
            {
                var enrollPay = new EnrollPay()
                {
                    Company_ID = requestParam.companyId,
                    Publisher_ID = requestParam.userId,
                    PayAmount = requestParam.total,
                    PayType = 1,
                    PaySn = chargeId,
                    orderID = orderId,
                    PayResult = 0,
                    CreateTime = DateTime.Now
                };
                _enrollPayRepository.Add(enrollPay);
                _enrollPayRepository.Context.Commit();

                foreach (PayDetail detail in requestParam.Details)
                {
                    var obj =
                        _enrollPayDetailRepository.Find(Specification<EnrollPayDetail>.Eval(x => x.ID == detail.id));
                    obj.AmountSalary = detail.salary;
                    obj.EnrollPay_ID = enrollPay.ID;
                    obj.State = EnrollPayState.Paid; // 标记为要结算

                    _enrollPayDetailRepository.Update(obj);
                    // 转入 求职者钱包 逻辑 待Update操作中如果是成功支付 则修改
                }
                _enrollPayDetailRepository.Context.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(PayCallBackRequestParam requestParam)
        {
            try
            {
                var enrollPay =
                    _enrollPayRepository.Find(Specification<EnrollPay>.Eval(x => x.orderID == requestParam.orderId));
                enrollPay.PayResult = PayResultState.Sucess;
                _enrollPayRepository.Update(enrollPay);
                Context.Commit();
                // 修改 求职者钱包
                return 1;
            }
            catch (Exception ex)
            {
                Context.Rollback();
                return 0;
            }
        }

        public PagedResult<EnrollPayDetailModel> ImportDetail(DataSet ds, int companyId, int userId, int jobId)
        {
            try
            {
                if (ds.Tables.Count > 0)
                {
                    #region 导入

                    foreach (DataTable table in ds.Tables)
                    {
                        int id = 0;
                        try
                        {
                            string tableName = table.TableName;
                            if (tableName.IndexOf('_') != -1)
                            {
                                id = Convert.ToInt32(tableName.Substring(tableName.IndexOf('_') + 1));
                            }
                        }
                        catch
                        {
                            id = 0;
                        }
                        if (id == 0)
                            continue;
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (string.IsNullOrEmpty(row[2].ToString().Trim(' ')))
                                    continue;
                                // 根据导入的手机号，查询相关的求职者信息
                                var jobSeeker =
                                    _jobSeekerRepository.Find(
                                        Specification<JobSeeker>.Eval(x => x.mobile == row[2].ToString()));
                                // 支付明细
                                EnrollPayDetail payDetail = null;
                                if (jobSeeker != null)
                                    payDetail =
                                        _enrollPayDetailRepository.Find(
                                            Specification<EnrollPayDetail>.Eval(
                                                x => x.JobSeeker_ID == jobSeeker.ID && x.Job_ID == id));
                                else
                                    payDetail =
                                        _enrollPayDetailRepository.Find(
                                            Specification<EnrollPayDetail>.Eval(
                                                x => x.UserMobile == row[2].ToString() && x.Job_ID == id));
                                if (payDetail == null)
                                {
                                    _enrollPayDetailRepository.Add(new EnrollPayDetail()
                                    {
                                        Job_ID = id,
                                        Publisher_ID = userId,
                                        Company_ID = companyId,
                                        JobSeeker_ID = jobSeeker == null ? 0 : jobSeeker.ID,
                                        State = 0,
                                        EnrollPay_ID = 0,
                                        AmountSalary = Convert.ToDecimal(row[4]), //支付金额
                                        UserName = row[1].ToString(), //姓名
                                        UserMobile = row[2].ToString(), //手机号
                                        JobName = "",
                                        Enroll_ID = 0,
                                        DaySalary = 0,
                                        WorkDays = 0,
                                        BankCardNo = row[3].ToString(), //收款账户
                                        CreateTime = DateTime.Now,
                                        PayTime = DateTime.Now
                                    });
                                }
                            }
                        }
                        else
                            continue;
                    }

                    #endregion

                    Context.Commit();
                    var result =
                        _enrollPayDetailRepository.FindAll(
                            Specification<EnrollPayDetail>.Eval(
                                x => x.Job_ID == jobId && x.Publisher_ID == userId && x.State == 0), x => x.CreateTime,
                            SortOrder.Descending, 1, 1000);

                    return Mapper.Instance.Map<PagedResult<EnrollPayDetailModel>>(result);
                }
                else
                {
                    return null; //无数据
                }
            }
            catch (Exception ex)
            {
                Context.Rollback();
                return null;
            }
        }

        public void ImportDetail(ImportPayDetailRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (!File.Exists(requestParam.FilePath))
            {
                throw new GreensJobException(StatusCodes.Failure, "导入文件不存在");
            }
            var importDatas = Excel.Load<PayeeModel>(requestParam.FilePath);
            if (!importDatas.Any())
            {
                throw new GreensJobException(StatusCodes.Failure, "导入文件中不存在付款记录");
            }
            var nowTime = DateTime.Now;
            var jobGroup = _jobGroupRepository.GetByKey(requestParam.jobGroupId);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");

            var errorInfo = string.Empty;
            foreach (var importData in importDatas)
            {
                var tmpErrorInfo = string.Empty;
                if (string.IsNullOrWhiteSpace(importData.Mobile))
                {
                    tmpErrorInfo += $"序号:{importData.SerialNumber} 手机号为空" + Environment.NewLine;
                }
                if (string.IsNullOrWhiteSpace(importData.UserName))
                {
                    tmpErrorInfo += $"序号:{importData.SerialNumber} 用户名为空" + Environment.NewLine;
                }
                if (!string.IsNullOrWhiteSpace(tmpErrorInfo))
                {
                    errorInfo += tmpErrorInfo;
                    continue;
                }
                var queryEnrollPayDetail =
                    _enrollPayDetailRepository.Find(
                        Specification<EnrollPayDetail>.Eval(
                            x => x.UserMobile == importData.Mobile && x.JobGroup_ID == requestParam.jobGroupId &&
                                 x.Job_ID == importData.jobId.GetIntOrDefault() &&
                                 x.JobGroup_ID == requestParam.jobGroupId &&
                                 (x.State == EnrollPayState.Untreated || x.State == EnrollPayState.Processed)));
                if (queryEnrollPayDetail == null)
                {
                    tmpErrorInfo += $"序号:{importData.SerialNumber} 没有支付明细记录对应" + Environment.NewLine;
                    //_enrollPayDetailRepository.Add(new EnrollPayDetail
                    //{
                    //    AmountSalary = importData.PaymentTotalAmount.GetDecimalOrDefault(),
                    //    BankCardNo = "",
                    //    Company_ID = jobGroup.companyID,
                    //    CreateTime = nowTime,
                    //    DaySalary = 0,
                    //    BasePay = importData.BasePayAmount.GetDecimalOrDefault(),
                    //    Bonus = importData.BonusAmount.GetDecimalOrDefault(),
                    //    Debit = importData.DebitAmount.GetDecimalOrDefault(),
                    //    EnrollPay_ID = null,
                    //    Enroll_ID = 0,
                    //    JobName = jobGroup.name,
                    //    JobSeeker_ID = 0,
                    //    Job_ID = jobGroup.ID,
                    //    JobGroup_ID = jobGroup.ID,
                    //    Publisher_ID = null,
                    //    State = EnrollPayState.Processed,
                    //    UserMobile = importData.Mobile,
                    //    UserName = importData.UserName,
                    //    StartDate = importData.StartDate.GetDateTimeOrDefault(),
                    //    EndDate = importData.EndDate.GetDateTimeOrDefault(),
                    //    WorkDays = importData.WorkDays.GetIntOrDefault()
                    //});
                }
                else
                {
                    queryEnrollPayDetail.AmountSalary = importData.PaymentTotalAmount.GetDecimalOrDefault();
                    queryEnrollPayDetail.BasePay = importData.BasePayAmount.GetDecimalOrDefault();
                    queryEnrollPayDetail.Bonus = importData.BonusAmount.GetDecimalOrDefault();
                    queryEnrollPayDetail.Debit = importData.DebitAmount.GetDecimalOrDefault();
                    queryEnrollPayDetail.StartDate = importData.StartDate.GetDateTimeOrDefault();
                    queryEnrollPayDetail.EndDate = importData.EndDate.GetDateTimeOrDefault();
                    queryEnrollPayDetail.WorkDays = importData.WorkDays.GetIntOrDefault();
                    queryEnrollPayDetail.UserName = importData.UserName;

                    _enrollPayDetailRepository.Update(queryEnrollPayDetail);
                }
            }
            if (!string.IsNullOrWhiteSpace(errorInfo))
            {
                throw new GreensJobException(StatusCodes.Failure, errorInfo);
            }
            Context.Commit();
        }

        public PagedResultModel<EnrollPayDetailModel> GetPayDetail(GetPayDetailRequestParam requestParam)
        {
            var result = _enrollPayDetailRepository.FindAll(
                Specification<EnrollPayDetail>.Eval(
                    x => x.JobGroup_ID == requestParam.jobGroupId &&
                         (x.State == EnrollPayState.Untreated || x.State == EnrollPayState.Processed)),
                x => x.CreateTime, SortOrder.Descending,
                requestParam.pageIndex,
                requestParam.pageSize);

            return
                Mapper.Instance.Map<PagedResultModel<EnrollPayDetailModel>>(result);
        }

        public string Payment(PaymentRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.selectedEnrollPayItems == null || !requestParam.selectedEnrollPayItems.Any())
                throw new GreensJobException(StatusCodes.Failure, "请选择需要支付的明细信息");

            var nowTime = DateTime.Now;
            var enrollPayDetails = new List<EnrollPayDetail>();

            var jobGroup = _jobGroupRepository.GetByKey(requestParam.jobGroupId);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");

            foreach (var selectedEnrollPayItem in requestParam.selectedEnrollPayItems)
            {
                var enrollPayDetail = _enrollPayDetailRepository.GetByKey(selectedEnrollPayItem.ID);

                enrollPayDetail.AmountSalary = selectedEnrollPayItem.AmountSalary;
                enrollPayDetail.BasePay = selectedEnrollPayItem.BasePay;
                enrollPayDetail.Bonus = selectedEnrollPayItem.Bonus;
                enrollPayDetail.Debit = selectedEnrollPayItem.Debit;

                if (enrollPayDetail == null)
                {
                    throw new GreensJobException(StatusCodes.Failure, "支付明细数据异常");
                }
                if (enrollPayDetail.State != EnrollPayState.Processed)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"[{enrollPayDetail.UserMobile}]支付明细数据状态异常");
                }
                if (enrollPayDetail.AmountSalary <= 0)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"[{enrollPayDetail.UserMobile}]支付金额必须大于0");
                }
                if (enrollPayDetail.AmountSalary !=
                    enrollPayDetail.BasePay + enrollPayDetail.Bonus - enrollPayDetail.Debit)
                {
                    throw new GreensJobException(StatusCodes.Failure,
                        $"[{enrollPayDetail.UserMobile}]支付金额不等于 基本工资+奖金-扣款");
                }
                var enroll = _enrollRepository.GetByKey(enrollPayDetail.Enroll_ID);
                if (enroll == null)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"[{enrollPayDetail.UserMobile}]报名数据异常");
                }
                if (!(enroll.employStatus == EmployStatu.Employ || enroll.employStatus == EmployStatu.Payed))
                {
                    throw new GreensJobException(StatusCodes.Failure, $"[{enrollPayDetail.UserMobile}]报名数据状态异常");
                }
                enrollPayDetails.Add(enrollPayDetail);
            }

            var channel = GetChannelByPayType(requestParam.payType);

            // 构造订单数据
            var enrollPay = new EnrollPay
            {
                Company_ID = requestParam.companyId,
                CreateTime = nowTime,
                orderID = NewOrderId(),
                PayAmount = enrollPayDetails.Sum(x => x.AmountSalary),
                PayResult = PayResultState.BeingPaid,
                PayType = requestParam.payType,
                Publisher_ID = requestParam.userId
            };

            var charge = Pingxx.CreateInstance()
                .CreateCharge(enrollPay.orderID, enrollPay.PayAmount, channel, jobGroup.name,
                    $"{jobGroup.name} 共计[{enrollPayDetails.Count}]人");
            if (charge == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "支付异常，请联系客服");
            }
            enrollPay.PaySn = charge.Id;
            _enrollPayRepository.Add(enrollPay);
            foreach (var enrollPayDetail in enrollPayDetails)
            {
                enrollPayDetail.EnrollPay = enrollPay;
                _enrollPayDetailRepository.Update(enrollPayDetail);
            }
            Context.Commit();

            return JsonConvert.SerializeObject(charge);
        }

        public void PaymentSuccess(PaymentSuccessRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.data == null)
                throw new ArgumentNullException(nameof(requestParam.data));
            if (requestParam.data.@object == null)
                throw new ArgumentNullException(nameof(requestParam.data.@object));
            var enrollPay =
                _enrollPayRepository.FindAll(x => x.EnrollPayDetails).FirstOrDefault(
                    x => x.PaySn == requestParam.data.@object.id && x.orderID == requestParam.data.@object.order_no)
                ;
            if (enrollPay == null)
            {
                throw new GreensJobServiceErrorException("订单数据异常");
            }
            if (enrollPay.PayResult == PayResultState.Fail || enrollPay.PayResult == PayResultState.Sucess)
            {
                throw new GreensJobServiceErrorException("订单状态异常");
            }
            if (enrollPay.EnrollPayDetails.Count == 0)
            {
                throw new GreensJobServiceErrorException("订单明细数据异常");
            }

            if (!requestParam.data.@object.paid)
            {
                enrollPay.PayResult = PayResultState.Fail;
                _enrollPayRepository.Update(enrollPay);
                Context.Commit();
            }
            var nowTime = DateTime.Now;
            var successEnrollPayDetails = new List<EnrollPayDetail>();
            foreach (var enrollPayDetail in enrollPay.EnrollPayDetails)
            {
                //TODO: 发布者钱包操作

                // 求职者钱包操作
                var queryJobSeeker =
                    _jobSeekerRepository.Find(
                        Specification<JobSeeker>.Eval(jobSeeker => jobSeeker.ID == enrollPayDetail.JobSeeker_ID));
                if (queryJobSeeker == null)
                {
                    throw new GreensJobServiceErrorException("请职者数据有误");
                }
                var job = _jobRepository.GetByKey(enrollPayDetail.Job_ID);
                if (job == null)
                {
                    throw new GreensJobServiceErrorException("未能找到该职位信息");
                }
                var enroll = _enrollRepository.GetByKey(enrollPayDetail.Enroll_ID);
                if (enroll == null)
                {
                    throw new GreensJobServiceErrorException("该录用信息不存在");
                }
                queryJobSeeker.JobSeekerWallet.ActualAmounts += enrollPayDetail.AmountSalary;
                queryJobSeeker.JobSeekerWallet.TotalAmounts += enrollPayDetail.AmountSalary;
                queryJobSeeker.JobSeekerWallet.LastUpdateAmounts += enrollPayDetail.AmountSalary;
                queryJobSeeker.JobSeekerWallet.LastUpdateTime = nowTime;
                _jobSeekerRepository.Update(queryJobSeeker);

                _jobSeekerWalletActionLogrRepository.Add(new JobSeekerWalletActionLog
                {
                    ActionID = WalletAction.Payroll,
                    ActionName = "工资发放",
                    Amount = enrollPayDetail.AmountSalary,
                    BankCardNo = "",
                    CreateTime = nowTime,
                    Enroll_ID = enrollPayDetail.Enroll_ID,
                    Job_ID = enrollPayDetail.Job_ID,
                    JobGroup_ID = enrollPayDetail.JobGroup_ID,
                    PaySn = requestParam.id,
                    PayType = GetPayTypeByChannel(requestParam.data.@object.channel),
                    PayTypeName = GetPayTypeNameByChannel(requestParam.data.@object.channel),
                    State = 1,
                    UserName = queryJobSeeker.mobile,
                    JobSeekerWallet_ID = queryJobSeeker.JobSeekerWallet.ID,
                    OpenCity_ID = job.City_ID,
                    JobName = job.addr,
                    JobGroupName = job.name
                });
                // 求职者录用状态操作
                if (enroll.employStatus == EmployStatu.Employ)
                {
                    enroll.employStatus = EmployStatu.Payed;
                    _enrollRepository.Update(enroll);
                }

                // 更新支付明细状态
                enrollPayDetail.State = EnrollPayState.Paid;
                enrollPayDetail.PayTime = nowTime;
                var successEnrollPayDetail = Mapper.Instance.Map<EnrollPayDetail>(enrollPayDetail);
                successEnrollPayDetail.State = EnrollPayState.Processed;
                successEnrollPayDetail.AmountSalary = 0;
                successEnrollPayDetail.BasePay = 0;
                successEnrollPayDetail.Bonus = 0;
                successEnrollPayDetail.Debit = 0;
                successEnrollPayDetails.Add(successEnrollPayDetail);
            }

            foreach (var successEnrollPayDetail in successEnrollPayDetails)
            {
                enrollPay.EnrollPayDetails.Add(successEnrollPayDetail);
            }

            // 订单操作
            enrollPay.PayResult = PayResultState.Sucess;
            _enrollPayRepository.Update(enrollPay);
            Context.Commit();
        }

        public PagedResultModel<EnrollPayDetailModel> GetSuccessPayDetail(GetSuccessPayDetailRequestParam requestParam)
        {
            var result = _enrollPayDetailRepository.FindAll(
                Specification<EnrollPayDetail>.Eval(
                    x => ((string.IsNullOrEmpty(requestParam.keyword) && x.State == EnrollPayState.Paid && x.Company_ID == requestParam.companyId) || (x.JobGroupName.Contains(requestParam.keyword) &&
                         x.State == EnrollPayState.Paid && x.Company_ID == requestParam.companyId))),
                    x => x.CreateTime,
                    SortOrder.Descending,
                    requestParam.pageIndex,
                    requestParam.pageSize);

            return
                Mapper.Instance.Map<PagedResultModel<EnrollPayDetailModel>>(result);
        }

        public IEnumerable<ExtractApplyModel> GetExtractApplys(GetExtractApplysRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            var extractApplys = _extractApplyrRepository.FindAll(Specification<ExtractApply>.Eval(x => x.State == 0)).OrderByDescending(x => x.CreateTime);
            return Mapper.Instance.Map<IEnumerable<ExtractApplyModel>>(extractApplys);
        }

        public void CompleteExtractApplys(CompleteExtractApplysRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.ExtractApplyIdList == null || !requestParam.ExtractApplyIdList.Any())
                throw new ArgumentNullException(nameof(requestParam.ExtractApplyIdList));
            foreach (var extractApplyId in requestParam.ExtractApplyIdList)
            {
                var extractApply = _extractApplyrRepository.GetByKey(extractApplyId);
                if (extractApply.State == 1)
                {
                    throw new GreensJobException(StatusCodes.Failure, "申请状态异常");
                }
                var jobSeekerWalletActionLog =
                    _jobSeekerWalletActionLogrRepository.Find(
                        Specification<JobSeekerWalletActionLog>.Eval(x => x.ExtractApply_ID == extractApplyId));
                if (jobSeekerWalletActionLog == null)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"{extractApply.JobSeeker.mobile}数据异常");
                }
                jobSeekerWalletActionLog.State = 1;
                _jobSeekerWalletActionLogrRepository.Update(jobSeekerWalletActionLog);

                extractApply.State = 1;
                extractApply.JobSeeker.JobSeekerWallet.TotalAmounts =
                    extractApply.JobSeeker.JobSeekerWallet.TotalAmounts -
                    extractApply.JobSeeker.JobSeekerWallet.FrozenAmounts;
                extractApply.JobSeeker.JobSeekerWallet.FrozenAmounts = 0;
                _extractApplyrRepository.Update(extractApply);

                Context.Commit();
            }
        }
    }
}