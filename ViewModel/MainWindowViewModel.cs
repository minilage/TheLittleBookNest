using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.View;

namespace TheLittleBookNest.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToBooksCommand { get; }
        public ICommand NavigateToAuthorsCommand { get; }

        public MainWindowViewModel()
        {
            // Sätt standardvyn (tillfälligt tom eller en välkomstvy)
            CurrentView = null;

            // Navigeringskommando för Books
            NavigateToBooksCommand = new RelayCommand(o => CurrentView = new BooksView());

            // Navigeringskommando för Authors
            NavigateToAuthorsCommand = new RelayCommand(o =>
            {
                var authorsView = new AuthorsView();
                authorsView.DataContext = new AuthorsViewModel();
                CurrentView = authorsView;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
