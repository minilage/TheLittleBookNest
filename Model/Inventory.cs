namespace TheLittleBookNest.Model
{
    public class Inventory
    {
        public int StoreID { get; set; } // Utländsk nyckel till Stores
        public string ISBN13 { get; set; } = string.Empty; // Ändrat från ISBN till ISBN13
        public int Quantity { get; set; }

        // Navigeringsproperty
        public Store Store { get; set; } = null!; // Koppling till Store
        public Book Book { get; set; } = null!; // Koppling till Book
    }
}
