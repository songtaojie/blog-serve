using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.EFCore
{
    public class BlogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.AddCustomConvention();
            optionsBuilder.UseSqlServer("Server=.;Database=EFCoreTest;User ID=sa;Password=songtaojie;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTest>().Property(b => b.Active).HasPrecision(18, 3);
            modelBuilder.Entity<BlogTest>().Property(b => b.ActivePrecision).HasPrecision(18);
            modelBuilder.Entity<BlogTest>().HasData(new BlogTest
            {
                Id = Guid.NewGuid().ToString(),
                Active = 12M
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
