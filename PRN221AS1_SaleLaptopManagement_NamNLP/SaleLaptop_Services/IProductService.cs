using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Product GetProduct(string id);
        public bool AddProduct(Product product);
        public bool RemoveProduct(string id);
        public bool UpdateProduct(Product product);
    }
}
