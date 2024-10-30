using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public interface ICategoriesService
    {
        public List<Category> GetCategory();
        public Category GetCategory(string id);
        public bool AddCategory(Category category);
        public bool RemoveCategory(string id);
        public bool UpdateCategory(Category category);
    }
}
