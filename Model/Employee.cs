namespace TheLittleBookNest.Model
{
    public class Employee
    {
        public int ID { get; set; } // Primärnyckel
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int StoreID { get; set; } // Utländsk nyckel som kopplar till Stores

        // Navigeringsproperty
        public Store Store { get; set; } = null!; // Koppling till butiken där den anställde jobbar
    }
}
