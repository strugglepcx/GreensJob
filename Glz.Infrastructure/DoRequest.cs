using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Glz.Infrastructure
{
    public class DoRequest
    {
        /// <summary>
        /// 短信验证，用户名
        /// </summary>
        public static readonly string CorpID = "SDK2524";

        /// <summary>
        /// 短信验证，密码
        /// </summary>
        public static readonly string Pwd = "123456";

        /// <summary>
        /// 验证码信息的格式
        /// </summary>
        public static string ValidationBodyFormat = "请输入验证码{0}进行验证，此验证码10分钟内有效。如有任何疑问，请咨询027-87743235，我们将竭诚为您服务！";
        /// <summary>
        /// 录用确认信息的格式
        /// </summary>
        public static string EmployConfirmBodyFormat = "录用通知：您报名的{0}，{1}上班，地点:{2}。请登录兼职客平台确认。";
        /// <summary>
        /// 支付确认信息的格式
        /// </summary>
        public static string PayedBodyFormat = "您已到成功提现{0}元到微信帐户{1}";
        ///// <summary>
        ///// 支付确认信息的格式
        ///// </summary>
        //public static string PayedBodyFormat = "您已到成功提现{0}元到微信帐户{1}";

        /// <summary>
        /// 请求WebService
        /// </summary>
        /// <param Name="Url">WebService地址</param>
        /// <param Name="Method">调用方法</param>
        /// <param Name="Mobile">发送手机号码</param>
        /// <param Name="Content">发送内容</param>
        /// <param Name="Cell">子号（可空）</param>
        /// <param Name="SendTime">定时时间（可空）</param>
        /// <returns>
        /// 返回发送结果：
        /// 大于0发送成功，
        /// -1：账号未注册，
        /// -2：其他错误，
        /// -3：帐号或密码错误，
        /// -4：一次提交信息不能超过10000 个手机号码，号码逗号隔开，
        /// -5：余额不足，请先充值
        /// -6：定时发送时间不是有效的时间格式
        /// -8：发送内容需在3 到250 字之间
        /// -9：发送号码为空
        /// -104：短信内容包含关键字
        /// </returns>
        public static long HttpPostWebService(string Url, string Method, string Mobile, string Content, string Cell, string SendTime)
        {
            long result = 0;
            string postData = "CorpID=" + CorpID + "&Pwd=" + Pwd + "&Mobile=" + Mobile + "&Content=" + HttpUtility.UrlEncode(Content, Encoding.UTF8) + "&Cell=" + Cell + "&SendTime=" + SendTime;
            //string postData = "CorpID=" + CorpID + "&Pwd=" + Pwd + "&Mobile=" + Mobile + "&Content=" + HttpUtility.UrlEncode(Content, Encoding.UTF8);
            byte[] dataArray = Encoding.UTF8.GetBytes(postData);
            //创建请求
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url + "/" + Method);
            request.Method = "POST";
            request.Host = "api.bjszrk.com";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataArray.Length;
            request.Timeout = 3000;
            Stream dataStream = null;
            StreamReader reader = null;
            //创建输入流
            try
            {
                dataStream = request.GetRequestStream();
                //发送请求
                dataStream.Write(dataArray, 0, dataArray.Length);
                dataStream.Close();

                reader = new StreamReader(request.GetResponse().GetResponseStream());
                string res = reader.ReadToEnd();
                //<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<string xmlns=\"http://tempuri.org/\">4924061679747073</string>
                result = Convert.ToInt64(res.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "").Replace("\r\n", "").Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", ""));
                reader.Close();
            }
            catch
            {
                return result;//连接服务器失败
            }
            finally
            {

            }
            return result;
        }
    }
}
