using Inventory.Core.Application.Interfaces;
using Inventory.Core.Application.Services;
using Inventory.Core.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using InventoryMobile.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection; // Add this using statement

namespace InventoryMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "inventory.db");

            builder.Services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            var app = builder.Build();

            // Configure the Ioc service provider
            Ioc.Default.ConfigureServices(app.Services);

            // Run database migration
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}