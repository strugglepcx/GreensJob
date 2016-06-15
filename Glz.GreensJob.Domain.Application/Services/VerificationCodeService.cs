using System;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;
using Glz.Infrastructure.Sms;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class VerificationCodeService : ApplicationService, IVerificationCodeService
    {
        private readonly ICache _cache;

        public VerificationCodeService(IRepositoryContext context, ICache cache) : base(context)
        {
            _cache = cache;
        }

        public VerificationCodeModel GetVerifyCode(GetVerifyCodeRequestParam requestParam)
        {
            var cacheKey = Const.ValidateCodeCacheKey + requestParam.platform + "_" + requestParam.userMobileNumber;
            var verificationCodeModel =
                _cache.Get<VerificationCodeModel>(cacheKey);

            if (verificationCodeModel != null && verificationCodeModel.DurationDateTime > DateTime.Now)
                throw new GreensJobException(StatusCodes.Failure, "发送验证码过于频繁");

            //appKey.userMobileNumber
            string code = RandomNumber.GetSixString();
            //long nResult = 1;
#if DEBUG
            long nResult = 1;
#else
            long nResult = SmsHelper.SendValidationCode(requestParam.userMobileNumber, code);
#endif
            if (nResult > 0)
            {
                var result = new VerificationCodeModel
                {
                    verificationCode = code
                };
                result.DurationDateTime = DateTime.Now.AddSeconds(result.validDuration);
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
                //var obj = new VerificationCode()
                //{
                //    verificationCode = code
                //};
                //_verificationCodeService.AddObject(new VerificationCodeObject()
                //{
                //    code = code,
                //    mobile = requestParam.userMobileNumber
                //});
                return result;
            }
            throw new GreensJobException(StatusCodes.Failure, "短信发送失败！");
        }
    }
}
