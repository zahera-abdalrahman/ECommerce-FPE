using ECommerceFPE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFPE.Data
{
    public class ECommerceDBContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options)
        {

        }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartItems> CartItems { get; set; }

        //public DbSet<CreditCard> CreditCard { get; set; }


        //public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<ReviewAll> ReviewAll { get; set; }

        //public DbSet<Images> ImagesProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity related entities (if needed)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDBContext).Assembly);
           
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(c => c.CreditCard)
            //    .WithOne(cc => cc.ApplicationUser)
            //    .HasForeignKey<CreditCard>(cc => cc.CustomerId);
        // Add your additional configurations here if necessary
    }
    }
}
