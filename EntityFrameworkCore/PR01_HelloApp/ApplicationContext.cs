using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR01_HelloApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=myDataBase;Integrated Security=SSPI;");
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
            new User[]
            {
                new User { Id=1, Name="Slavic", Age=23}
            });
        }
    }
}
