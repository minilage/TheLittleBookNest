namespace TheLittleBookNest.Model
{
    public class Order
    {
        public int ID { get; set; } // Primärnyckel
        public DateTime OrderDate { get; set; }
        public int StoreID { get; set; } // Utländsk nyckel till Stores
        public int CustomerID { get; set; } // Utländsk nyckel till Customers

        // Navigeringsproperty
        public Store Store { get; set; } // Koppling till Store
        public Customer Customer { get; set; } // Koppling till Customer
        public ICollection<OrderDetail> OrderDetails { get; set; } // En order kan ha många detaljer
    }
}
