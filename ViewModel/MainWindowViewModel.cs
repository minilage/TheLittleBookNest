using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheLittleBookNest.Command;

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
        public ICommand NavigateToStoresCommand { get; }

        public MainWindowViewModel()
        {
            // Initialisera navigeringskommandon med ViewModels
            NavigateToBooksCommand = new RelayCommand(o => CurrentView = new BooksViewModel());
            NavigateToAuthorsCommand = new RelayCommand(o => CurrentView = new AuthorsViewModel());
            NavigateToStoresCommand = new RelayCommand(o => CurrentView = new StoresViewModel());

            // Sätt standardvyn till null tills en specifik startvy definieras
            CurrentView = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
