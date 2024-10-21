using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.Extensions.Azure;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Infra layer
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAzureClients(Clientbuilder =>
            {
                Clientbuilder.AddQueueServiceClient(builder.Configuration["AzureMessageKey:QueueKey"]);  
            });
            //applicaton layer
            builder.Services.AddApplicationServices();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.CreatedbIfNotExists();
            app.Run();
        }
    }
}