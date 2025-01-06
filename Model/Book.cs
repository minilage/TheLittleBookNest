namespace TheLittleBookNest.Model
{
    public class Book
    {
        public string ISBN13 { get; set; } = string.Empty; // Primärnyckel
        public string Title { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public int AuthorID { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string BookFormat { get; set; } = string.Empty;
        public int? PublisherID { get; set; }

        // Navigeringspropertyer
        public Author Author { get; set; } = null!;
        public Publisher? Publisher { get; set; } // Publisher kan vara null
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail> { };
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory> { };

    }
}
