namespace TheLittleBookNest.Model
{
    public class Publisher
    {
        public int ID { get; set; } // Primärnyckel


        public string Name { get; set; } = string.Empty;

        // För framtida användning
        // Kommentera ut PublisherName eftersom det ignoreras i Fluent API
        // public string PublisherName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Navigeringsproperty
        public ICollection<Book> Books { get; set; } = new List<Book>(); // En publisher kan ha många böcker
    }
}
