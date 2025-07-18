namespace InventoryMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register navigation route
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}