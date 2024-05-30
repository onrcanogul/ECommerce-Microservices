using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Order.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });
            return services;
        }
    }
}
