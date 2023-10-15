using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProducName = "Iphone 13", Price = 60000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 2, ProducName = "Iphone 14", Price = 70000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 3, ProducName = "Iphone 15", Price = 80000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 4, ProducName = "Iphone 16", Price = 90000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 5, ProducName = "Iphone 17", Price = 95000, IsActive = true });

        }                

        public DbSet<Product> Products { get; set; }




    }



}