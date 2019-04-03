using coreBookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiOnlineBookStoreProject.Models
{
    public class OnlineBookStoreAPIDbContext : DbContext
    {
        private object b;

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publication> Publications { get; set; }
       public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TRD-512;Initial Catalog=api_Online_Bookdb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Customer>()
               .HasIndex(u => u.UserName)
               .IsUnique();

            modelBuilder
              .Entity<Admin>()
              .HasIndex(a => a.AdminUserName)
              .IsUnique();


            modelBuilder.Entity<OrderBook>
                (build =>
                {
                    build.HasKey(b => new { b.OrderId, b.BookId });
                }
                );
           }

    }
}
