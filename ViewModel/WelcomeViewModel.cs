using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Services;
using TheLittleBookNest.View;

namespace TheLittleBookNest.ViewModel
{
    public class WelcomeViewModel
    {
        public ICommand EnterCommand { get; }

        public WelcomeViewModel()
        {
            EnterCommand = new RelayCommand(OpenMainWindow);
        }

        private async void OpenMainWindow(object? parameter = null)
        {
            try
            {
                await Task.Delay(1000); // Vänta 1 sekund för animation
                var mainWindow = new MainWindow();
                mainWindow.Show();

                if (Application.Current.MainWindow is Window welcomeView)
                {
                    Application.Current.MainWindow = mainWindow; // Uppdatera huvudfönstret
                    welcomeView.Close(); // Stäng WelcomeView
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
