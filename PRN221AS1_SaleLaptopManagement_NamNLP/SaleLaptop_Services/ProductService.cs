using SaleLaptop_BusinessObjects;
using SaleLaptop_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;

        public ProductService()
        {
            productRepo = new ProductRepo();
        }
        public List<Product> GetProducts()=> productRepo.GetProducts();
       

        public Product GetProduct(string id)=> productRepo.GetProductById(id);
        

        public bool AddProduct(Product product)=> productRepo.AddProduct(product);
        

        public bool RemoveProduct(string id)=> productRepo.DeleteProduct(id);
        

        public bool UpdateProduct(Product product)=> productRepo.UpdateProduct(product);    
        
    }
}
