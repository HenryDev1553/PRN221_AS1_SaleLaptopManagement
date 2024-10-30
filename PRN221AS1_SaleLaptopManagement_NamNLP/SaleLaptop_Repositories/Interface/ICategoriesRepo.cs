using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories.Interface
{
    public interface ICategoriesRepo
    {
        IEnumerable<Category> GetCategories();
        Category? DeleteCategory(int categopryId);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        IEnumerable<Category> GetCategoriesByName(string searchValue);
    }
}
