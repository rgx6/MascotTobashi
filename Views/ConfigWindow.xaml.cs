using System.Windows;
using System.Windows.Controls;
using MascotTobashi.Models;
using MascotTobashi.ViewModels;

namespace MascotTobashi.Views
{
    public partial class ConfigWindow : Window
    {
        private ConfigViewModel configViewModel;

        public ConfigWindow()
        {
            InitializeComponent();

            configViewModel = new ConfigViewModel();

            this.DataContext = configViewModel;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            Config.Current.LaunchIntervalSeconds = configViewModel.LaunchIntervalSeconds;
            Config.Current.LaunchDurationSecondsMin = configViewModel.LaunchDurationSecondsMin;
            Config.Current.LaunchDurationSecondsMax = configViewModel.LaunchDurationSecondsMax;
            Config.Current.Scale = configViewModel.Scale;

            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
