namespace TheLittleBookNest.Model
{
    public class Author
    {
        public int ID { get; set; } // Primärnyckel
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}