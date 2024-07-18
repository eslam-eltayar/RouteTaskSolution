using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;
using System.Reflection;

namespace System.Repository.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options)
        : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ConfigureOrderCustomerRelationship(modelBuilder);
            ConfigureOrderItemRelationship(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureOrderCustomerRelationship(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Order and Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)        
                .WithMany(c => c.Orders)        
                .HasForeignKey(o => o.CustomerId)  
                .OnDelete(DeleteBehavior.Restrict);  
        }

        private void ConfigureOrderItemRelationship(ModelBuilder modelBuilder)
        {
            // Configure the relationship between OrderItem and Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)             // OrderItem has one Order
                .WithMany(o => o.OrderItems)        // Order has many OrderItems
                .HasForeignKey(oi => oi.OrderId)   // Foreign key in OrderItem referencing OrderId
                .OnDelete(DeleteBehavior.Restrict); // Decide onDelete behavior as per your requirement
        }

        public DbSet<Customer> Custmors { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
