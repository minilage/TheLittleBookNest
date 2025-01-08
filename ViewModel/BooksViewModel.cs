using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;
using TheLittleBookNest.View;

namespace TheLittleBookNest.ViewModel
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>(); // Initierad
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged(); // Använder CallerMemberName
            }
        }

        // Flagga för att kontrollera om data är laddad
        private bool isDataLoaded;

        // Kommando för att öppna dialogen
        public ICommand AddBookCommand { get; }

        // Kommando för Page Down
        public ICommand ScrollToBottomCommand { get; }

        public BooksViewModel()
        {
            // Initiera kommandon
            AddBookCommand = new RelayCommand(OpenAddBookDialog);
            ScrollToBottomCommand = new RelayCommand(ScrollToBottom);

            // Starta laddning av data
            LoadBooksAsync().ConfigureAwait(false); // Undviker CS4014
        }

        private async Task LoadBooksAsync()
        {
            // Kontrollera om data redan är laddad
            if (isDataLoaded)
                return;

            try
            {
                using (var context = new AppDbContext())
                {
                    var books = await context.Books.ToListAsync();
                    Books = new ObservableCollection<Book>(books);
                }
                isDataLoaded = true; // Markera data som laddad
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
            }
        }

        private void OpenAddBookDialog(object? parameter)
        {
            // Skapa och öppna dialogen
            var dialog = new AddBookDialog
            {
                DataContext = new AddBookDialogViewModel() // Koppla till ViewModel för dialogen
            };

            dialog.ShowDialog();

            // Uppdatera böcker efter att dialogen stängs
            LoadBooksAsync().ConfigureAwait(false);
        }

        private void ScrollToBottom(object? parameter)
        {
            // Hämta det aktiva fönstret
            var focusedWindow = Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive);

            // Hitta BooksDataGrid och scrolla till sista objektet
            if (focusedWindow?.FindName("BooksDataGrid") is DataGrid booksDataGrid)
            {
                if (booksDataGrid.Items.Count > 0)
                {
                    booksDataGrid.ScrollIntoView(booksDataGrid.Items[^1]);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable för att undvika CS8618

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
