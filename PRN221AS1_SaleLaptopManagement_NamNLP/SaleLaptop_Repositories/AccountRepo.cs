using SaleLaptop_BusinessObjects;
using SaleLaptop_Daos;
using SaleLaptop_Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public Account GetAccountByEmail(string email) => AccountDao.Instance.GetAccountByEmail(email);

        public Account GetAccountById(int id) => AccountDao.Instance.GetAccountById(id);

        public Account GetAccountByUserName(string userName) => AccountDao.Instance.GetAccountByUserName(userName);
        public List<Account> GetAccounts() => AccountDao.Instance.GetAccounts();
        public Account Login(string email, string password)
        {
            try
            {
                var account = AccountDao.Instance.GetAccountByEmail(email);
                return account?.Password == password ? account : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
