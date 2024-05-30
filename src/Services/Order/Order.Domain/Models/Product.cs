using Order.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;


        public static Product Create(ProductId id,  string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new()
            {
                Name = name,
                Id = id,
                Price = price,
            };

        }
    }
}
