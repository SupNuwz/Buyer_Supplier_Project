using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.DataServices;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface ISupplierStandardInventoryBusinessEntity
    {
        void AddSupplierStandardInventory(SupplierStandardInventoryDto[] supplierStandardInventoryDto);
        List<SupplierStandardInventoryDto> GetSupplierStandardInventories(int supplierId);
        List<SupplierStandardInventoryDto> GetSelectedSupplierStandardInventories(int supplierId);
        SupplierStandardInventoryDto GetSupplierStandardInventory(int id);
        void DeleteSupplierstandardInventory(int id);
        void UpdateSupplierstandardInventory(SupplierStandardInventoryDto[] supplierStandardInventoryDto);
        List<SupplierStandardInventoryDto> GetInitialSupplierStandardInventories();

    }
    public class SupplierStandardInventoryBusinessEntity: ISupplierStandardInventoryBusinessEntity
    {
        private ISupplierStandardInventoryService supplierStandardInventoryService;
        private IStandardInventoryBusinessEntity standardInventoryBusinessEntity;

        public SupplierStandardInventoryBusinessEntity(ISupplierStandardInventoryService supplierStandardInventoryService,
                                                        IStandardInventoryBusinessEntity standardInventoryBusinessEntity)
        {
            this.supplierStandardInventoryService = supplierStandardInventoryService;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
        }
        public List<SupplierStandardInventoryDto> GetSupplierStandardInventories(int supplierId)
        {
            var supplierStandardInventory = supplierStandardInventoryService.GetSupplierStandardInventoriesBySupplierId(supplierId);
            var supplierstandardInventoryDtoList = supplierStandardInventory.Select(p => 
            new SupplierStandardInventoryDto()
            { Id = p.Id, StandardInventoryId=p.StandardInventoryId ,SupplierId=p.SupplierId, InventoryItemCategoryId = p.StandardInventory.InventoryItemCategoryId, InventoryItemName = p.StandardInventory.ItemName, IsSelected = p.IsSelected
            }).ToList();
            return supplierstandardInventoryDtoList;
        }

        public List<SupplierStandardInventoryDto> GetSelectedSupplierStandardInventories(int supplierId)
        {
            var supplierStandardInventory = supplierStandardInventoryService.GetSupplierStandardInventoriesBySupplierId(supplierId);
            var supplierstandardInventoryDtoList = supplierStandardInventory.Where(r=>r.IsSelected && !r.IsDeleted).Select(p =>
            new SupplierStandardInventoryDto()
            {
                Id = p.Id,
                StandardInventoryId = p.StandardInventoryId,
                SupplierId = p.SupplierId,
                InventoryItemCategoryId = p.StandardInventory.InventoryItemCategoryId,
                InventoryItemName = p.StandardInventory.ItemName,
                IsSelected = p.IsSelected
            }).ToList();
            return supplierstandardInventoryDtoList;
        }

        public void AddSupplierStandardInventory(SupplierStandardInventoryDto[] supplierStandardInventoryDtoList)
        {

            var supplierStandardInventoryList = new List<SupplierStandardInventory>();

            foreach (var item in supplierStandardInventoryDtoList)
            {
                var supplierStandardInventory = new SupplierStandardInventory();

                supplierStandardInventory.Id = item.Id;
                supplierStandardInventory.StandardInventoryId = item.StandardInventoryId;
                supplierStandardInventory.SupplierId = item.SupplierId;
                supplierStandardInventory.IsSelected = item.IsSelected;

                supplierStandardInventoryList.Add(supplierStandardInventory);
            }


            this.supplierStandardInventoryService.AddSupplierStandaradInventory(supplierStandardInventoryList);

        }
        public SupplierStandardInventoryDto GetSupplierStandardInventory(int id)
        {
            var supplierStandardInventory = this.supplierStandardInventoryService.GetSupplierStandardInventory(id);

            var supplierStandardInventoryDto = new SupplierStandardInventoryDto();
          
           supplierStandardInventoryDto.Id = supplierStandardInventory.Id;
           supplierStandardInventoryDto.StandardInventoryId = supplierStandardInventory.StandardInventoryId;
           supplierStandardInventoryDto.SupplierId = supplierStandardInventory.SupplierId;
        
            return supplierStandardInventoryDto;
        }

        public void DeleteSupplierstandardInventory(int id)
        {
            var supplierStandardinventory = this.supplierStandardInventoryService.GetSupplierStandardInventory(id);
            supplierStandardinventory.IsDeleted = true;
            this.supplierStandardInventoryService.SaveChanges();
        }

        public void UpdateSupplierstandardInventory(SupplierStandardInventoryDto[] supplierStandardInventoryDto)
        {
            if (supplierStandardInventoryDto != null)
            {
                var supplierStandardInventory = this.supplierStandardInventoryService.GetSupplierStandardInventoriesBySupplierId(supplierStandardInventoryDto.First().SupplierId);

                foreach (var item in supplierStandardInventory)
                {
                    var supplierStandardInventoryDtoItem = supplierStandardInventoryDto.FirstOrDefault(p => p.Id == item.Id);

                    item.IsSelected = supplierStandardInventoryDtoItem.IsSelected;
                }

                this.supplierStandardInventoryService.SaveChanges();
            }
        }

        public List<SupplierStandardInventoryDto> GetInitialSupplierStandardInventories()
        {
            var standardinventory = this.standardInventoryBusinessEntity.GetStandardInventories();

            var supplierStandardinventory = new List<SupplierStandardInventoryDto>();

            standardinventory.ForEach(p => supplierStandardinventory.Add(
                    new SupplierStandardInventoryDto()
                    { InventoryItemCategoryId = p.InventoryItemCategoryId, InventoryItemName = p.ItemName, StandardInventoryId = p.ID }));

            return supplierStandardinventory;
        }
    }
}
