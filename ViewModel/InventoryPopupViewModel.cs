using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.ViewModel
{
    public class InventoryPopupViewModel : BaseViewModel
    {
        public ObservableCollection<Store> Stores { get; set; }
        public ObservableCollection<BookWithStock> Books { get; set; } = new ObservableCollection<BookWithStock>();
        public int Quantity { get; set; }
        public bool IsAdd { get; set; }
        public bool IsRemove { get; set; }

        private Store selectedStore = null!;
        public Store SelectedStore
        {
            get => selectedStore;
            set
            {
                if (selectedStore != value)
                {
                    selectedStore = value;
                    OnPropertyChanged(nameof(SelectedStore));

                    if (selectedStore != null)
                    {
                        try
                        {
                            LoadBooksWithStock(selectedStore.ID);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while loading books for the selected store: {ex.Message}");
                        }
                    }

                    // Återställ vald bok utan att trigga meddelandet
                    var firstBook = Books.FirstOrDefault();
                    if (firstBook != null)
                    {
                        selectedBook = firstBook;
                        // Ingen OnPropertyChanged här, för att undvika att "valid book"-meddelandet triggas
                    }
                    else
                    {
                        selectedBook = new BookWithStock
                        {
                            Title = "No Book Selected",
                            ISBN13 = string.Empty,
                            Quantity = 0
                        };
                    }
                    OnPropertyChanged(nameof(SelectedBook)); // Flyttad här för att undvika flera körningar av vald bok
                }
            }
        }

        private BookWithStock selectedBook = null!;
        public BookWithStock SelectedBook
        {
            get => selectedBook;
            set
            {
                // Undvik validering om `value` är null under laddning av böcker
                if (selectedBook != value)
                {
                    if (value != null)
                    {
                        selectedBook = value;
                        OnPropertyChanged(nameof(SelectedBook));
                    }
                    else if (!isLoadingBooks) // Validera endast om böcker inte håller på att laddas
                    {
                        MessageBox.Show("Please select a valid book.");
                    }
                }
            }
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public InventoryPopupViewModel(ObservableCollection<Store> stores, ObservableCollection<BookWithStock> books)
        {
            if (stores == null || !stores.Any())
                throw new ArgumentException("Stores collection cannot be null or empty.", nameof(stores));

            Stores = stores;
            Books = books ?? new ObservableCollection<BookWithStock>();

            SelectedStore = Stores.FirstOrDefault() ?? throw new InvalidOperationException("No stores available.");
            SelectedBook = Books.FirstOrDefault() ?? new BookWithStock();

            ConfirmCommand = new RelayCommand(ExecuteConfirm);
            CancelCommand = new RelayCommand(_ => CloseDialog());
        }

        private bool isLoadingBooks = false;

        public void LoadBooksWithStock(int selectedStoreId)
        {
            try
            {
                isLoadingBooks = true; // Starta laddningen
                using (var context = new AppDbContext())
                {
                    var booksWithStock = context.Books
                        .GroupJoin(
                            context.Inventory.Where(i => i.StoreID == selectedStoreId),
                            book => book.ISBN13,
                            inventory => inventory.ISBN13,
                            (book, inventory) => new BookWithStock
                            {
                                Title = book.Title,
                                ISBN13 = book.ISBN13,
                                Quantity = inventory.Select(i => i.Quantity).FirstOrDefault()
                            })
                        .ToList();

                    Books = new ObservableCollection<BookWithStock>(booksWithStock);
                    OnPropertyChanged(nameof(Books));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading books: {ex.Message}");
            }
            finally
            {
                isLoadingBooks = false; // Avsluta laddningen
            }
        }


        private void ExecuteConfirm(object? parameter)
        {
            // Kontrollera en gång att bokvalet är giltigt innan åtgärd
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a valid book before confirming.");
                return;
            }

            if (Quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            try
            {
                if (IsAdd)
                {
                    AddToInventory();
                }
                else if (IsRemove)
                {
                    RemoveFromInventory();
                }

                // Bekräftelsemeddelande kvarstår
                CloseDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void AddToInventory()
        {
            try
            {
                using var context = new AppDbContext();
                var inventoryItem = context.Inventory.FirstOrDefault(i => i.StoreID == SelectedStore.ID && i.ISBN13 == SelectedBook.ISBN13);

                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += Quantity;
                }
                else
                {
                    if (SelectedStore == null || SelectedBook == null)
                    {
                        MessageBox.Show("Invalid store or book selection.");
                        return;
                    }

                    context.Inventory.Add(new Inventory
                    {
                        StoreID = SelectedStore.ID,
                        ISBN13 = SelectedBook.ISBN13,
                        Quantity = Quantity
                    });
                }

                context.SaveChanges();
                LoadBooksWithStock(SelectedStore.ID);
                MessageBox.Show($"Added {Quantity} of '{SelectedBook.Title}' to {SelectedStore.StoreName}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding to inventory: {ex.Message}");
            }
        }

        private void RemoveFromInventory()
        {
            try
            {
                using var context = new AppDbContext();
                var inventoryItem = context.Inventory.FirstOrDefault(i => i.StoreID == SelectedStore.ID && i.ISBN13 == SelectedBook.ISBN13);

                if (inventoryItem != null)
                {
                    if (inventoryItem.Quantity >= Quantity)
                    {
                        inventoryItem.Quantity -= Quantity;
                        if (inventoryItem.Quantity == 0)
                            context.Inventory.Remove(inventoryItem);

                        context.SaveChanges();
                        LoadBooksWithStock(SelectedStore.ID);
                        MessageBox.Show($"Removed {Quantity} of '{SelectedBook.Title}' from {SelectedStore.StoreName}.");
                    }
                    else
                    {
                        MessageBox.Show("Not enough stock to remove the requested quantity.");
                    }
                }
                else
                {
                    MessageBox.Show($"'{SelectedBook.Title}' does not exist in inventory for {SelectedStore.StoreName}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while removing from inventory: {ex.Message}");
            }
        }

        private static void CloseDialog()
        {
            Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive)?.Close();
        }
    }
}
