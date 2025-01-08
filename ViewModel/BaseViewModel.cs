using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;

namespace TheLittleBookNest.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; }

        protected BaseViewModel()
        {
            CloseCommand = new RelayCommand(CloseDialog);
            SaveCommand = new RelayCommand(SaveData, CanSave);
        }

        private void CloseDialog(object? parameter)
        {
            Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive)?.Close();
        }

        protected virtual void SaveData(object? parameter)
        {
            MessageBox.Show("Save logic not implemented.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected virtual bool CanSave(object? parameter) => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
