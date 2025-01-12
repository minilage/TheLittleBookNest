namespace TheLittleBookNest.Model
{
    public class Inventory
    {
        public int ID { get; set; } // Primärnyckel
        public int StoreID { get; set; } // Utländsk nyckel till Stores
        public string ISBN { get; set; } = string.Empty; // Ändrat från ISBN13 till ISBN
        public int StockThreshold { get; set; } // För att hantera påfyllning
        public int Quantity { get; set; }

        // Navigeringsproperty
        public Store Store { get; set; } = null!; // Koppling till Store
        public Book Book { get; set; } = null!; // Koppling till Book
    }
}
