using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class StoresViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Store> _stores = new ObservableCollection<Store>(); // Initierad
        public ObservableCollection<Store> Stores
        {
            get => _stores;
            set
            {
                _stores = value;
                OnPropertyChanged(); // Använder CallerMemberName
            }
        }

        public StoresViewModel()
        {
            // Starta den asynkrona dataladdningen
            LoadStoresAsync().ConfigureAwait(false); // Förhindrar CS4014
        }

        private async Task LoadStoresAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Ladda butiker asynkront
                    var stores = await context.Stores.ToListAsync();
                    Stores = new ObservableCollection<Store>(stores);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stores: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable för att matcha INotifyPropertyChanged

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
