using AzBlobStorage.Interfces;
using AzBlobStorage.Servicces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBlobStorage.IoC
{
    public static class AzBlobStorageInfrastructureRegister
    {
        public static void AddInfrastructurLayer(IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IAzBlobStorageService, AzBlobStorageService>();

        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.c <AzConfiguration>(configuration.GetSection("AzConfiguration"));

        }





    }
}
