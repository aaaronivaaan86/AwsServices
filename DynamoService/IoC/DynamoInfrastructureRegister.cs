using Amazon.DynamoDBv2;
using DynamoService.Interfaces;
using DynamoService.Repositories;
using DynamoService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoService.IoC
{
    public static class DynamoInfrastructureRegister
    {

        public static void AddInfrastructurLayer(this IServiceCollection services, IConfiguration configuration)
        {
            AddRespositories(services);
            AddServices(services);
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDynamoService, DynamoServiceService>();
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();


        }

        private static void AddRespositories(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
        }

    }

}
