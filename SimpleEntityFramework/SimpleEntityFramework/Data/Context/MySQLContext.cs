using Microsoft.EntityFrameworkCore;
using SimpleEntityFramework.Data.Mapping;
using SimpleEntityFramework.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SimpleEntityFramework.Data.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public MySQLContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Items { get; set; }

    }

}
