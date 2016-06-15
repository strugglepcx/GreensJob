using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutEmployeeInfoModele : ResultBase
    {
        [DataMember]
        public List<EmployeeInfo> employerList { get; set; }
        [DataMember]
        public int? PageNumber { get; set; }
        [DataMember]
        public int? PageSize { get; set; }
        [DataMember]
        public int? TotalPages { get; set; }
        [DataMember]
        public int? TotalRecords { get; set; }
    }

    [DataContract]
    public class EmployeeInfo
    {
        [DataMember]
        public int EnrollID { get; set; }
        [DataMember]
        public string employeeName { get; set; }
        [DataMember]
        public string employeeMobileNumber { get; set; }
        [DataMember]
        public int employeeState { get; set; }
        [DataMember]
        public int enrollMethod { get; set; }
        [DataMember]
        public int continousDays { get; set; }
        [DataMember]
        public List<EmployeeDate> employData { get; set; }
        [DataMember]
        public string jobName { get; set; }
        [DataMember]
        public IList<EnrollDetailObject> EnrollDetail { get; set; }
        [DataMember]
        public int userID { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        [DataMember]
        public int jobId { get; set; }
    }
    [DataContract]
    public class EmployeeDate
    {
        [DataMember]
        public DateTime startDate { get; set; }
        [DataMember]
        public DateTime endDate { get; set; }
    }


}