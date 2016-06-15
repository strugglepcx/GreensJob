using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Glz.GreensJob.Mobile.MessageHandlers.CustomMessageHandler;
using Glz.Infrastructure;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using Glz.GreensJob.Mobile.Models;

namespace Glz.GreensJob.Mobile.Controllers
{
    public class WeiXinController : Controller
    {
        public static readonly string Token = WebConfigurationManager.AppSettings["WeixinToken"];//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = WebConfigurationManager.AppSettings["WeixinEncodingAESKey"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string WeixinAppSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string EmploySuccessNoticeTemplateId = WebConfigurationManager.AppSettings["EmploySuccessNoticeTemplateId"];//录用成功通知模板Id。
        public static readonly string EmployResultNoticeTemplateId = WebConfigurationManager.AppSettings["EmployResultNoticeTemplateId"];//录用结果通知模板Id。
        public static readonly string TopColor = "#FFFFFF";
        public static readonly string EmployUrl = WebConfigurationManager.AppSettings["EmployUrl"];//录用列表URL。
        public static readonly string JobDetailUrl = WebConfigurationManager.AppSettings["JobDetailUrl"];//职位详情URL。

        public WeiXinController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。
        /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            postModel.Token = Token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息

            messageHandler.Execute();//执行微信处理过程

            return new FixWeixinBugWeixinResult(messageHandler);//返回结果

        }

        [HttpGet]
        public ActionResult SendEmploySuccessNotice(string openId, string jobName, string companyName, string salary, string workTime, string workPlace)
        {
            var token = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(AppId, WeixinAppSecret);
            Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(token.access_token, openId, EmploySuccessNoticeTemplateId, TopColor, EmployUrl, new
            SendEmploySuccessNoticeData
            {
                first = new ValueItem($"您报名的【{jobName}】职位已经被【{companyName}】录用"),
                keyword1 = new ValueItem(salary),
                keyword2 = new ValueItem(workTime),
                keyword3 = new ValueItem(workPlace),
                remark = new ValueItem("恭喜您！")
            });
            return Json(new { code = StatusCodes.Success }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SendEmployResultNotice(string openId, string workTime, string canceljobId, string canceljobName, string canceljobStatus, string confirmJobName)
        {
            var token = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(AppId, WeixinAppSecret);
            Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(token.access_token, openId, EmployResultNoticeTemplateId, TopColor, JobDetailUrl + canceljobId, new
            SendEmployResultNoticeData
            {
                first = new ValueItem($"您的{canceljobStatus}职位【{canceljobName}】和确认上岗职位【{confirmJobName}】的时间冲突！"),
                keyword1 = new ValueItem(canceljobName),
                keyword2 = new ValueItem(workTime),
                remark = new ValueItem("点击详情，可以重新报名！")
            });
            return Json(new { code = StatusCodes.Success }, JsonRequestBehavior.AllowGet);
        }
    }
}