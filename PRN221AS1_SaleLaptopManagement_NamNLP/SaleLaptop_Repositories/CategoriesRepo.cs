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
    public class CategoriesRepo : ICategoriesRepo
    {
        
        public void AddCategory(Category category)
        {
            try
            {
                CategoriesDao.Instance.Add(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public Category? DeleteCategory(int categoryId)
        {
            try
            {
                // Retrieve the category to check for associated products
                var category = CategoriesDao.Instance.GetCategoryById(categoryId);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }

                // Check if the category has any associated products
                if (category.Products != null && category.Products.Any())
                {
                    throw new Exception("Cannot delete this category because it has associated products");
                }

                // Proceed with deletion if no products are associated
                var deletedCategory = CategoriesDao.Instance.Delete(categoryId);
                return deletedCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public IEnumerable<Category> GetCategories()
        {
            try
            {
                var categories = CategoriesDao.Instance.GetAllCategories();
                if (categories == null) throw new Exception("Categories is Empty");
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Category> GetCategoriesByName(string searchValue)
        {
            try
            {
                var result = CategoriesDao.Instance.GetCategoryByName(searchValue);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                CategoriesDao.Instance.Update(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
