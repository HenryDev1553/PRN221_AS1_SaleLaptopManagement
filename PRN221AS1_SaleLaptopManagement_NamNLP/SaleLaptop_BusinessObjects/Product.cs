using System;
using System.Collections.Generic;

namespace SaleLaptop_BusinessObjects
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
