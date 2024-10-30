using Microsoft.EntityFrameworkCore;
using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class ProductDao
    {
        private static ProductDao instance = null;
        private SaleLaptopManageDBContext dbContext;
        private ProductDao()
        {
            dbContext = new SaleLaptopManageDBContext();
        }
        public static ProductDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductDao();
                return instance;
            }
        }
        public IEnumerable<Product> GetProducts() => dbContext.Products.Include(x => x.Category).ToList();
        public Product GetProductById(int id)
        {
            try
            {
                var product = dbContext.Products.Include(x => x.Category).SingleOrDefault(c => c.ProductId.Equals(id));
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void Add(Product product)
        {
            if(product != null)
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
        }
        public void Update(Product product)
        {
            var productUpdate = dbContext.Products.SingleOrDefault(m => m.ProductId == product.ProductId);
            if (productUpdate != null)
            {
                productUpdate.ProductId = product.ProductId;
                productUpdate.Brand = product.Brand;
                productUpdate.Model = product.Model;
                productUpdate.ProductName = product.ProductName;
                productUpdate.Price = product.Price;
                productUpdate.Stock = product.Stock;
                productUpdate.CategoryId = product.CategoryId;
                dbContext.SaveChanges();
            }
        }
        public Product? Delete(int productId)
        {
            try
            {
                var product = dbContext.Products.SingleOrDefault(p => p.ProductId.Equals(productId));

                if (product != null)
                {
                    dbContext.Products.Remove(product);
                    dbContext.SaveChanges();
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Product> GetProductByName(string name)
        {
            try
            {
                var product = dbContext.Products.Include(x => x.Category).Where(c => c.ProductName.Contains(name)).ToList();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
