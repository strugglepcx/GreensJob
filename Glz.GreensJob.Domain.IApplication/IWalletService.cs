using Glz.Infrastructure;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.IApplication
{
    public interface IWalletService : IApplicationServiceContract
    {
        WalletObject GetObjectByMember(int memberID, int category);
        /*
        int AddObject(WalletObject obj);
        */
        int UpdateObject(WalletObject obj);
    }
}
