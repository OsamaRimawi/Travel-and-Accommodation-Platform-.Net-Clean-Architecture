using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TBP.Core
{
    public static class CoreStartup
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }

}
