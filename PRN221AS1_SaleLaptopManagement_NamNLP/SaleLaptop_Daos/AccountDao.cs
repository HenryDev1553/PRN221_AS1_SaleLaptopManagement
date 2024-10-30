using Microsoft.EntityFrameworkCore;
using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class AccountDao
    {
        private static AccountDao instance = null;
        private SaleLaptopManageDBContext dbContext;

        public AccountDao()
        {
            dbContext = new SaleLaptopManageDBContext();
        }

        public static AccountDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDao();
                return instance;
            }
        }

        public Account GetAccountById(int accountId) => dbContext.Accounts.SingleOrDefault(x => x.AccountId == accountId);
        public Account GetAccountByUserName(string username) => dbContext.Accounts.SingleOrDefault(x => x.UserName == username);
        public Account GetAccountByEmail(string email) {
            try
            {
                var account = dbContext.Accounts.SingleOrDefault(y => y.Email == email);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception("User not exist !!");
            }
        }
        public List<Account> GetAccounts() => dbContext.Accounts.ToList();
        
        
    }
}
