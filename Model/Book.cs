namespace TheLittleBookNest.Model
{
    public class Book
    {
        public string ISBN13 { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public int AuthorID { get; set; }
        public string Genre { get; set; }
        public string BookFormat { get; set; }
        public int? PublisherID { get; set; }

        // Navigeringspropertyer
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
    }
}
