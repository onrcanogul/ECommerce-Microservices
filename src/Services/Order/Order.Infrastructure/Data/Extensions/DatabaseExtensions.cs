using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();        

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomerAsync(context);
        }
        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if(!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync();
                await context.SaveChangesAsync();
            }
            
        }
        private static async Task SeedProductAsync(ApplicationDbContext context)
        {

        }
        private static async Task SeedOrderandItemsAsync(ApplicationDbContext context)
        {

        }
    }
}
