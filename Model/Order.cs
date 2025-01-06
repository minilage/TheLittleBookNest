namespace TheLittleBookNest.Model
{
    public class Order
    {
        public int ID { get; set; } // Primärnyckel
        public DateTime OrderDate { get; set; }
        public int StoreID { get; set; } // Utländsk nyckel till Stores
        public int CustomerID { get; set; } // Utländsk nyckel till Customers

        // Navigeringsproperty
        public Store Store { get; set; } = null!; // Koppling till Store
        public Customer Customer { get; set; } = null!; // Koppling till Customer
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // En order kan ha många detaljer
    }
}
