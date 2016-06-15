using System;

namespace Glz.GreensJob.Dto
{
    public class VerificationCodeObject
    {
        public int id { get; set; }
        public string code { get; set; }

        public string mobile { get; set; }

        public DateTime createDate { get; set; }
    }
}