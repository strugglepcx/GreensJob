using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class Company : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int cityID { get; set; }

        public string name { get; set; }

        public string image { get; set; }

        public string introduce { get; set; }

        public string addr { get; set; }

        public int status { get; set; }

        public string license { get; set; }

        public string certificates { get; set; }

        public string contact { get; set; }

        public string tel { get; set; }

        public DateTime createDate { get; set; }
        /// <summary>
        /// 关注公司求职者列表
        /// </summary>
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}
