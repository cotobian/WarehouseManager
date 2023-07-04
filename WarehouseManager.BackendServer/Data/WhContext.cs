using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data
{
    public class WhContext : DbContext
    {
        public WhContext(DbContextOptions<WhContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<CurrentPosition> CurrentPositions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ExtraReceiptBarcode> ExtraReceiptBarcodes { get; set; }
        public DbSet<ExtraReceiptDetail> ExtraReceiptDetails { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<ForkliftJob> ForkliftJobs { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public DbSet<ReceiptOrder> ReceiptOrders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<TallyJob> TallyJobs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseDoor> WarehouseDoors { get; set; }
        public DbSet<WarehousePosition> WarehousePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}