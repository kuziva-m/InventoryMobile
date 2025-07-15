using CommunityToolkit.Mvvm.DependencyInjection;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            using (var scope = Ioc.Default.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
                dbContext.Database.Migrate();
            }
            MainPage = new AppShell();
        }
    }
}
