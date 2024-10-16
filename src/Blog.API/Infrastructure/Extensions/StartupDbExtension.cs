using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions
{
    public static class StartupDbExtension
    {
        public static async void CreatedbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var blogContext = services.GetRequiredService<AppContext>();
            var logger = services.GetRequiredService<ILogger<AppContext>>();
            try
            {
                var databasecreate = blogContext.Database.GetService<IDatabaseCreator>()
                            as RelationalDatabaseCreator;
                if(databasecreate != null)
                {
                    logger.LogInformation("enter databasecreate");
                    if (!databasecreate.CanConnect())
                    {
                        databasecreate.Create();
                        logger.LogInformation("enter database create");
                    }
                    if(!databasecreate.HasTables()) 
                    {
                        databasecreate.CreateTables();
                        logger.LogInformation("enter databasecreate createTables");
                    
                    }
                    DBInitializerSeedData.InitializeDatabase(blogContext);
                    logger.LogInformation("enter databasecreate InitializeDatabase");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"migration issue {ex.Message}");
            }

        }
    }
}
