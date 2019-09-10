using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface ISupplierStandardInventoryService: IBaseDataService
    {
        IEnumerable<SupplierStandardInventory> GetSupplierStandardInventories();
        void AddSupplierStandaradInventory(List<SupplierStandardInventory> supplierStandardInventory);
        SupplierStandardInventory GetSupplierStandardInventory(int supplierStandardInventoryID);

        IEnumerable<SupplierStandardInventory> GetSupplierStandardInventoriesBySupplierId(int supplierId);
    }
    public class SupplierStandardInventoryService : BaseDataService, ISupplierStandardInventoryService
    {
        private DatabaseContext databaseContext;
        public SupplierStandardInventoryService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public IEnumerable<SupplierStandardInventory> GetSupplierStandardInventories()
        {
            var supplierStandaradInventory = (from e in databaseContext.SupplierStandardInventory
                                      where e.IsDeleted == false
                                      select e).ToList();
            return supplierStandaradInventory;
        }
        public void AddSupplierStandaradInventory(List<SupplierStandardInventory> supplierStandardInventory)
        {
            databaseContext.SupplierStandardInventory.AddRange(supplierStandardInventory);
            databaseContext.SaveChanges();
        }
        public SupplierStandardInventory GetSupplierStandardInventory (int supplierStandardInventoryID)
        {
            var supplierStandardInventory = from e in databaseContext.SupplierStandardInventory
                            where e.Id == supplierStandardInventoryID
                            select e;

            return supplierStandardInventory.FirstOrDefault();
        }

        public IEnumerable<SupplierStandardInventory> GetSupplierStandardInventoriesBySupplierId(int supplierId)
        {
            var supplierStandaradInventory = (from e in databaseContext.SupplierStandardInventory.Include(s => s.StandardInventory)
                                              where e.SupplierId == supplierId && e.IsDeleted == false 
                                             select e).OrderByDescending (x=>x.IsSelected).
                                              ToList();
            return supplierStandaradInventory;
        }
    }
}
