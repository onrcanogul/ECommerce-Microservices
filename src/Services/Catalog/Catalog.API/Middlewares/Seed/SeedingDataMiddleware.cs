namespace Catalog.API.Middleware.Seed
{
    public static class SeedingDataMiddleware
    {
        public async static void UseSeed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();



            if (context.Products.Count() == 0)
            {
                await context.Products.AddAsync(new()
                {
                    Name = "Product1",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description1",
                    ImageFile = "ImageFile",
                    Price = 100
                });
                await context.Products.AddAsync(new()
                {
                    Name = "Product2",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description2",
                    ImageFile = "ImageFile",
                    Price = 200
                });
                await context.Products.AddAsync(new()
                {
                    Name = "Product3",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description3",
                    ImageFile = "ImageFile",
                    Price = 300
                });
                await context.Products.AddAsync(new()
                {
                    Name = "Product4",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description4",
                    ImageFile = "ImageFile",
                    Price = 400
                });
                await context.Products.AddAsync(new()
                {
                    Name = "Product5",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description5",
                    ImageFile = "ImageFile",
                    Price = 500
                });
                await context.Products.AddAsync(new()
                {
                    Name = "Product6",
                    Category = ["category1", "category2", "category3"],
                    Description = "Description6",
                    ImageFile = "ImageFile",
                    Price = 600
                });

                await context.SaveChangesAsync();

            }
        }
        
    }
}