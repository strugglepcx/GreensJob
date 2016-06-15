using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class Publisher : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int companyID { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }

        public string password { get; set; }

        public bool isAdmin { get; set; }

        public DateTime lastLoginDate { get; set; }

        public DateTime createDate { get; set; }

        public virtual PublisherRight PublisherRight { get; set; }
        /// <summary>
        /// 发布者钱包
        /// </summary>
        public virtual PublisherWallet PublisherWallet { get; set; }
    }
}
