using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Sms
{
    public interface ISmsMessage
    {
        void SendEmploySuccessNotice(string mobile, string jobName, string firstWorkDate, string workPlace);
    }
}
