namespace TheLittleBookNest.Model
{
    public class OrderDetail
    {
        public int ID { get; set; } // Primärnyckel
        public int OrderID { get; set; } // Utländsk nyckel till Orders
        public string ISBN13 { get; set; } // Utländsk nyckel till Books
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigeringsproperty
        public Order Order { get; set; } // Koppling till Order
        public Book Book { get; set; } // Koppling till Book
    }
}
