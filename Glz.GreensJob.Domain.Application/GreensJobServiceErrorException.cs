using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application
{
    public class GreensJobServiceErrorException : Exception
    {
        public GreensJobServiceErrorException(string message) : base(message)
        {
        }
    }
}
