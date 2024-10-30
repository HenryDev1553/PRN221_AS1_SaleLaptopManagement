using Microsoft.EntityFrameworkCore;
using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class CategoriesDao
    {
        private static CategoriesDao instance = null;
        private SaleLaptopManageDBContext dbContext;

        public CategoriesDao() {
            dbContext = new SaleLaptopManageDBContext();
        }
        public static CategoriesDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoriesDao();
                return instance;
            }
        }

        public IEnumerable<Category> GetCategoryByName(string name)
        {
            try
            {
                var cateList = dbContext.Categories.Where(c => c.CategoryName.Contains(name)).ToList();
                return cateList;
            } catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }
        public void Add(Category category)
        {
            try
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public Category? Delete(int categoryId)
        {
            try
            {
                var category = dbContext.Categories.Find(categoryId);
                if (category != null)
                {
                    dbContext.Categories.Remove(category);
                    dbContext.SaveChanges();
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public void Update(Category category)
        {
            try
            {
                var cateUp = dbContext.Categories.SingleOrDefault(m => m.CategoryId == category.CategoryId);
                if(cateUp != null)
                {
                    cateUp.CategoryName = category.CategoryName;
                    cateUp.Description = category.Description;
                    dbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }   
        }
        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                var categories = dbContext.Categories.Where(x => true).Select(x => new Category
                {
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    CategoryName = x.CategoryName
                }).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Category? GetCategoryById(int categoryId) => dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == categoryId);
    }

}

