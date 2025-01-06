using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class AuthorsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Author> _authors = new ObservableCollection<Author>(); // Initierad för att undvika null
        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged(); // Använder CallerMemberName
            }
        }

        public AuthorsViewModel()
        {
            // Starta den asynkrona dataladdningen
            LoadAuthorsAsync().ConfigureAwait(false); // Förhindrar CS4014
        }

        private async Task LoadAuthorsAsync()
        {
            using (var context = new AppDbContext())
            {
                // Ladda författare asynkront
                var authors = await context.Authors.ToListAsync();
                Authors = new ObservableCollection<Author>(authors);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable för att matcha INotifyPropertyChanged

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
