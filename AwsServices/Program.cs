using Amazon.S3;
using AzBlobStorage.IoC;
using DynamoService.IoC;
using S3Service.IoC;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        S3InfrastructureRegister.AddInfrastructurLayer(builder.Services, builder.Configuration);
        AzBlobStorageInfrastructureRegister.AddInfrastructurLayer(builder.Services, builder.Configuration);
        DynamoInfrastructureRegister.AddInfrastructurLayer(builder.Services, builder.Configuration);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}