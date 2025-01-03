using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;
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

        private async void OpenMainWindow()
        {
            await Task.Delay(1000); // Vänta tills animationen är klar (1 sekund)
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow.Close(); // Stänger WelcomeView
            Application.Current.MainWindow = mainWindow; // Sätter MainWindow som huvudfönster
        }

    }
}