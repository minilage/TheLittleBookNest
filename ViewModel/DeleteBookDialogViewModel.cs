using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;
using TheLittleBookNest.Services;

namespace TheLittleBookNest.ViewModel
{
    public class DeleteBookDialogViewModel : BaseViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        public Book? SelectedBook { get; set; }

        public ICommand ConfirmDeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteBookDialogViewModel(ObservableCollection<Book> books)
        {
            Books = books ?? throw new ArgumentNullException(nameof(books));
            ConfirmDeleteCommand = new RelayCommand(DeleteBook, CanDelete);
            CancelCommand = new RelayCommand(_ => CloseDialog());
        }

        private bool CanDelete(object? parameter) => SelectedBook != null;

        private void DeleteBook(object? parameter)
        {
            if (SelectedBook == null)
            {
                MessageBoxHelper.ShowMessage("Please select a book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBook.Title}'?",
                                         "Confirm Delete",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new AppDbContext();

                    // Hämta relaterade orderdetaljer och ta bort dem
                    var relatedOrderDetails = context.OrderDetails
                                                     .Where(od => od.ISBN == SelectedBook.ISBN13)
                                                     .ToList();

                    if (relatedOrderDetails.Any())
                    {
                        context.OrderDetails.RemoveRange(relatedOrderDetails);
                        context.SaveChanges(); // Spara ändringar för att undvika konflikt
                    }

                    // Ta bort boken
                    var bookToDelete = context.Books.FirstOrDefault(b => b.ISBN13 == SelectedBook.ISBN13);

                    if (bookToDelete != null)
                    {
                        context.Books.Remove(bookToDelete);
                        context.SaveChanges();

                        Books.Remove(SelectedBook); // Uppdatera UI
                        MessageBoxHelper.ShowMessage("Book deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxHelper.ShowMessage("Book not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private static void CloseDialog()
        {
            Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive)?.Close();
        }
    }
}
