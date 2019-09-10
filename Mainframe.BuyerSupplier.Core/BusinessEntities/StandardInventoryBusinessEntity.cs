using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.Models;
using Mainframe.BuyerSupplier.Common.Utility;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{

    public interface IStandardInventoryBusinessEntity
    {
        List<StandardInventoryDto> GetStandardInventories();
        List<StandardInventoryDto> GetAllStandardInventories();


        void AddInventory(StandardInventoryDto value);
        StandardInventoryDto GetInventory(int id);
        void DeleteInventory(int employeeId);
        void UpdateInventory(StandardInventoryDto standardInventoryDto);
        bool IsItemAvailable(string itemName, int itemID);


    }
    public class StandardInventoryBusinessEntity : IStandardInventoryBusinessEntity
    {
        private IStandardInventoryDataService istandardInventoryDataService;
        IUnitOfMeasureBusinessEntity unitOfMeasureBusinessEntity;
        IFileServerBusinessEntity fileServerBusinessEntity;
        IInventoryItemCategoriesBusinessEntity inventoryItemCategoriesBusinessEntity;
        IInventoryItemSubCategoryBusinessEntity inventoryItemSubCategoryBusinessEntity;

        public StandardInventoryBusinessEntity(IStandardInventoryDataService istandardInventoryDataService,
            IUnitOfMeasureBusinessEntity unitOfMeasureBusinessEntity,
            IFileServerBusinessEntity fileServerBusinessEntity,
            IInventoryItemCategoriesBusinessEntity inventoryItemCategoriesBusinessEntity,
            IInventoryItemSubCategoryBusinessEntity inventoryItemSubCategoryBusinessEntity
            )
        {
            this.istandardInventoryDataService = istandardInventoryDataService;
            this.unitOfMeasureBusinessEntity = unitOfMeasureBusinessEntity;
            this.fileServerBusinessEntity = fileServerBusinessEntity;
            this.inventoryItemCategoriesBusinessEntity = inventoryItemCategoriesBusinessEntity;
            this.inventoryItemSubCategoryBusinessEntity = inventoryItemSubCategoryBusinessEntity;
        }

        public List<StandardInventoryDto> GetStandardInventories()
        {
            var standardInventory = istandardInventoryDataService.GetStandardInventories();
            var unitOfMeasures = unitOfMeasureBusinessEntity.GetUnitOfMeasure();
            var inventoryItemCategories = inventoryItemCategoriesBusinessEntity.GetInventoryItemCategories();
            var inventoryItemSubCategories = inventoryItemSubCategoryBusinessEntity.GetAllActiveInventoryItemSubCategory();


            var standardInventoryDtoList = standardInventory.Select(p => new StandardInventoryDto() {
                ID = p.ID,
                ItemName = p.ItemName,
                InventoryItemCategoryId = p.InventoryItemCategoryId,
                InventoryItemSubCategoryId = p.InventoryItemSubCategoryId,
                QuantityUnitOfMesureId = p.QuantityUnitOfMesureId,
                Seasonality = p.Seasonality,
                MinimumInventory = p.MinimumInventory,
                FileID =p.FileServerDetailID }).ToList();

                var fileDetails = fileServerBusinessEntity.GetFileDetails(standardInventory.Select(p=> p.FileServerDetailID));

            standardInventoryDtoList.ForEach(p =>
            {
                var unitOfMeasure = unitOfMeasures.FirstOrDefault(s => s.ID == p.QuantityUnitOfMesureId);

                if (unitOfMeasure != null)
                {
                    p.QuantityUnitOfMeasureName = unitOfMeasure.Name;
                }

                var fileDto = fileDetails.FirstOrDefault(f=> f.ID == p.FileID);

                if (fileDto != null)
                {

                    var url = FileServerUtility.GetPresignedUrl(fileDto.BucketName, fileDto.Key).Result;

                    p.FileUrl = url;
                }

                var inventoryItemCategory = inventoryItemCategories.FirstOrDefault(i => i.ID == p.InventoryItemCategoryId);

                if (inventoryItemCategory != null)
                {
                    p.InventoryItemCategoryName = inventoryItemCategory.Name;
                }

                var inventoryItemSubCategory = inventoryItemSubCategories.FirstOrDefault(c => c.ID == p.InventoryItemSubCategoryId);

                if (inventoryItemSubCategory != null)
                {
                    p.InventoryItemSubCategoryName = inventoryItemSubCategory.Name;
                }
            });


            return standardInventoryDtoList;
        }

        public List<StandardInventoryDto> GetAllStandardInventories()
        {
            var standardInventory = istandardInventoryDataService.GetStandardInventories();
            var unitOfMeasures = unitOfMeasureBusinessEntity.GetUnitOfMeasure();


            var standardInventoryDtoList = standardInventory.Select(p => new StandardInventoryDto()
            {
                ID = p.ID,
                ItemName = p.ItemName,
                InventoryItemCategoryId = p.InventoryItemCategoryId,
                InventoryItemSubCategoryId = p.InventoryItemSubCategoryId,
                QuantityUnitOfMesureId = p.QuantityUnitOfMesureId,
                Seasonality = p.Seasonality,
                MinimumInventory = p.MinimumInventory,
                FileID = p.FileServerDetailID
            }).ToList();

            var fileDetails = fileServerBusinessEntity.GetFileDetails(standardInventory.Select(p => p.FileServerDetailID));

            standardInventoryDtoList.ForEach(p =>
            {
                var unitOfMeasure = unitOfMeasures.FirstOrDefault(s => s.ID == p.QuantityUnitOfMesureId);

                if (unitOfMeasure != null)
                {
                    p.QuantityUnitOfMeasureName = unitOfMeasure.Name;
                }

                var fileDto = fileDetails.FirstOrDefault(f => f.ID == p.FileID);

                if (fileDto != null)
                {

                    var url = FileServerUtility.GetPresignedUrl(fileDto.BucketName, fileDto.Key).Result;

                    p.FileUrl = url;
                }
            });


            return standardInventoryDtoList;
        }


        public void AddInventory(StandardInventoryDto value)
        {
            var standardInventory = new StandardInventory();

            standardInventory.ID = value.ID;
            standardInventory.ItemName = value.ItemName;
            standardInventory.InventoryItemCategoryId = value.InventoryItemCategoryId;
            standardInventory.InventoryItemSubCategoryId = value.InventoryItemSubCategoryId;
            standardInventory.QuantityUnitOfMesureId = value.QuantityUnitOfMesureId;
            standardInventory.Seasonality = value.Seasonality;
            standardInventory.MinimumInventory = value.MinimumInventory;
            standardInventory.FileServerDetailID = value.FileID;

         
            this.istandardInventoryDataService.AddInventory(standardInventory);
        }

        public StandardInventoryDto GetInventory(int id)
        {
            var inventory = this.istandardInventoryDataService.GetInventory(id);

            var inventryDto = new StandardInventoryDto();

            inventryDto.ID = inventory.ID;
            inventryDto.ItemName = inventory.ItemName;
            inventryDto.InventoryItemCategoryId = inventory.InventoryItemCategoryId;
            inventryDto.InventoryItemSubCategoryId = inventory.InventoryItemSubCategoryId;
            inventryDto.QuantityUnitOfMesureId = inventory.QuantityUnitOfMesureId;
            inventryDto.Seasonality = inventory.Seasonality;
            inventryDto.MinimumInventory = inventory.MinimumInventory;
            inventryDto.FileID = inventory.FileServerDetailID;

            var unitOfMeasures = unitOfMeasureBusinessEntity.GetUnitOfMeasure();
            var unitOfMeasure = unitOfMeasures.FirstOrDefault(p => p.Name == inventryDto.QuantityUnitOfMeasureName);
            var inventoryItemCategories = inventoryItemCategoriesBusinessEntity.GetInventoryItemCategories();
            var inventoryItemCategory = inventoryItemCategories.FirstOrDefault(p => p.Name == inventryDto.InventoryItemCategoryName);
            var inventoryItemSubCategories = inventoryItemSubCategoryBusinessEntity.GetAllActiveInventoryItemSubCategory();
            var inventoeyItemSubCategory = inventoryItemSubCategories.FirstOrDefault(p => p.Name == inventryDto.InventoryItemSubCategoryName);
            return inventryDto;
        }

        public void DeleteInventory(int id)
        {
            var inventory = this.istandardInventoryDataService.GetInventory(id);
            inventory.IsDeleted = true;
            this.istandardInventoryDataService.SaveChanges();
        }

        public void UpdateInventory(StandardInventoryDto standardInventoryDto)
        {
            var inventory = this.istandardInventoryDataService.GetInventory(standardInventoryDto.ID);
            inventory.ID = standardInventoryDto.ID;
            inventory.ItemName = standardInventoryDto.ItemName;
            inventory.InventoryItemCategoryId = standardInventoryDto.InventoryItemCategoryId;
            inventory.InventoryItemSubCategoryId = standardInventoryDto.InventoryItemSubCategoryId;
            inventory.QuantityUnitOfMesureId = standardInventoryDto.QuantityUnitOfMesureId;
            inventory.Seasonality = standardInventoryDto.Seasonality;
            inventory.MinimumInventory = standardInventoryDto.MinimumInventory;
            inventory.FileServerDetailID = standardInventoryDto.FileID;

            this.istandardInventoryDataService.SaveChanges();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.istandardInventoryDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
    }

    }
}
