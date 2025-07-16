namespace InventoryMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the main page to be the AppShell
            MainPage = new AppShell();
        }
    }
}