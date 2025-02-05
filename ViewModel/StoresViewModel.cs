﻿using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheLittleBookNest.Command;
using TheLittleBookNest.Data;
using TheLittleBookNest.Model;
using TheLittleBookNest.View;

namespace TheLittleBookNest.ViewModel
{
    public class StoresViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Store> _stores = new();
        private ObservableCollection<Book> _books = new();

        public ObservableCollection<Store> Stores
        {
            get => _stores;
            set
            {
                _stores = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenInventoryPopupCommand => new RelayCommand(_ =>
        {
            // Konvertera Books till BookWithStock med Quantity = 0 som standard
            var booksWithStock = new ObservableCollection<BookWithStock>(
                Books.Select(book => new BookWithStock
                {
                    Title = book.Title,
                    ISBN13 = book.ISBN13,
                    Quantity = 0 // Standardvärde
                })
            );

            // Skapa en instans av InventoryPopupViewModel med Stores och BooksWithStock
            var viewModel = new InventoryPopupViewModel(Stores, booksWithStock);

            // Skapa popup och skicka med ViewModel
            var popup = new InventoryPopup(viewModel);
            popup.ShowDialog();

            // Uppdatera TotalInventory efter popupen
            if (viewModel.SelectedStore != null)
            {
                UpdateStoreInventory(viewModel.SelectedStore.ID);
            }
        });

        public StoresViewModel()
        {
            LoadStoresAsync().ConfigureAwait(false);
            LoadBooksAsync().ConfigureAwait(false);
        }

        private async Task LoadStoresAsync()
        {
            try
            {
                using var context = new AppDbContext();
                {
                    var stores = await context.Stores
                        .Select(s => new Store
                        {
                            ID = s.ID,
                            StoreName = s.StoreName,
                            Street = s.Street,
                            City = s.City,
                            PostalCode = s.PostalCode,
                            Country = s.Country,
                            ContactPerson = s.ContactPerson,
                            Phone = s.Phone,
                            Email = s.Email,
                            TotalInventory = s.Inventory.Sum(i => i.Quantity)
                        }).ToListAsync();

                    Stores = new ObservableCollection<Store>(stores);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stores: {ex.Message}");
            }
        }

        private async Task LoadBooksAsync()
        {
            try
            {
                using var context = new AppDbContext();
                {
                    var books = await context.Books.ToListAsync();
                    Books = new ObservableCollection<Book>(books);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
            }
        }

        /// <summary>
        /// Uppdaterar TotalInventory för en viss butik.
        /// </summary>
        private void UpdateStoreInventory(int storeId)
        {
            try
            {
                using var context = new AppDbContext();
                var store = Stores.FirstOrDefault(s => s.ID == storeId);
                if (store != null)
                {
                    store.TotalInventory = context.Inventory
                        .Where(i => i.StoreID == storeId)
                        .Sum(i => i.Quantity);

                    OnPropertyChanged(nameof(Stores));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating store inventory: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
