using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pingpp.Models;

namespace Glz.Infrastructure
{
    public class Pingxx
    {
        private readonly string _appkey = ConfigurationManager.AppSettings["Appkey"];
        private readonly string _model = ConfigurationManager.AppSettings["PingxxModel"];
        private readonly string _successUrl = ConfigurationManager.AppSettings["SuccessUrl"];
        private static readonly object SyncObject = new object();

        private static Pingxx _controller;

        private Pingxx()
        {
            if (_model.ToLower().Equals("test"))         //  开发模式
                Pingpp.Pingpp.ApiKey = "sk_test_a58SGO9GOKKKnnTCu1iTmXX1";
            else if (_model.ToLower().Equals("live"))    //  生产模式
                Pingpp.Pingpp.ApiKey = "sk_live_CqbzPSOmn9q54ebT4KGqrz98";
        }

        public static Pingxx CreateInstance()
        {
            if (_controller != null) return _controller;
            lock (SyncObject)
            {
                if (_controller == null)
                {
                    _controller = new Pingxx();
                }
            }
            return _controller;
        }

        /// <summary>
        /// 生成付款凭证
        /// </summary>
        public Charge CreateCharge(string OrderID, decimal Money, string payChannel, string Subject, string Body)
        {
            Charge charge = null;
            var app = new Dictionary<string, string> { { "id", _appkey } };
            var param = new Dictionary<string, object>
            {
                {"order_no", OrderID},
                {"amount", Money * 100},
                {"channel", payChannel}
            };
            //订单总金额, 单位为对应币种的最小货币单位，例如：人民币为分（如订单总金额为 1 元，此处请填 100）。

            #region channel

            /*
            alipay: 支付宝手机支付
            alipay_wap:支付宝手机网页支付
            alipay_qr:支付宝扫码支付
            alipay_pc_direct:支付宝 PC 网页支付
            bfb:百度钱包移动快捷支付
            bfb_wap:百度钱包手机网页支付
            upacp:银联全渠道支付（2015 年 1 月 1 日后的银联新商户使用。若有疑问，请与 Ping++或者相关的收单行联系）
            upacp_wap: 银联全渠道手机网页支付（2015 年 1 月 1 日后的银联新商户使用。若有疑问，请与 Ping++或者相关的收单行联系）
            upacp_pc: 银联 PC 网页支付
            cp_b2b:银联企业网银支付
            wx:微信支付
            wx_pub:微信公众账号支付
            wx_pub_qr:微信公众账号扫码支付
            yeepay_wap:易宝手机网页支付
            jdpay_wap:京东手机网页支付
            cnp_u:应用内快捷支付（银联）
            cnp_f: 应用内快捷支付（外卡）
            applepay_upacp: Apple Pay
            */

            #endregion

            switch (payChannel)
            {
                case "alipay_pc_direct":
                    //Dictionary<String, String> callBack = new Dictionary<string, string>();
                    //extra.Add("success_url", "http://120.26.211.168:8081/api/tool/v1/PaySuccess/");
                    param.Add("extra", new Dictionary<string, object> { { "success_url", _successUrl } });
                    break;
                case "wx_pub_qr":
                    param.Add("extra", new Dictionary<string, object> { { "product_id", "121212" } });
                    break;
                case "wx_pub":
                    param.Add("extra", new Dictionary<string, object> { { "open_id", "o8FtqwGOFpCf5n1F0kRBLCrKIt18" } });
                    break;
            }
            param.Add("currency", "cny");
            param.Add("subject", Subject);   //商品的标题，该参数最长为 32 个 Unicode 字符，银联全渠道（upacp/upacp_wap）限制在 32 个字节。
            param.Add("body", Body);         //商品的描述信息，该参数最长为 128 个 Unicode 字符，yeepay_wap 对于该参数长度限制为 100 个 Unicode 字符。
            //param.Add("client_ip", IPv4);
            //发起支付请求终端的 IP 地址，格式为 IPV4，如: 127.0.0.1。
            param.Add("client_ip", "127.0.0.1");
            param.Add("app", app);
            charge = Charge.Create(param);
            //return charge;
            return charge;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="chId">凭证号</param>
        /// <param name="Money">退款金额</param>
        /// <param name="Description">退款描述</param>
        /// <returns></returns>
        public Refund Refunds(string chId, int Money, string Description)
        {
            Refund re = null;
            Dictionary<String, Object> refundParam = new Dictionary<String, Object>();
            refundParam.Add("amount", Money);
            refundParam.Add("description", Description);
            try
            {
                re = Refund.Create(chId, refundParam);
            }
            catch
            {

            }
            return re;
        }

    }

    /// <summary>
    /// 支付渠道
    /// </summary>
    public class PayChannel
    {
        /// <summary>
        /// 支付宝手机支付
        /// </summary>
        public static string alipay = "alipay";
        /// <summary>
        /// 支付宝 PC 网页支付
        /// </summary>
        public static string alipay_pc_direct = "alipay_pc_direct";
        /// <summary>
        /// 微信支付
        /// </summary>
        public static string wx = "wx";
        /// <summary>
        /// 银联全渠道支付
        /// </summary>
        public static string upacp = "upacp";
        /// <summary>
        /// 银联 PC 网页支付
        /// </summary>
        public static string upacp_pc = "upacp_pc";
    }
}
