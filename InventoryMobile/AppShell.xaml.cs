namespace InventoryMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register the MainPage for routing
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}