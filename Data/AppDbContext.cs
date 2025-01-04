using Microsoft.EntityFrameworkCore;
using TheLittleBookNest.Model;

namespace TheLittleBookNest.Data
{
    public class AppDbContext : DbContext
    {
        // Tabeller (DbSet representerar en tabell i databasen)
        public DbSet<Book> Books { get; set; }

        // Konfigurera anslutningssträngen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER01;Database=bookstore;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Konfigurera tabellrelationer och regler (valfritt)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exempel: Konfigurera kolumnens maxlängd
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(200);

            // Exempel: Relationer mellan tabeller (om nödvändigt)
            // modelBuilder.Entity<Book>()
            //     .HasOne(b => b.Author)
            //     .WithMany(a => a.Books)
            //     .HasForeignKey(b => b.AuthorId);
        }
    }
}
