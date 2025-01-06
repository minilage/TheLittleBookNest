namespace TheLittleBookNest.Model
{
    public class Customer
    {
        public int ID { get; set; } // Primärnyckel
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Navigeringsproperty
        public ICollection<Order> Orders { get; set; } = new List<Order>(); // En kund kan ha många ordrar
    }
}
