using System.Windows;
using MascotTobashi.Models;

namespace MascotTobashi
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Config.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Config.Current.Save();
        }
    }
}
