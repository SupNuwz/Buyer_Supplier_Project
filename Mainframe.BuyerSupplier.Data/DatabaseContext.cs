
using Mainframe.BuyerSupplier.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Mainframe.BuyerSupplier.Data
{

    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options) { }
        public DbSet<StandardInventory> StandardInventory { get; set; }
        public DbSet<Users> Users { get; set; }
 		public DbSet<SupplierBase> SupplierBase { get; set; }
        public DbSet<UserSupplierBase> UserSupplierBase { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<InventoryItemCategory> InventoryItemCategory { get; set; }
        public DbSet<InventoryItemSubCategory> InventoryItemSubCategory { get; set; }
        public DbSet<SupplierInventory> SupplierInventory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderAssignment> OrderAssignment { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
		public DbSet<DeliverySlots> DeliverySlots { get; set; }

        public DbSet<SupplierStandardInventory> SupplierStandardInventory { get; set; }
        public DbSet<TimeWave> TimeWave { get; set; }
        public DbSet<DeliveryCostConfiguration> DeliveryCostConfiguration { get; set; }
        public DbSet<FileServerDetail> FileServerDetail { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<DiscountConfiguration> DiscountConfiguration { get; set; }
        public DbSet<CommissionConfiguration> CommissionConfiguration { get; set; }
        public DbSet<WatchList> WatchList { get; set; }

    }
}

