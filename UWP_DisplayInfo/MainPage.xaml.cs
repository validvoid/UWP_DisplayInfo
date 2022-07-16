using System.Diagnostics;
using Windows.Devices.Display.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_DisplayInfo
{
    public sealed partial class MainPage : Page
    {
        public DisplayInfoViewModel ViewModel => (DisplayInfoViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new DisplayInfoViewModel();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadDisplayInfo();
        }
    }
}
