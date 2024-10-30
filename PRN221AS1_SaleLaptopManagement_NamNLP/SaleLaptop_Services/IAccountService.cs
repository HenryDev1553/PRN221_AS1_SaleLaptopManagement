using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public interface IAccountService
    {
        public Account GetAccount(string email);
        public Account Login (string email, string password);
    }
}
