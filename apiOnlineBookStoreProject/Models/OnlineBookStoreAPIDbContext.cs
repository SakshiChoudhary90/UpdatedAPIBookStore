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
        public OnlineBookStoreAPIDbContext()
        {

        }

        public OnlineBookStoreAPIDbContext(DbContextOptions<OnlineBookStoreAPIDbContext>options):base(options)
        {

        }

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
            //optionsBuilder.UseSqlServer("Data Source=TRD-512;Initial Catalog=api_Online_Bookdb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(e => e.PublicationName)
                .HasColumnName("PublicationName")
                .HasMaxLength(5)
                .IsUnicode(false);
            });
            base.OnModelCreating(modelBuilder);

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
