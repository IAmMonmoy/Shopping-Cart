using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shopping_Cart_Api.Models;

namespace Shopping_Cart_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BuyingList> BuyingList { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shipment> Shipments { get; set; }
        
        public DbSet<SoldList> SoldList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shipment>().HasKey(c => new{c.UserId , c.ProductId});
            base.OnModelCreating(builder);
        }
    }
}