using SaleLaptop_BusinessObjects;
using SaleLaptop_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo accountRepo =null;
        public AccountService()
        {
            if(accountRepo == null) accountRepo = new AccountRepo();
        }

        public Account GetAccount(string email)=> accountRepo.GetAccount(email);

        public Account Login(string email, string password)
        {
            Account account = GetAccount(email);
            if (account == null)
            {
                if (account.Password == password)
                {
                    return account;
                }
            }
            return null;
        }
    }
}
