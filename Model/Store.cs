namespace TheLittleBookNest.Model
{
    public class Store
    {
        public int ID { get; set; }
        public string StoreName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigeringspropertyer
        public ICollection<Order> Orders { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<Employee> Employees { get; set; } // Lägg till detta
    }
}
