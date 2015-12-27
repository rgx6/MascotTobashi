using System.Windows;
using System.Windows.Input;
using MascotTobashi.Models;
using MascotTobashi.ViewModels;

namespace MascotTobashi.Views
{
    public partial class AreaConfigWindow : Window
    {
        private WindowResizer ob;

        private AreaConfigViewModel areaConfigViewModel;

        public AreaConfigWindow()
        {
            InitializeComponent();

            this.Left = Config.Current.Left;
            this.Top = Config.Current.Top;
            this.Width = Config.Current.Width;
            this.Height = Config.Current.Height;

            areaConfigViewModel = new AreaConfigViewModel();

            this.DataContext = areaConfigViewModel;

            ob = new WindowResizer(this);
        }

        private void Resize(object sender, MouseButtonEventArgs e)
        {
            ob.resizeWindow(sender);

            UpdateViewModel();
        }

        private void DisplayResizeCursor(object sender, MouseEventArgs e)
        {
            ob.displayResizeCursor(sender);
        }

        private void ResetCursor(object sender, MouseEventArgs e)
        {
            ob.resetCursor();
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState != MouseButtonState.Pressed) return;
            ob.dragWindow();

            UpdateViewModel();
        }

        private void max_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

            var width = this.Width;
            var height = this.Height;

            this.WindowState = WindowState.Normal;

            this.Left = 0;
            this.Top = 0;
            this.Width = width;
            this.Height = height;

            UpdateViewModel();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            Config.Current.Left = areaConfigViewModel.Left;
            Config.Current.Top = areaConfigViewModel.Top;
            Config.Current.Width = areaConfigViewModel.Width;
            Config.Current.Height = areaConfigViewModel.Height;

            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateViewModel()
        {
            areaConfigViewModel.Left = this.Left;
            areaConfigViewModel.Top = this.Top;
            areaConfigViewModel.Width = this.Width;
            areaConfigViewModel.Height = this.Height;
        }
    }
}
