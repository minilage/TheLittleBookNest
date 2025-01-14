using System.Windows;

namespace TheLittleBookNest.View
{
    public partial class CustomMessageBox : Window
    {
        public string Message { get; set; } = string.Empty;

        public CustomMessageBox(string message, string title = "Message")
        {
            InitializeComponent();
            DataContext = this; // Bind data till fönstret
            Message = message;
            Title = title;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
