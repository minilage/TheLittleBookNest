using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheLittleBookNest.Command;

namespace TheLittleBookNest.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object? currentView = null!; // Initierad med null!
        public object? CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(); // Använder CallerMemberName
            }
        }

        // Navigeringskommandon
        public ICommand NavigateToBooksCommand { get; }
        public ICommand NavigateToAuthorsCommand { get; }
        public ICommand NavigateToStoresCommand { get; }

        // Lazy-initialisering av ViewModels
        private readonly Lazy<BooksViewModel> booksViewModel = new(() => new BooksViewModel());
        private readonly Lazy<AuthorsViewModel> authorsViewModel = new(() => new AuthorsViewModel());
        private readonly Lazy<StoresViewModel> storesViewModel = new(() => new StoresViewModel());

        public MainWindowViewModel()
        {
            // Initiera navigeringskommandon
            NavigateToBooksCommand = new RelayCommand(o => CurrentView = booksViewModel.Value);
            NavigateToAuthorsCommand = new RelayCommand(o => CurrentView = authorsViewModel.Value);
            NavigateToStoresCommand = new RelayCommand(o => CurrentView = storesViewModel.Value);

            // Sätt standardvyn
            CurrentView = null; // Initial vy sätts här
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable för att matcha INotifyPropertyChanged

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
