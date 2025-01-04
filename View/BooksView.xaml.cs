using System.Windows.Controls;
using TheLittleBookNest.ViewModel;

namespace TheLittleBookNest.View
{
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
            DataContext = new BooksViewModel(); // Sätt DataContext till BooksViewModel
        }
    }
}
