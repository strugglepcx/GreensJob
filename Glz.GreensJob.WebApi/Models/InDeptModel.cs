using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InDeptModel : RequestParamBase
    {
        [DataMember]
        public int DeptID { get; set; }


        [DataMember]
        public string DeptName { get; set; }
    }
}