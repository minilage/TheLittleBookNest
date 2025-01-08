namespace TheLittleBookNest.Model
{
    public class Author
    {
        public int ID { get; set; } // Primärnyckel

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Beräknad egenskap för visning
        public string FullName => $"{FirstName} {LastName}";

        public DateTime BirthDate { get; set; }

        // Relation till böcker
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
