using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.ViewModel; // Lägg till namespace för MainWindowViewModel

namespace TheLittleBookNest.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Sätt DataContext till MainWindowViewModel
            DataContext = new MainWindowViewModel();
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown(); // Stänger applikationen
            }
        }
    }
}

