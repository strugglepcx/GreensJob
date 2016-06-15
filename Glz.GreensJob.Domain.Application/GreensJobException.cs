using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Application
{
    public class GreensJobException : Exception
    {
        public GreensJobException(StatusCodes code, string message)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public StatusCodes Code { get; private set; }
    }
}
