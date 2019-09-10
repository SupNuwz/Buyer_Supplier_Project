using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface ISupplierInventoryBusinessEntity
    {
        List<SupplierInventoryDto> GetSupplierInventories();
        SupplierInventoryDto GetSupplierInventory(int id);
        void AddSupplierInventory(SupplierInventoryDto value);
        void UpdateSupplierInventory(SupplierInventoryDto userDto);
        void DeleteSupplierInventory(int userId);
        List<SupplierInventoryDto> GetSupplierWiseSupplierInventories(int userId);

        List<SupplierInventoryDto> GetSupplierStandardInventoryWiseSupplierInventory(int supplierStandardInventoryId);

    }
    public class SupplierInventoryBusinessEntity : ISupplierInventoryBusinessEntity
    {
        private ISupplierInventoryDataService supplierInventoryDataService;       
        private IStandardInventoryDataService standardInventoryBusinessEntity;
        private IUserService userService;

        public SupplierInventoryBusinessEntity(ISupplierInventoryDataService supplierInventoryDataService,
            IStandardInventoryDataService standardInventoryBusinessEntity,           
            IUserService userService)
        {
            this.supplierInventoryDataService = supplierInventoryDataService;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
            this.userService = userService;
        }

        public List<SupplierInventoryDto> GetSupplierInventories()
        {
            

            var sandardInventory = standardInventoryBusinessEntity.GetStandardInventories();    

            var suppliers = this.userService.GetUsers().Where(p=> p.UserType == "Supplier");

            var supplierInventoryList = supplierInventoryDataService.GetSupplierInventoriesBySupplierIds(suppliers.Select(p=> p.ID).ToList());

            var supplierInventoryDtoList = supplierInventoryList.Select(r => new SupplierInventoryDto()
            {
                ID = r.Id,
                SupplierStandardInventoryId = r.SupplierStandardInventoryId,
                Qty = r.Qty,
                UnitPrice = r.UnitPrice,
                AvailableQty = r.AvailableQty,
                ProcessingQty = r.ProcessingQty,
                InventoryDate = r.InventoryDate,
                SupplierName = suppliers.FirstOrDefault(p => p.ID == r.SupplierStandardInventory.SupplierId).Name,
                InventoryItemName = r.SupplierStandardInventory.StandardInventory  == null ? "" : r.SupplierStandardInventory.StandardInventory.ItemName

            }).ToList();

            //supplierInventoryDtoList.ForEach(p =>
            //{
            //    var supplierInventoryItem = supplierInventoryList.FirstOrDefault(s => s.Id == p.ID);

            //    var standardInventory = sandardInventory.FirstOrDefault(i => i.ID == supplierInventoryItem.SupplierStandardInventory.StandardInventoryId);
            //    if (standardInventory != null)
            //        p.InventoryItemName = standardInventory.ItemName;
            //});

            return supplierInventoryDtoList.OrderBy(p=> p.SupplierName).ToList();
        }

        public SupplierInventoryDto GetSupplierInventory(int id)
        {
            var supplierInventory = this.supplierInventoryDataService.GetSupplierInventory(id);

            var supplierInventoryDto = new SupplierInventoryDto
            {
                ID = supplierInventory.Id,
                SupplierStandardInventoryId = supplierInventory.SupplierStandardInventoryId,
                Qty = supplierInventory.Qty,
                UnitPrice = supplierInventory.UnitPrice,
                AvailableQty = supplierInventory.AvailableQty,
                ProcessingQty = supplierInventory.ProcessingQty,
                InventoryDate = supplierInventory.InventoryDate
            };
            return supplierInventoryDto;
        }

        public void AddSupplierInventory(SupplierInventoryDto value)
        {
            var supplierInventory = new SupplierInventory
            {

                Id = value.ID,
                SupplierStandardInventoryId = value.SupplierStandardInventoryId,
                Qty = value.Qty,
                UnitPrice = value.UnitPrice,
                AvailableQty = value.AvailableQty,
                ProcessingQty = value.ProcessingQty,
                InventoryDate = DateTime.Now
            };

            this.supplierInventoryDataService.AddSupplierInventory(supplierInventory);
        }        

        public void UpdateSupplierInventory(SupplierInventoryDto supplierInventoryDto)
        {
            var supplierInventory = this.supplierInventoryDataService.GetSupplierInventory(supplierInventoryDto.ID);
            supplierInventory.Id = supplierInventoryDto.ID;
            supplierInventory.SupplierStandardInventoryId = supplierInventoryDto.SupplierStandardInventoryId;
            supplierInventory.Qty = supplierInventoryDto.Qty;
            supplierInventory.UnitPrice = supplierInventoryDto.UnitPrice;
            supplierInventory.AvailableQty = supplierInventoryDto.AvailableQty;
            supplierInventory.ProcessingQty = supplierInventoryDto.ProcessingQty;

            this.supplierInventoryDataService.UpdateSupplierInventory(supplierInventory);
        }

        public void DeleteSupplierInventory(int supplierInventoryId)
        {
            var supplierInventory = this.supplierInventoryDataService.GetSupplierInventory(supplierInventoryId);
            supplierInventory.IsDeleted = false;            
            this.supplierInventoryDataService.UpdateSupplierInventory(supplierInventory);
        }

        public List<SupplierInventoryDto> GetSupplierWiseSupplierInventories(int userId)
        {
            var supplierInventoryDtos = supplierInventoryDataService.GetSupplierWiseSupplierInventories(userId);
            var supplierInventoryDtoList = supplierInventoryDtos.Select(r => new SupplierInventoryDto()
            {
                ID = r.Id,
                SupplierStandardInventoryId = r.SupplierStandardInventoryId,
                Qty = r.Qty,
                UnitPrice = r.UnitPrice,
                AvailableQty = r.AvailableQty,
                ProcessingQty = r.ProcessingQty,
                InventoryDate = r.InventoryDate
            }).ToList();
            return supplierInventoryDtoList;
        }

        public List<SupplierInventoryDto> GetSupplierStandardInventoryWiseSupplierInventory(int supplierStandardInventoryId)
        {
            var supplierInventoryDtos = supplierInventoryDataService.GetSupplierStandardInventoryWiseSupplierInventory(supplierStandardInventoryId);
            var supplierInventoryDtoList = supplierInventoryDtos.Select(r => new SupplierInventoryDto()
            {
                ID = r.Id,
                SupplierStandardInventoryId = r.SupplierStandardInventoryId,
                Qty = r.Qty,
                UnitPrice = r.UnitPrice,
                AvailableQty = r.AvailableQty,
                ProcessingQty = r.ProcessingQty,
                InventoryDate = r.InventoryDate
            }).ToList();
            return supplierInventoryDtoList;
        }
    }
}
