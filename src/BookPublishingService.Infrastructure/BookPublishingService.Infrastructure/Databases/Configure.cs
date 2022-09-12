using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookPublishingService.Infrastructure.Databases
{
    public static class Configure
    {
        public static void UseServicesData(this IServiceCollection services, IConfiguration configuration)
        {            

            services.AddTransient<IDatabaseConnection, DatabaseConnection>();
            services.AddTransient<IDatabase, Database>();

        }
    }
}
