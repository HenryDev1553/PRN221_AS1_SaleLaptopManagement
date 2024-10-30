using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class RoleDao
    {
        private static RoleDao instance;
        private SaleLaptopManageDBContext dbContext;
        public RoleDao()
        {
            dbContext = new SaleLaptopManageDBContext();
        }
        public static RoleDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoleDao();
                return instance;
            }
        }

    }
}
