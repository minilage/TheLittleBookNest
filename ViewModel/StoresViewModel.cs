using System.Collections.ObjectModel;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class StoresViewModel
    {
        public ObservableCollection<Store> Stores { get; set; }

        public StoresViewModel()
        {
            using (var context = new AppDbContext())
            {
                // Ladda alla butiker från databasen
                Stores = new ObservableCollection<Store>(context.Set<Store>().ToList());
            }
        }
    }
}
