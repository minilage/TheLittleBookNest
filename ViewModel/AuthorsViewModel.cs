using System.Collections.ObjectModel;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class AuthorsViewModel
    {
        public ObservableCollection<Author> Authors { get; set; }

        public AuthorsViewModel()
        {
            using (var context = new AppDbContext())
            {
                // Ladda alla författare från databasen
                Authors = new ObservableCollection<Author>(context.Authors.ToList());
            }
        }
    }
}
