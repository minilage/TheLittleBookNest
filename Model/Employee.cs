namespace TheLittleBookNest.Model
{
    public class Employee
    {
        public int ID { get; set; } // Primärnyckel
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int StoreID { get; set; } // Utländsk nyckel som kopplar till Stores

        // Navigeringsproperty
        public Store Store { get; set; } // Koppling till butiken där den anställde jobbar
    }
}
