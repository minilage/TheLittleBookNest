using System.Windows.Controls;
using System.Windows.Input;
using TheLittleBookNest.ViewModel;

namespace TheLittleBookNest.View
{
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
            DataContext = new BooksViewModel(); // Sätt DataContext till BooksViewModel

            // Lyssna på tangenttryckningar för Page Down
            this.PreviewKeyDown += BooksView_PreviewKeyDown;
        }

        private void BooksView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown)
            {
                // Scrolla till sista objektet i DataGrid
                if (BooksDataGrid.Items.Count > 0)
                {
                    BooksDataGrid.ScrollIntoView(BooksDataGrid.Items[^1]);
                }
            }
        }
    }
}
