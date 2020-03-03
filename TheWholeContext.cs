using Microsoft.EntityFrameworkCore;
using TaskMediatrFluentValidation.Entity;

namespace TaskMediatrFluentValidation
{
    public class TheWholeContext : DbContext
    {
        public TheWholeContext(DbContextOptions<TheWholeContext> options): base(options) { }
        public DbSet<Customer> CustomersAcc { get; set; }
        public DbSet<CustomerPaymentCards> Cards { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Products> Productsbuy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerPaymentCards>()
                        .HasOne(j => j.customer)
                        .WithMany()
                        .HasForeignKey(j => j.customer_id);
            modelBuilder.Entity<Products>()
                        .HasOne(p => p.merchant)
                        .WithMany()
                        .HasForeignKey(p => p.merchant_id);
        }

    }
}