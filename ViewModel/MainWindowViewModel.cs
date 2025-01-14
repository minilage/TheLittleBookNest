using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheLittleBookNest.Command;

namespace TheLittleBookNest.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object? currentView;
        public object? CurrentView
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

        private readonly Lazy<BooksViewModel> booksViewModel = new(() => new BooksViewModel());
        private readonly Lazy<AuthorsViewModel> authorsViewModel = new(() => new AuthorsViewModel());
        private readonly Lazy<StoresViewModel> storesViewModel = new(() => new StoresViewModel());

        public MainWindowViewModel()
        {
            NavigateToBooksCommand = new RelayCommand(o => CurrentView = booksViewModel.Value);
            NavigateToAuthorsCommand = new RelayCommand(o => CurrentView = authorsViewModel.Value);
            NavigateToStoresCommand = new RelayCommand(o => CurrentView = storesViewModel.Value);
            CurrentView = new DashboardViewModel(); // Sätter DashboardView som standard
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
