﻿using CommunityToolkit.Mvvm.DependencyInjection;
using Inventory.Core.Application.Interfaces;
using Inventory.Core.Application.Services;
using Inventory.Core.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using InventoryMobile.Converters;
using InventoryMobile.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryMobile;

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
            options.UseSqlite($"Data Source={dbPath}",
                sqliteOptions => sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        // Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IInventoryService, InventoryService>();

        // ViewModels & Pages
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>();

        // Converters
        builder.Services.AddTransient<InverseBoolConverter>();

        var app = builder.Build();

        Ioc.Default.ConfigureServices(app.Services);

        // Move database migration to a background task
        Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            await dbContext.Database.MigrateAsync();
        });

        return app;
    }
}
