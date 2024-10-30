using SaleLaptop_BusinessObjects;
using SaleLaptop_Daos;
using SaleLaptop_Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SaleLaptop_Repositories
{
    public class ProductRepo : IProductRepo
    {
        public void AddProduct(Product product)
        {
            if(product == null)
            {
                Console.WriteLine("ko co product");
            }
            else
            {
                ProductDao.Instance.Add(product);
            }
           
        }
        public Product? DeleteProduct(int id)
        {
            try
            {
                var product = ProductDao.Instance.Delete(id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product? GetProductById(int id)
        {
            try
            {
                var product = ProductDao.Instance.GetProductById(id);
                if(product ==null) throw new Exception("Product not found");
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
                var result = ProductDao.Instance.GetProductByName(name);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetProducts()
        {

            try
            {
                var result = ProductDao.Instance.GetProducts();
                if (result == null) throw new Exception("Product is Empty");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                ProductDao.Instance.Update(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
