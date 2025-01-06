namespace TheLittleBookNest.Model
{
    public class Customer
    {
        public int ID { get; set; } // Primärnyckel
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigeringsproperty
        public ICollection<Order> Orders { get; set; } // En kund kan ha många ordrar
    }
}

