using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public string  Name { get; set; }
        public double Price { get; set; }
        public int MinQuantity { get; set; }
        public string ImagePath { get; set; }
        public double? DiscountRate { get; set; }
        public Guid Code { get; set; } = Guid.NewGuid();
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
