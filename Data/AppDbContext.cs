using Microsoft.EntityFrameworkCore;
using TheLittleBookNest.Model;
// Alias för att undvika konflikt med System.Security.Policy.Publisher
using Publisher = TheLittleBookNest.Model.Publisher;

namespace TheLittleBookNest.Data
{
    public class AppDbContext : DbContext
    {
        // Tabeller (DbSet representerar en tabell i databasen)
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Publisher> Publishers { get; set; } = null!;
        public DbSet<Inventory> Inventory { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        // Konfigurera anslutningssträngen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER01;Database=bookstore;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Konfigurera tabellrelationer och regler
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ange ISBN13 som primärnyckel för Book
            modelBuilder.Entity<Book>()
                .HasKey(b => b.ISBN13);

            // Konfigurera relationen mellan Books och Authors
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID);

            // Konfigurera relationen mellan Books och Publishers
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherID);

            // Resten av relationerna
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StoreID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Book)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(od => od.ISBN13);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Store)
                .WithMany(s => s.Inventory)
                .HasForeignKey(i => i.StoreID);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Book)
                .WithMany(b => b.Inventory)
                .HasForeignKey(i => i.ISBN13);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Store)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.StoreID);

            // Books-tabellen
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(200);

            base.OnModelCreating(modelBuilder);
        }
    }
}
