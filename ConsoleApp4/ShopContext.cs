using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4
{
    public class ShopContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Make sure the database knows how to handle the duplicate address property
            modelBuilder.Entity<Address>().HasMany(x => x.ShippingAddressCustomers).WithOne(x => x.SpAddress)
                .HasForeignKey(x => x.ShippingAddress).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>().HasMany(x => x.BillingAddressCustomers).WithOne(x => x.BlAddress)
                .HasForeignKey(x => x.BillingAddress).OnDelete(DeleteBehavior.Restrict);
        }
    }
}