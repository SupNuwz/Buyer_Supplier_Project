using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IStandardInventoryDataService : IBaseDataService
    {
        IEnumerable<StandardInventory> GetStandardInventories();
        IEnumerable<StandardInventory> GetAllStandardInventories();
        void AddInventory(StandardInventory standardInventory);
        StandardInventory GetInventory(int InventoryID);

        bool IsItemAvailable(string itemName, int itemID);



    }
    public class StandardInventoryDataService : BaseDataService, IStandardInventoryDataService
    {
        private DatabaseContext databaseContext;
        public StandardInventoryDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<StandardInventory> GetStandardInventories()
        {
            var standaradInventory = (from e in databaseContext.StandardInventory
                                      where e.IsDeleted == false
                                      select e).ToList();
            return standaradInventory;
        }

        public IEnumerable<StandardInventory> GetAllStandardInventories()
        {
            var standaradInventories = (from e in databaseContext.StandardInventory
                                      select e).ToList();
            return standaradInventories;
        }


        public void AddInventory(StandardInventory standardInventory)
        { 
            databaseContext.StandardInventory.Add(standardInventory);
            databaseContext.SaveChanges();
        }
        public StandardInventory GetInventory(int InventoryID)
        {
            var inventory = from e in databaseContext.StandardInventory
                            where e.ID == InventoryID
                            select e;

            return inventory.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var inventoryItem= from s in databaseContext.StandardInventory
                    where ((s.ItemName == itemName) 
                          && (itemID == 0 || ( itemID != s.ID && s.IsDeleted == false) ))
                   select s;

            return inventoryItem.Any();
    }
    }
}