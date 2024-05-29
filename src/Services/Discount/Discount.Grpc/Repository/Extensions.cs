using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Repository
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using DiscountContext context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            context.Database.MigrateAsync();


            return app;
        }
    }
}
