using System.Windows;
using System.Windows.Input;

namespace TheLittleBookNest.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
