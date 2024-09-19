using AuthenticationAndAuthorization.Extensions;

namespace AuthenticationAndAuthorization
{
    public static class ServiceProviderFactory
    {
        public static ServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .Build();
            services.ConfigureServices(configuration);
            return services.BuildServiceProvider();
        }
    }
}
