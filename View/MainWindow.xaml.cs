using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.ViewModel;

namespace TheLittleBookNest.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown(); // Stänger applikationen
            }
        }

        // Hantera klick på bannern
        private void BannerImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                // Uppdatera CurrentView till standardvyn
                viewModel.CurrentView = null; // Byt till standardvyn eller en specifik vy här
            }
        }
    }
}
