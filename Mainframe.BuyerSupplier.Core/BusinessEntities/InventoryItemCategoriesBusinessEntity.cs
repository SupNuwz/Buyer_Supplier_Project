using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IInventoryItemCategoriesBusinessEntity
    {
        List<InventoryItemCategoriesDto> GetInventoryItemCategories();
        void AddCategory(InventoryItemCategoriesDto value);

        InventoryItemCategoriesDto GetCategory(int id);
        void DeleteCategory(int id);
        void UpdateCategory(InventoryItemCategoriesDto inventoryItemCategoriesDto);
        bool IsItemAvailable(string itemName, int itemID);
    }
    public class InventoryItemCategoriesBusinessEntity : IInventoryItemCategoriesBusinessEntity
    {
        private IInventoryItemCategoriesDataService iInventoryItemCategoriesDataService;
        public InventoryItemCategoriesBusinessEntity(IInventoryItemCategoriesDataService iInventoryItemCategoriesDataService)
        {
            this.iInventoryItemCategoriesDataService = iInventoryItemCategoriesDataService;
        }

        public List<InventoryItemCategoriesDto> GetInventoryItemCategories()
        {
            var inventoryItemCategories = iInventoryItemCategoriesDataService.GetInventoryItemCategories();
            var inventoryItemCategoriesDtoList = inventoryItemCategories.Select(i => new InventoryItemCategoriesDto() { ID = i.ID, Name = i.Name, Description = i.Description }).ToList();
            return inventoryItemCategoriesDtoList;
        }

        public void AddCategory(InventoryItemCategoriesDto value)
        {
            var inventoryItemCategories = new InventoryItemCategory();

            inventoryItemCategories.ID = value.ID;
            inventoryItemCategories.Name = value.Name;
            inventoryItemCategories.Description = value.Description;

            this.iInventoryItemCategoriesDataService.AddCategory(inventoryItemCategories);

        }

        public InventoryItemCategoriesDto GetCategory(int id)
        {
            var category = this.iInventoryItemCategoriesDataService.GetCategory(id);

            var categoryDto = new InventoryItemCategoriesDto();

            categoryDto.ID = category.ID;
            categoryDto.Name = category.Name;
            categoryDto.Description = category.Description;
            return categoryDto;
        }

        public void DeleteCategory(int id)
        {
            var category = this.iInventoryItemCategoriesDataService.GetCategory(id);
            category.IsDeleted = true;
            this.iInventoryItemCategoriesDataService.SaveChanges();
        }

        public void UpdateCategory(InventoryItemCategoriesDto inventoryItemCategoriesDto)
        {
            var category = this.iInventoryItemCategoriesDataService.GetCategory(inventoryItemCategoriesDto.ID);

            category.ID = inventoryItemCategoriesDto.ID;
            category.Name = inventoryItemCategoriesDto.Name;
            category.Description = inventoryItemCategoriesDto.Description;

            this.iInventoryItemCategoriesDataService.SaveChanges();

        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.iInventoryItemCategoriesDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }
    }
}
