using System.Windows.Controls;
using TheLittleBookNest.ViewModel;

namespace TheLittleBookNest.View
{
    /// <summary>
    /// Interaction logic for AuthorsView.xaml
    /// </summary>
    public partial class AuthorsView : UserControl
    {
        public AuthorsView()
        {
            InitializeComponent();
            DataContext = new AuthorsViewModel(); // Ställ in DataContext till AuthorsViewModel
        }
    }
}
