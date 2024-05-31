using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(Guid.NewGuid()),"Customer2","customer2@example.com"),
            Customer.Create(CustomerId.Of(Guid.NewGuid()),"Customer2","customer2@example.com")
        };

        public static IEnumerable<Product> Products { get; set; }
    }
}
