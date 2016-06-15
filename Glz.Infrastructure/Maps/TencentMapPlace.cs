using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Glz.Infrastructure.Maps
{
    public class TencentMapPlace : IMapPlace
    {
        /// <summary>
        /// Api基础URL
        /// </summary>
        private const string BaseApiUrl = "http://apis.map.qq.com/ws/place/v1/";
        /// <summary>
        /// 开发密钥
        /// </summary>
        private const string appKey = "CDNBZ-4NWR5-MW6I6-Q5U4G-GCA27-6WF6K";
        public string GetCity(decimal lat, decimal lng, decimal radius)
        {
            var city = string.Empty;
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(BaseApiUrl);
                var result = proxy.GetAsync($"search?boundary=nearby({lat},{lng},{radius})&page_size=1&page_index=1&keyword=路&orderby=_distance&key={appKey}").Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var resultData = result.Content.ReadAsAsync<dynamic>().Result;
                    if (resultData.status == 0)
                    {
                        city = resultData.data[0].ad_info.city;
                    }
                }
            }
            return city;
        }
    }
}
