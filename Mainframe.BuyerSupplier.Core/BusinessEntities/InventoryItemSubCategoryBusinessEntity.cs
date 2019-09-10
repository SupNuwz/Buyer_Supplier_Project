using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IInventoryItemSubCategoryBusinessEntity
    {
        List<InventoryItemSubCategoryDto> GetAllInventoryItemSubCategory();
        List<InventoryItemSubCategoryDto> GetAllActiveInventoryItemSubCategory();
        void AddInventoryItemSubCategory(InventoryItemSubCategoryDto value);
        void DeleteInventoryItemSubCategory(int value);

        InventoryItemSubCategoryDto GetInventoryItemSubCategory(int id);

        void UpdateInventoryItemSubCategory(InventoryItemSubCategoryDto inventoryItemSubCategoryDto);
    }
   public class InventoryItemSubCategoryBusinessEntity: IInventoryItemSubCategoryBusinessEntity
    {
        private IInventoryItemSubCategoryService inventoryItemSubCategoryService;
         IInventoryItemCategoriesDataService inventoryItemCategoryService;

        public InventoryItemSubCategoryBusinessEntity(IInventoryItemSubCategoryService inventoryItemSubCategoryServices, 
                                                     IInventoryItemCategoriesDataService inventoryItemCategoryService)
        {
            this.inventoryItemSubCategoryService = inventoryItemSubCategoryServices;
            this.inventoryItemCategoryService = inventoryItemCategoryService;
        }

        public List<InventoryItemSubCategoryDto> GetAllInventoryItemSubCategory()
        {
            var inventoryItemSubCategoryDtos = inventoryItemSubCategoryService.GetAllActiveInventoryItemSubCategory();
            var inventoryItemCategoryDtos = inventoryItemCategoryService.GetInventoryItemCategories();

            var inventoryItemSubCategoryDtoList = inventoryItemSubCategoryDtos.Select(d => new InventoryItemSubCategoryDto()
            { ID = d.ID, Name = d.Name, Description = d.Description, InventoryItemCategoryID = d.InventoryItemCategoryID }).ToList();

            inventoryItemSubCategoryDtoList.ForEach(p => {
                var inventoryItemSCategory = inventoryItemCategoryDtos.FirstOrDefault(s => s.ID == p.InventoryItemCategoryID);

                if (inventoryItemSCategory != null)
                {
                    p.CategoryName = inventoryItemSCategory.Name;
                }
            });

            return inventoryItemSubCategoryDtoList;
        }

        public List<InventoryItemSubCategoryDto> GetAllActiveInventoryItemSubCategory()
        {
            var inventoryItemSubCategoryDtos = inventoryItemSubCategoryService.GetAllActiveInventoryItemSubCategory();
            var inventoryItemCategoryDtos = inventoryItemCategoryService.GetInventoryItemCategories();

            var inventoryItemSubCategoryDtoList = inventoryItemSubCategoryDtos.Select(d => new InventoryItemSubCategoryDto()
            { ID = d.ID, Name = d.Name, Description = d.Description, InventoryItemCategoryID = d.InventoryItemCategoryID }).ToList();

                        inventoryItemSubCategoryDtoList.ForEach(p => {
                var inventoryItemSCategory = inventoryItemCategoryDtos.FirstOrDefault(s => s.ID == p.InventoryItemCategoryID);

                if (inventoryItemSCategory != null)
                {
                    p.CategoryName = inventoryItemSCategory.Name;
                }
            });

            return inventoryItemSubCategoryDtoList;
        }
        public void AddInventoryItemSubCategory(InventoryItemSubCategoryDto value)
        {
            var inventoryItemSubCategory = new InventoryItemSubCategory();

            inventoryItemSubCategory.Name = value.Name;
            inventoryItemSubCategory.Description = value.Description;
            inventoryItemSubCategory.InventoryItemCategoryID = value.InventoryItemCategoryID;


            this.inventoryItemSubCategoryService.AddInventoryItemSubCategory(inventoryItemSubCategory);
        }

        public void DeleteInventoryItemSubCategory(int ID)
        {
            var inventoryItemSubCategory = this.inventoryItemSubCategoryService.GetInventoryItemSubCategory(ID);
            inventoryItemSubCategory.IsDeleted = true;

            this.inventoryItemSubCategoryService.SaveChanges();
        }

        public InventoryItemSubCategoryDto GetInventoryItemSubCategory(int id)
        {
            var inventoryItemSubCategory = this.inventoryItemSubCategoryService.GetInventoryItemSubCategory(id);

            var inventoryItemSubCategoryDto = new InventoryItemSubCategoryDto();
            inventoryItemSubCategoryDto.ID = inventoryItemSubCategory.ID;
            inventoryItemSubCategoryDto.Name = inventoryItemSubCategory.Name;
            inventoryItemSubCategoryDto.Description = inventoryItemSubCategory.Description;
            inventoryItemSubCategoryDto.InventoryItemCategoryID = inventoryItemSubCategory.InventoryItemCategoryID;

            var inventoryItemCategories = inventoryItemCategoryService.GetInventoryItemCategories();
            var inventoryItemCategory = inventoryItemCategories.FirstOrDefault(p => p.ID == inventoryItemSubCategoryDto.ID);
            if (inventoryItemCategory != null)
            {
                inventoryItemSubCategoryDto.CategoryName = inventoryItemCategory.Name;
            }

            return inventoryItemSubCategoryDto;
        }

        public void UpdateInventoryItemSubCategory(InventoryItemSubCategoryDto inventoryItemSubCategoryDto)
        {
            var inventoryItemSubCategory = this.inventoryItemSubCategoryService.GetInventoryItemSubCategory(inventoryItemSubCategoryDto.ID);
            inventoryItemSubCategory.Name = inventoryItemSubCategoryDto.Name;
            inventoryItemSubCategory.Description = inventoryItemSubCategoryDto.Description;
            inventoryItemSubCategory.InventoryItemCategoryID = inventoryItemSubCategoryDto.InventoryItemCategoryID;

           this.inventoryItemSubCategoryService.SaveChanges();
        }


    }
}
