using System.Windows;
using TheLittleBookNest.ViewModel;

namespace TheLittleBookNest.View
{
    public partial class InventoryPopup : Window
    {
        public InventoryPopup(InventoryPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Sätter DataContext till det som skickas in
        }
    }
}
