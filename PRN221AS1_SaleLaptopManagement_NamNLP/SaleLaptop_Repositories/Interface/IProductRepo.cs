using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories.Interface
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetProducts();
        Product? DeleteProduct(int id);
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        IEnumerable<Product> GetProductByName(string name);

    }
}
