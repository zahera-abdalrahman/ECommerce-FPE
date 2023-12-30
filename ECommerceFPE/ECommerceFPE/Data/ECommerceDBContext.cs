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

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

<<<<<<< Updated upstream
=======
        public DbSet<CartItems> CartItems { get; set; }

        public DbSet<CreditCard> CreditCard { get; set; }


        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<ReviewAll> ReviewAll { get; set; }

        public DbSet<Images> ImagesProducts { get; set; }
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
>>>>>>> Stashed changes

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity related entities (if needed)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDBContext).Assembly);
            modelBuilder.Entity<Customer>()
       .HasOne(c => c.CreditCard)
       .WithOne(cc => cc.Customer)
       .HasForeignKey<CreditCard>(cc => cc.CustomerId);

            modelBuilder.Entity<OrderItems>().HasNoKey();

            modelBuilder.Entity<ProductCatalog>().Ignore(pc => pc.OrderItems);


            

            //modelBuilder.Entity<OrderItems>().HasKey(o => o.OrderItemsId);
            // Add your additional configurations here if necessary
        }
    }
}
