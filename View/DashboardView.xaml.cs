using System.Windows.Controls;

namespace TheLittleBookNest.View
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new ViewModel.DashboardViewModel(); // Kopplar ViewModel
        }
    }
}
