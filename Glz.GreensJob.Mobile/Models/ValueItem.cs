using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glz.GreensJob.Mobile.Models
{
    [Serializable]
    public class ValueItem
    {
        public ValueItem(string value)
            : this(value, "#743A3A")
        {

        }
        public ValueItem(string value, string color)
        {
            this.value = value;
            this.color = color;
        }
        public string value { get; set; }
        public string color { get; set; }
    }
}