using System.Collections.ObjectModel;
using TheLittleBookNest.Command;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class DeleteBookDialogViewModel : BaseViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        public Book? SelectedBook { get; set; }
        public Action? CloseDialog { get; set; }

        public RelayCommand ConfirmDeleteCommand { get; }

        public DeleteBookDialogViewModel(ObservableCollection<Book> books)
        {
            Books = books;

            // Kommandot för att radera boken
            ConfirmDeleteCommand = new RelayCommand(_ => ConfirmDelete(), _ => CanDelete());
        }

        private void ConfirmDelete()
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
                CloseDialog?.Invoke(); // Stänger dialogen efter borttagning
            }
        }

        private bool CanDelete() => SelectedBook != null;
    }
}
