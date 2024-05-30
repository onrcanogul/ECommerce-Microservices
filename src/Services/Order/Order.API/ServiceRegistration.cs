namespace Order.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            return app;
        }
    }
}
