using Microsoft.EntityFrameworkCore;

namespace PharmacyManagementSystem.Data
{
    public class PharmacyManagementSystemContext : DbContext
    {
        public PharmacyManagementSystemContext(DbContextOptions<PharmacyManagementSystemContext> options)
            : base(options)
        {
        }


        public DbSet<Models.User> User { get; set; } = default!;

        public DbSet<Models.Drug>? Drug { get; set; }

        public DbSet<Models.Order>? Order { get; set; }

        public DbSet<Models.Sales>? Sale { get; set; }

        public DbSet<Models.OrderDetail>? OrderDetail { get; set; }

        public DbSet<Models.SupplierDetails>? SupplierDetail { get; set; }

        public DbSet<Models.UserAddress>? UserAddress { get; set; }


    }
}
