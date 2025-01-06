using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

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

        public BooksViewModel()
        {
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

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable för att undvika CS8618

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
