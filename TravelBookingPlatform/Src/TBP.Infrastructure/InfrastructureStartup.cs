using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBP.Core.Interfaces;
using TBP.Infrastructure.Database;
using TBP.Infrastructure.Database.Repositories;

namespace TBP.Infrastructure
{
    public static class InfrastructureStartup
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TBPDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"));
            });
            services.AddScoped<IFeaturedDealRepository, FeaturedDealRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
