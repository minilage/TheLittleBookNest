namespace TheLittleBookNest.Model
{
    public class Inventory
    {
        public int ID { get; set; } // Primärnyckel
        public int StoreID { get; set; } // Utländsk nyckel till Stores
        public string ISBN13 { get; set; } = string.Empty; // Utländsk nyckel till Books
        public int StockLevel { get; set; }
        public int StockThreshold { get; set; } // För att hantera påfyllning

        // Navigeringsproperty
        public Store Store { get; set; } = null!; // Koppling till Store
        public Book Book { get; set; } = null!; // Koppling till Book
    }
}
