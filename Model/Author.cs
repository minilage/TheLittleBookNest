namespace TheLittleBookNest.Model
{
    public class Author
    {
        public int ID { get; set; } // Primärnyckel

        public string FirstName { get; set; } = string.Empty; // Säkerställer att egenskapen inte är null
        public string LastName { get; set; } = string.Empty;  // Säkerställer att egenskapen inte är null
        public DateTime BirthDate { get; set; } // Hanterar DateTime direkt utan ändringar

        // Relation till böcker
        public ICollection<Book> Books { get; set; } = new List<Book> { }; // Initieras för att undvika null
    }
}
