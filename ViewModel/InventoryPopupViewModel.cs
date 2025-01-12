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

                    Console.WriteLine($"SelectedStore changed: {selectedStore?.StoreName} (ID: {selectedStore?.ID})");

                    if (selectedStore != null)
                    {
                        LoadBooksWithStock(selectedStore.ID);
                    }
                }
            }
        }

        private BookWithStock selectedBook = null!;
        public BookWithStock SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public InventoryPopupViewModel(ObservableCollection<Store> stores, ObservableCollection<BookWithStock> books)
        {
            Stores = stores ?? throw new ArgumentNullException(nameof(stores));
            Books = books ?? new ObservableCollection<BookWithStock>();

            Console.WriteLine($"Number of stores loaded: {Stores.Count}");
            foreach (var store in Stores)
            {
                Console.WriteLine($"Store: {store.StoreName} (ID: {store.ID})");
            }

            SelectedStore = Stores.FirstOrDefault() ?? throw new InvalidOperationException("No stores available.");
            SelectedBook = Books.FirstOrDefault() ?? new BookWithStock();

            ConfirmCommand = new RelayCommand(ExecuteConfirm);
            CancelCommand = new RelayCommand(_ => CloseDialog());
        }

        public void LoadBooksWithStock(int selectedStoreId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var booksWithStock = context.Books
                        .GroupJoin(
                            context.Inventory.Where(i => i.StoreID == selectedStoreId),
                            book => book.ISBN13,
                            inventory => inventory.ISBN,
                            (book, inventory) => new BookWithStock
                            {
                                Title = book.Title,
                                ISBN13 = book.ISBN13,
                                Quantity = inventory.Select(i => i.Quantity).FirstOrDefault()
                            })
                        .ToList();

                    Console.WriteLine($"Books loaded for StoreID {selectedStoreId}: {booksWithStock.Count} books");
                    foreach (var book in booksWithStock)
                    {
                        Console.WriteLine($"- {book.Title} (Quantity: {book.Quantity})");
                    }

                    Books = new ObservableCollection<BookWithStock>(booksWithStock);
                    OnPropertyChanged(nameof(Books));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading books: {ex.Message}");
            }
        }

        private void ExecuteConfirm(object? parameter)
        {
            if (Quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (IsAdd)
            {
                AddToInventory();
            }
            else if (IsRemove)
            {
                RemoveFromInventory();
            }

            CloseDialog();
        }

        private void AddToInventory()
        {
            using var context = new AppDbContext();
            var inventoryItem = context.Inventory.FirstOrDefault(i => i.StoreID == SelectedStore.ID && i.ISBN == SelectedBook.ISBN13);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity += Quantity;
            }
            else
            {
                context.Inventory.Add(new Inventory
                {
                    StoreID = SelectedStore.ID,
                    ISBN = SelectedBook.ISBN13,
                    Quantity = Quantity,
                    StockThreshold = 5
                });
            }

            context.SaveChanges();
            LoadBooksWithStock(SelectedStore.ID);
            MessageBox.Show($"Added {Quantity} of '{SelectedBook.Title}' to {SelectedStore.StoreName}.");
        }

        private void RemoveFromInventory()
        {
            using var context = new AppDbContext();
            var inventoryItem = context.Inventory.FirstOrDefault(i => i.StoreID == SelectedStore.ID && i.ISBN == SelectedBook.ISBN13);

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

        private static void CloseDialog()
        {
            Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.IsActive)?.Close();
        }
    }
}
