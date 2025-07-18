namespace InventoryMobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Set the root navigation to Shell
        MainPage = new AppShell();
    }
}
