using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IVehicleTypeBusinessEntity
    {
        List<VehicleTypeDto> GetVehicleType();
        void AddVehicleType(VehicleTypeDto value);
        VehicleTypeDto GetType(int id);

        void DeleteVehicleType(int id);
        void UpdateVehicleType(VehicleTypeDto vehicleTypeDto);
        bool IsItemAvailable(string itemName, int itemID);
    }

    public class VehicleTypeBusinessEntity : IVehicleTypeBusinessEntity
    {
        private IVehicleTypeDataService iVehicleTypeDataService;
        public VehicleTypeBusinessEntity(IVehicleTypeDataService iVehicleTypeDataService)
        {
            this.iVehicleTypeDataService = iVehicleTypeDataService;
        }

        public List<VehicleTypeDto> GetVehicleType()
        {
            var vehicleType = iVehicleTypeDataService.GetVehicleType();

            var vehicleTypeDtoList = vehicleType.Select(p => new VehicleTypeDto() { ID = p.ID, Name = p.Name, Description = p.Description }).ToList();
            return vehicleTypeDtoList;
        }


        public void AddVehicleType(VehicleTypeDto value)
        {
            var vehicleType = new VehicleType();

            vehicleType.ID = value.ID;
            vehicleType.Name = value.Name;
            vehicleType.Description = value.Description;

            this.iVehicleTypeDataService.AddVehicleType(vehicleType);
        }


        public VehicleTypeDto GetType(int id)
        {
            var type = this.iVehicleTypeDataService.GetType(id);

            var typeDto = new VehicleTypeDto();

            typeDto.ID = type.ID;
            typeDto.Name = type.Name;
            typeDto.Description = type.Description;
            return typeDto;
        }

        public void DeleteVehicleType(int id)
        {
            var type = this.iVehicleTypeDataService.GetType(id);
            type.IsDeleted = true;
            this.iVehicleTypeDataService.SaveChanges();
        }


        public void UpdateVehicleType(VehicleTypeDto vehicleTypeDto)
        {
            var type = this.iVehicleTypeDataService.GetType(vehicleTypeDto.ID);
            type.ID = vehicleTypeDto.ID;
            type.Name = vehicleTypeDto.Name;
            type.Description = vehicleTypeDto.Description;

            this.iVehicleTypeDataService.SaveChanges();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.iVehicleTypeDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }
    }
}
