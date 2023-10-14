using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FullStack_Task.Extensions
{
    public static class CreateDbWithSeedingData
    {

        public static async void SeedDataWithMigration(this WebApplication app)
        {
            #region create the database if it does not already exist.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    //await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.ToString(), "An Error occured during migration");
                }
            }
            #endregion

        }
    }
}
