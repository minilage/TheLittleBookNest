﻿namespace TheLittleBookNest.Model
{
    public class Store
    {
        public int ID { get; set; } // Primärnyckel
        public string StoreName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigeringspropertyer
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>(); // Navigeringsproperty för Employees
        public int TotalInventory { get; set; }
    }
}