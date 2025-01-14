using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;
using TheLittleBookNest.Services;

namespace TheLittleBookNest.ViewModel
{
    public class AddBookDialogViewModel : INotifyPropertyChanged
    {
        // Fält
        private string _isbn13 = string.Empty;
        public string ISBN13
        {
            get => _isbn13;
            set
            {
                _isbn13 = value;
                OnPropertyChanged();
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _language = string.Empty;
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        private Author? _selectedAuthor;
        public Author? SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
            }
        }

        private string? _selectedBookFormat;
        public string? SelectedBookFormat
        {
            get => _selectedBookFormat;
            set
            {
                _selectedBookFormat = value;
                OnPropertyChanged();
            }
        }

        // Författare och bokformat
        public ObservableCollection<Author> Authors { get; set; } = new ObservableCollection<Author>();
        public ObservableCollection<string> BookFormats { get; set; } = new ObservableCollection<string>
        {
            "Digital", "Pocket", "Hardcover", "E-Book"
        };

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public AddBookDialogViewModel()
        {
            SaveCommand = new RelayCommand(SaveBook, CanSave);
            CloseCommand = new RelayCommand(CloseDialog);

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ISBN13) ||
                    e.PropertyName == nameof(Title) ||
                    e.PropertyName == nameof(Price) ||
                    e.PropertyName == nameof(Language) ||
                    e.PropertyName == nameof(SelectedAuthor) ||
                    e.PropertyName == nameof(SelectedBookFormat))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            };

            // Hämta författare
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            using var context = new AppDbContext();
            {
                foreach (var author in context.Authors)
                {
                    Console.WriteLine($"Loaded Author: {author.FirstName} {author.LastName}");
                    Authors.Add(new Author
                    {
                        ID = author.ID,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                    });
                }
            }
        }


        private bool CanSave(object? parameter)
        {
#if DEBUG
            Console.WriteLine("=== CanSave Debug ===");
            Console.WriteLine($"ISBN13: '{ISBN13}', Length: {ISBN13.Length}, ValidDigits: {ISBN13.All(char.IsDigit)}");
            Console.WriteLine($"Title: '{Title}', Valid: {!string.IsNullOrWhiteSpace(Title)}");
            Console.WriteLine($"Price: {Price}, Valid: {Price > 0}");
            Console.WriteLine($"Language: '{Language}', Valid: {!string.IsNullOrWhiteSpace(Language)}");
            Console.WriteLine($"SelectedAuthor: {SelectedAuthor?.FullName}, Valid: {SelectedAuthor != null}");
            Console.WriteLine($"SelectedBookFormat: '{SelectedBookFormat}', Valid: {!string.IsNullOrWhiteSpace(SelectedBookFormat)}");
#endif

            return !string.IsNullOrWhiteSpace(ISBN13) &&
                   ISBN13.Length == 13 &&
                   ISBN13.All(char.IsDigit) &&
                   !string.IsNullOrWhiteSpace(Title) &&
                   Price > 0 &&
                   !string.IsNullOrWhiteSpace(Language) &&
                   SelectedAuthor != null &&
                   !string.IsNullOrWhiteSpace(SelectedBookFormat);
        }

        private void SaveBook(object? parameter)
        {
            if (!CanSave(parameter))
            {
                MessageBoxHelper.ShowMessage("Please fill in all required fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new AppDbContext())
            {
                var book = new Book
                {
                    ISBN13 = ISBN13,
                    Title = Title,
                    Price = Price,
                    Language = Language,
                    AuthorID = SelectedAuthor!.ID,
                    BookFormat = SelectedBookFormat!
                };

                context.Books.Add(book);
                context.SaveChanges();
            }

            MessageBoxHelper.ShowMessage("Book added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            CloseDialog(null);
        }

        private void CloseDialog(object? parameter)
        {
            Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive)?.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
