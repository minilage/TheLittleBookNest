namespace TheLittleBookNest.Model
{
    public class OrderDetail
    {
        public int ID { get; set; } // Primärnyckel
        public int OrderID { get; set; } // Utländsk nyckel till Orders

        public string ISBN { get; set; } = string.Empty; // Utländsk nyckel till Books

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigeringsproperty
        public Order Order { get; set; } = null!; // Koppling till Order
        public Book Book { get; set; } = null!; // Koppling till Book
    }
}
