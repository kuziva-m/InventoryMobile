namespace InventoryMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register navigation route
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

        // Navigate to MainPage immediately after Shell loads
        Dispatcher.Dispatch(async () =>
        {
            await GoToAsync(nameof(MainPage));
        });
    }
}
