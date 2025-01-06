namespace TheLittleBookNest.Model
{
    public class Publisher
    {
        public int ID { get; set; } // Primärnyckel
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigeringsproperty
        public ICollection<Book> Books { get; set; } // En publisher kan ha många böcker
    }
}
