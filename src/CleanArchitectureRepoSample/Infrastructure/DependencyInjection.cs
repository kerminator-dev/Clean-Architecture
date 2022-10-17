using Application.Common.Interface;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("AppDatabase");

                options.UseSqlite
                (
                    connectionString,
                    b => b.MigrationsAssembly
                    (
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
                );
            });

            services.AddScoped<IApplicationDbContext>(provider =>
            {
               return provider.GetService<ApplicationDbContext>();
            });


            return services;
        }
    }
}
