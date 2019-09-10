using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;
using Mainframe.BuyerSupplier.Data.DataServices;

namespace Mainframe.BuyerSupplier.Data.Data_Services
{

    public interface IInventoryItemCategoriesDataService : IBaseDataService
    {
        IEnumerable<InventoryItemCategory> GetInventoryItemCategories();
        void AddCategory(InventoryItemCategory inventoryItemCategory);
        InventoryItemCategory GetCategory(int CategoryID);
        bool IsItemAvailable(string itemName, int itemID);
    }
    public class InventoryItemCategoriesDataService : BaseDataService, IInventoryItemCategoriesDataService
    {
        private DatabaseContext databaseContext;
        public InventoryItemCategoriesDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<InventoryItemCategory> GetInventoryItemCategories()
        {
            var inventoryItemCategory = (from c in databaseContext.InventoryItemCategory
                                where c.IsDeleted == false
                                select c).ToList();
            return inventoryItemCategory;
        }

        public void AddCategory(InventoryItemCategory inventoryItemCategory)
        {
            databaseContext.InventoryItemCategory.Add(inventoryItemCategory);
            databaseContext.SaveChanges();
        }

        public InventoryItemCategory GetCategory(int CategoryID)
        {
            var category = from c in databaseContext.InventoryItemCategory
                       where c.ID == CategoryID
                       select c;
            return category.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var inventoryItemCategoryItem = from s in databaseContext.InventoryItemCategory
                                            where ((s.Name == itemName)
                                                   && (itemID == 0 || (itemID != s.ID && s.IsDeleted == false)))
                                            select s;

            return inventoryItemCategoryItem.Any();
        }
    }
}

