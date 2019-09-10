using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface ISupplierBaseBusinessEntity
    {
        List<SupplierBaseDto> GetAllSupplierBases();
        List<SupplierBaseDto> GetAllActiveSupplierBases();
        void AddSupplierBase(SupplierBaseDto value);
        void DeleteSupplierBase(int value);

        SupplierBaseDto GetSupplierBase(int id);

        void UpdateSupplierBase(SupplierBaseDto supplierBaseDto);
        bool IsItemAvailable(string itemName, int itemID);


    }

    public class SupplierBusinessEntity : ISupplierBaseBusinessEntity
    {
        private ISupplierBaseService supplierService;

        public SupplierBusinessEntity(ISupplierBaseService supplierServices)
        {
            this.supplierService = supplierServices;
        }

        public List<SupplierBaseDto> GetAllSupplierBases()
        {
            var supplierBaseDtos = supplierService.GetAllActiveSupplierBases();

            var supplierBaseDtoList = supplierBaseDtos.Select(d => new SupplierBaseDto()
            { SupplierBaseId = d.SupplierBaseId, SupplierBaseName = d.SupplierBaseName, DeliverySlot = d.DeliverySlot }).ToList();

            return supplierBaseDtoList;
        }

        public List<SupplierBaseDto> GetAllActiveSupplierBases()
        {
            var supplierBaseDtos = supplierService.GetAllActiveSupplierBases();

            var supplierBaseDtoList = supplierBaseDtos.Select(d => new SupplierBaseDto()
            { SupplierBaseId = d.SupplierBaseId, SupplierBaseName = d.SupplierBaseName, DeliverySlot = d.DeliverySlot }).ToList();

            return supplierBaseDtoList;
        }
        public void AddSupplierBase(SupplierBaseDto value)
        {
            var supplierBase = new SupplierBase();

            supplierBase.SupplierBaseName = value.SupplierBaseName;
            supplierBase.DeliverySlot = value.DeliverySlot;

            this.supplierService.AddSupplierBase(supplierBase);
        }

        public void DeleteSupplierBase(int SupplierBaseId)
        {
            var supplierBase = this.supplierService.GetSupplierBase(SupplierBaseId);
            supplierBase.IsDeleted = true;

            this.supplierService.SaveChanges();
        }

        public SupplierBaseDto GetSupplierBase(int id)
        {
            var supplierBase = this.supplierService.GetSupplierBase(id);

            var supplierBaseDto = new SupplierBaseDto();
            supplierBaseDto.SupplierBaseId = supplierBase.SupplierBaseId;
            supplierBaseDto.SupplierBaseName = supplierBase.SupplierBaseName;
            supplierBaseDto.DeliverySlot = supplierBase.DeliverySlot;



            return supplierBaseDto;
        }

        public void UpdateSupplierBase(SupplierBaseDto supplierBaseDto)
        {
            var supplierBase = this.supplierService.GetSupplierBase(supplierBaseDto.SupplierBaseId);
            supplierBase.SupplierBaseName = supplierBaseDto.SupplierBaseName;
            supplierBase.DeliverySlot = supplierBaseDto.DeliverySlot;


            this.supplierService.SaveChanges();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.supplierService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }


    }
}
