using Matchfinder.Data;
using Matchfinder.Interfaces;
using Matchfinder.Services;
using Microsoft.EntityFrameworkCore;

namespace Matchfinder.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();

            return services;
        }
    }
}
