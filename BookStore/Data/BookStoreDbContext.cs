using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    /// <summary>
    /// DB context class to create code first database for book store
    /// </summary>
    public class BookStoreDbContext:DbContext
    {
        /// <summary>
        /// Consturctor for dependency management
        /// It will send a builder to base class
        /// </summary>
        /// <param name="options"></param>
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Publisher> Publisher { get; set; }

        /// <summary>
        /// Method to define relations between tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasOne(p => p.Category)
                        .WithMany(c => c.Books)
                        .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
