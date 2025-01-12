namespace TheLittleBookNest.Model
{
    public class BookWithStock
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public string DisplayText => $"{Title} ({Quantity})"; // Textformat för visning
    }

}
