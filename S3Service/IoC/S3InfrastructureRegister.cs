using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using S3Service.Interfces;
using S3Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Service.IoC
{
    public static class S3InfrastructureRegister
    {

        public static void AddInfrastructurLayer(this IServiceCollection services, IConfiguration configuration)
        {
            AddAwsConfig(services, configuration);
            AddServices(services);
        }

        private static void AddConfiguration(this IServiceCollection services, IConfiguration configuration) { }
        private static void AddRepositories(this IServiceCollection services) { }
        private static void AddServices(this IServiceCollection services) 
        {
            services.AddSingleton<IS3BucketOperationsService, S3BucketOperationsService>();
            services.AddSingleton<IS3FileOperationsService, S3FileOperationsService>();
        }

        private static void AddJWTBearerConfig(this IServiceCollection services, IConfiguration configuration) { }
        private static void AddSwaggerConfig(this IServiceCollection services) { }

        private static void AddPolicyConfig(this IServiceCollection services, IConfiguration configuration) { }
        private static void AddAwsConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
        }


    }
}
