using System;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class WalletService : ApplicationService, IWalletService
    {
        private readonly IRepository<int,Wallet> _walletRepository;

        public WalletService(IRepositoryContext context, IRepository<int, Wallet> WalletRepository) : base(context)
        {
            _walletRepository = WalletRepository;
        }
        /*
        public int AddObject(WalletObject obj)
        {
            try {
                var wallet = new Wallet()
                {
                    memberID = obj.memberID,
                    memberCategory = obj.memberCategory,
                    TotalAmounts = obj.TotalAmounts,
                    Integral = obj.Integral
                };
                _walletRepository.Context.Commit();
                return wallet.ID;
            }
            catch {
                _walletRepository.Context.Rollback();
                return 0;
            }
        }
        */
        public WalletObject GetObjectByMember(int memberID, int category)
        {
            try
            {
                var wallet = _walletRepository.Find(Specification<Wallet>.Eval(x => x.memberID == memberID && x.memberCategory == category));
                return AutoMapper.Mapper.Map<WalletObject>(wallet);
            }
            catch {
                return null;
            }
        }

        public int UpdateObject(WalletObject obj)
        {
            try {
                _walletRepository.Update(new Wallet()
                {
                    ID = obj.ID,
                    TotalAmounts = obj.TotalAmounts,
                    Integral = obj.Integral
                });
                _walletRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _walletRepository.Context.Rollback();
                return 0;
            }
        }
    }
}
