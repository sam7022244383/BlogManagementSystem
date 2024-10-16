using Application.Interface;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureDIRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            ,IConfiguration configuration)
        {
            services.AddDbContext<AppContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
             o => o.EnableRetryOnFailure()));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAuthorRepository , AuthorRepository>();
            return services;
        }
    }
}
