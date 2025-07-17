namespace InventoryMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent(); // This loads the resources from App.xaml

            // Set the main page to a new AppShell
            MainPage = new AppShell();
        }
    }
}