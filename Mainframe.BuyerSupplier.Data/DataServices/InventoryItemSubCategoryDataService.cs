using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.Data_Services
{
    public interface IInventoryItemSubCategoryService : IBaseDataService
    {
        IEnumerable<InventoryItemSubCategory> GetAllInventoryItemSubCategory();
        IEnumerable<InventoryItemSubCategory> GetAllActiveInventoryItemSubCategory();
        InventoryItemSubCategory GetInventoryItemSubCategory(int SupplierBaseId);
        void AddInventoryItemSubCategory(InventoryItemSubCategory InventoryItemSubCategory);
    }
   public class InventoryItemSubCategoryDataService : BaseDataService, IInventoryItemSubCategoryService
    {
        private DatabaseContext dataContext;

        public InventoryItemSubCategoryDataService(DatabaseContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<InventoryItemSubCategory> GetAllInventoryItemSubCategory()
        {
            var InventoryItemSubCategory = (from e in dataContext.InventoryItemSubCategory
                                select e).ToList();
            return InventoryItemSubCategory;
        }

        public IEnumerable<InventoryItemSubCategory> GetAllActiveInventoryItemSubCategory()
        {
            var InventoryItemSubCategory = (from e in dataContext.InventoryItemSubCategory
                                where e.IsDeleted == false
                                select e).ToList();
            return InventoryItemSubCategory;
        }

        public void AddInventoryItemSubCategory(InventoryItemSubCategory InventoryItemSubCategory)
        {
            dataContext.InventoryItemSubCategory.Add(InventoryItemSubCategory);
            dataContext.SaveChanges();
        }



        public InventoryItemSubCategory GetInventoryItemSubCategory(int ID)
        {
            var InventoryItemSubCategory = from e in dataContext.InventoryItemSubCategory
                               where e.ID == ID
                                           select e;

            return InventoryItemSubCategory.FirstOrDefault();
        }

    }
}
