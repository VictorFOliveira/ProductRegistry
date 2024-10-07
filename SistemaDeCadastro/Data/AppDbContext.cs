using Microsoft.EntityFrameworkCore;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductModel>().ToTable("Products");
            modelBuilder.Entity<ProductModel>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductModel>().Property(p => p.Price).HasColumnType("decimal(18, 2");

            modelBuilder.Entity<UserModel>().ToTable("Users");
        }
    }
}
