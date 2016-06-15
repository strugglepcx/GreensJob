using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Glz.GreensJob.WebApi.Models;
using Glz.Infrastructure;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// 工具
    /// </summary>
    [RoutePrefix("api/tool")]
    public class ToolController : ApiBaseController
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param Name="file">文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/upload")]
        public OutUploadModel Upload(Stream file)
        {
            try
            {
                var ms = new MemoryStream();
                file.CopyTo(ms);
                ms.Position = 0;

                var encoding = Encoding.UTF8;
                var reader = new StreamReader(ms, encoding);
                var headerLength = 0L;

                //读取第一行  
                var firstLine = reader.ReadLine();
                //计算偏移（字符串长度+回车换行2个字符）  
                headerLength += encoding.GetBytes(firstLine).LongLength + 2;

                //读取第二行  
                var secondLine = reader.ReadLine();
                //计算偏移（字符串长度+回车换行2个字符）  
                headerLength += encoding.GetBytes(secondLine).LongLength + 2;
                //解析文件名  
                var cFileName = new System.Text.RegularExpressions.Regex("filename=\"(?<fn>.*)\"").Match(secondLine).Groups["fn"].Value;
                var nCur = cFileName.LastIndexOf(".");
                var FileExtension = "";
                if (nCur != -1)
                    FileExtension = cFileName.Substring(nCur);
                var path = System.Web.HttpContext.Current.Server.MapPath("~/upload/");
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + FileExtension;
                var fileFullName = path + fileName;

                //一直读到空行为止  
                while (true)
                {
                    //读取一行  
                    var line = reader.ReadLine();
                    //若到头，则直接返回  
                    if (line == null)
                        break;
                    //若未到头，则计算偏移（字符串长度+回车换行2个字符）  
                    headerLength += encoding.GetBytes(line).LongLength + 2;
                    if (line == "")
                        break;
                }

                //设置偏移，以开始读取文件内容  
                ms.Position = headerLength;
                ////减去末尾的字符串：“\r\n--\r\n”  
                ms.SetLength(ms.Length - encoding.GetBytes(firstLine).LongLength - 3 * 2);

                using (var fileToupload = new FileStream(fileFullName, FileMode.Create))
                {
                    ms.CopyTo(fileToupload);
                    fileToupload.Close();
                    fileToupload.Dispose();
                }
                var outCode = CreateResult<OutUploadModel>(StatusCodes.Success, "上传成功");
                List<string> list = new List<string>();
                list.Add("/upload/" + fileName);
                outCode.Data = list;
                return outCode;
            }
            catch
            {
                return CreateResult<OutUploadModel>(0, "上传失败");
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param Name="file">文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/Import")]
        public ResultBase Import(Stream file)
        {
            return CreateResult<ResultBase>(0, "导入失败");
        }

        /*
        /// <summary>
        /// 生成支付凭证
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/CreateCharge")]
        public OutChargeModel CreateCharge([FromBody]InChargeModel appKey) {
            if (!ModelState.IsValid) return CreateResult<OutChargeModel>(0, "无效参数");
            var clientIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            var charge = Pingxx.CreateInstance().CreateCharge(appKey.OrderID, appKey.Money, appKey.Channel, appKey.Subject, appKey.Body, clientIP);
            if (charge!=null)
            {
                var outCode = CreateResult<OutChargeModel>(StatusCodes.Success, "生成成功");
                outCode.Charge = charge;
                return outCode;
            }
            else
            {
                return CreateResult<OutChargeModel>(0, "生成失败");
            }
        }
        */

    }
}
