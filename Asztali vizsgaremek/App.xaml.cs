using System.Configuration;
using System.Data;
using System.Windows;

namespace Asztali_vizsgaremek
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }
}

