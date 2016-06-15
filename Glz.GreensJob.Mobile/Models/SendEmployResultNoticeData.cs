using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glz.GreensJob.Mobile.Models
{
    [Serializable]
    public class SendEmployResultNoticeData
    {
        public ValueItem first { get; set; }
        public ValueItem keyword1 { get; set; }
        public ValueItem keyword2 { get; set; }
        public ValueItem remark { get; set; }
    }
}