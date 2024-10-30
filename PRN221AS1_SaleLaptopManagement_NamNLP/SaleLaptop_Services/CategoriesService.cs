using SaleLaptop_BusinessObjects;
using SaleLaptop_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo categoriesRepo;

        public CategoriesService()
        {
            categoriesRepo = new CategoriesRepo();
        }
        public bool AddCategory(Category category)=> categoriesRepo.AddCategory(category);
        

        public List<Category> GetCategory()=> categoriesRepo.GetCategories();
        

        public Category GetCategory(string id)=> categoriesRepo.GetCategory(id);
        
        public bool RemoveCategory(string id)=> categoriesRepo.RemoveCategory(id);
      

        public bool UpdateCategory(Category category)=> categoriesRepo.UpadateCategory(category);
        
    }
}
