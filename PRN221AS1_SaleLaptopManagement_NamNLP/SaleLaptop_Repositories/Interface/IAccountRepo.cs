using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories.Interface
{
    public interface IAccountRepo
    {
        public List<Account> GetAccounts();
        public Account GetAccountById(int id);
        public Account GetAccountByUserName(string userName);
        public Account GetAccountByEmail(string email);
        public Account? Login(string email, string password);

    }
}
