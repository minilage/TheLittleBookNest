using System.Windows;
using TheLittleBookNest.View;

namespace TheLittleBookNest.Services
{
    public static class MessageBoxHelper
    {
        public static void ShowMessage(string message, string title = "Message", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information)
        {
            var customMessageBox = new CustomMessageBox(message, title);
            customMessageBox.ShowDialog();
        }
    }
}