using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface ISupplierInventoryDataService : IBaseDataService
    {
        IEnumerable<SupplierInventory> GetSupplierInventories();
        SupplierInventory GetSupplierInventory(int supplierInventoryID);
        void AddSupplierInventory(SupplierInventory supplierInventory);
        void UpdateSupplierInventory(SupplierInventory supplierInventory);
        List<SupplierInventory> GetSupplierWiseSupplierInventories(int userId);
        List<SupplierInventory> GetSupplierStandardInventoryWiseSupplierInventory(int supplierStandardInventoryId);

        List<SupplierInventory> GetSupplierInventoriesBySupplierIds(List<int> userIds);

        List<SupplierInventory> GetSupplierInventoriesBySupplierStandardInventoryIds(List<int> standardInventories);

    }
    public class SupplierInventoryDataService : BaseDataService, ISupplierInventoryDataService
    {
        private DatabaseContext databaseContext;

        public SupplierInventoryDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<SupplierInventory> GetSupplierInventories()
        {
            var supplierInventories = (from e in databaseContext.SupplierInventory.Include(s => s.SupplierStandardInventory)
                                       where e.IsDeleted == false 
                                       select e).ToList();
            return supplierInventories;
        }

        public SupplierInventory GetSupplierInventory(int supplierInventoryID)
        {
            var supplierInventory = from e in databaseContext.SupplierInventory
                       where e.Id == supplierInventoryID
                       select e;

            return supplierInventory.FirstOrDefault();
        }

        public void AddSupplierInventory(SupplierInventory supplierInventory)
        {
            databaseContext.SupplierInventory.Add(supplierInventory);
            databaseContext.SaveChanges();
        }

        public void UpdateSupplierInventory(SupplierInventory supplierInventory)
        {
            databaseContext.SupplierInventory.Update(supplierInventory);
            databaseContext.SaveChanges();
        }        

        public List<SupplierInventory> GetSupplierWiseSupplierInventories(int userId)
        {
            var supplierInventories = (from e in databaseContext.SupplierInventory
                                       where e.SupplierStandardInventory.SupplierId == userId && e.IsDeleted == false 
                                       select e).ToList();
            return supplierInventories;
        }

        public List<SupplierInventory> GetSupplierStandardInventoryWiseSupplierInventory(int supplierStandardInventoryId)
        {
            var supplierInventories = (from e in databaseContext.SupplierInventory
                                       where e.SupplierStandardInventoryId == supplierStandardInventoryId && e.AvailableQty > 0 && e.IsDeleted == false
                                       select e).ToList();
            return supplierInventories;
        }

        public List<SupplierInventory> GetSupplierInventoriesBySupplierIds(List<int> userIds)
        {
            var supplierInventories = (from e in databaseContext.SupplierInventory.Include(s => s.SupplierStandardInventory)
                                       where e.IsDeleted == false && userIds.Contains(e.SupplierStandardInventory.SupplierId) && e.SupplierStandardInventory.StandardInventory != null
                                       select e).ToList();
            return supplierInventories;
        }

        public List<SupplierInventory> GetSupplierInventoriesBySupplierStandardInventoryIds(List<int> supplierStandardInventories)
        {
            var supplierInventories = (from e in databaseContext.SupplierInventory.Include(s => s.SupplierStandardInventory)
                                       where e.IsDeleted == false && supplierStandardInventories.Contains(e.SupplierStandardInventoryId) 
                                       && e.SupplierStandardInventory.StandardInventory != null
                                       select e).ToList();
            return supplierInventories;
        }
    }
}
