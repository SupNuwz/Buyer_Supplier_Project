using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IVehicleBusinessEntity
    {
        List<VehicleDto> GetAllVehicle();
        List<VehicleDto> GetAllActiveVehicle();
        void AddVehicle(VehicleDto value);
        void DeleteVehicle(int value);

        VehicleDto GetVehicle(int id);

        void UpdateVehicle(VehicleDto vehicleDto);
    }


    public class VehicleBusinessEntity : IVehicleBusinessEntity
    {
        private IVehicleDataService vehicleService;
        ISupplierBaseService supplierBaseService;
        IVehicleTypeDataService vehicleTypeDataService;

        public VehicleBusinessEntity(IVehicleDataService vehicleService,
                                     ISupplierBaseService supplierBaseService,
                                     IVehicleTypeDataService vehicleTypeDataService)
        {
            this.vehicleService = vehicleService;
            this.supplierBaseService = supplierBaseService;
            this.vehicleTypeDataService = vehicleTypeDataService;
        }


        public List<VehicleDto> GetAllVehicle()
        {
            var vehicleDtos = vehicleService.GetAllActiveVehicle();
            var supplierBaseDtos = supplierBaseService.GetAllSupplierBases();
            var vehicleTypeDtos = vehicleTypeDataService.GetVehicleType();

            var vehicleDtoList = vehicleDtos.Select(d => new VehicleDto()
            { ID = d.ID, SupplierBaseId = d.SupplierBaseId, DriverContactNo = d.DriverContactNo, NumberPlate = d.NumberPlate, VehicleTypeId = d.VehicleTypeId, ColorCode = d.ColorCode, MaximumCapacity = d.MaximumCapacity, Availability = d.Availability }).ToList();

            vehicleDtoList.ForEach(p => {
                var supplierBase = supplierBaseDtos.FirstOrDefault(s => s.SupplierBaseId == p.SupplierBaseId);

                if (supplierBase != null)
                {
                    p.SupplierBase = supplierBase.SupplierBaseName;
                }
            });

            vehicleDtoList.ForEach(p => {
                var vehicleType = vehicleTypeDtos.FirstOrDefault(s => s.ID == p.VehicleTypeId);

                if (vehicleType != null)
                {
                    p.VehicleType = vehicleType.Name;
                }
            });

            return vehicleDtoList;
        }

        public List<VehicleDto> GetAllActiveVehicle()
        {
            var vehicleDtos = vehicleService.GetAllActiveVehicle();
            var supplierBaseDtos = supplierBaseService.GetAllSupplierBases();
            var vehicleTypeDtos = vehicleTypeDataService.GetVehicleType();

            var vehicleDtoList = vehicleDtos.Select(d => new VehicleDto()
            { ID = d.ID, SupplierBaseId = d.SupplierBaseId, DriverContactNo = d.DriverContactNo, NumberPlate = d.NumberPlate, VehicleTypeId = d.VehicleTypeId, ColorCode = d.ColorCode, MaximumCapacity = d.MaximumCapacity, Availability = d.Availability }).ToList();

            vehicleDtoList.ForEach(p => {
                var supplierBase = supplierBaseDtos.FirstOrDefault(s => s.SupplierBaseId == p.SupplierBaseId);

                if (supplierBase != null)
                {
                    p.SupplierBase = supplierBase.SupplierBaseName;
                }
            });

            vehicleDtoList.ForEach(p => {
                var vehicleType = vehicleTypeDtos.FirstOrDefault(s => s.ID == p.VehicleTypeId);

                if (vehicleType != null)
                {
                    p.VehicleType = vehicleType.Name;
                }
            });

            return vehicleDtoList;
        }
        public void AddVehicle(VehicleDto value)
        {
            var vehicle = new Vehicle();

            vehicle.SupplierBaseId = value.SupplierBaseId;
            vehicle.DriverContactNo = value.DriverContactNo;
            vehicle.NumberPlate = value.NumberPlate;
            vehicle.VehicleTypeId = value.VehicleTypeId;
            vehicle.ColorCode = value.ColorCode;
            vehicle.MaximumCapacity = value.MaximumCapacity;
            vehicle.Availability = value.Availability;

            this.vehicleService.AddVehicle(vehicle);
        }

        public void DeleteVehicle(int ID)
        {
            var vehicle = this.vehicleService.GetVehicle(ID);
            vehicle.IsDeleted = true;

            this.vehicleService.SaveChanges();
        }


        public VehicleDto GetVehicle(int id)
        {
            var vehicle = this.vehicleService.GetVehicle(id);

            var vehicleDto = new VehicleDto();

            vehicleDto.ID = vehicle.ID;
            vehicleDto.SupplierBaseId = vehicle.SupplierBaseId;
            vehicleDto.DriverContactNo = vehicle.DriverContactNo;
            vehicleDto.NumberPlate = vehicle.NumberPlate;
            vehicleDto.VehicleTypeId = vehicle.VehicleTypeId;
            vehicleDto.ColorCode = vehicle.ColorCode;
            vehicleDto.MaximumCapacity = vehicle.MaximumCapacity;
            vehicleDto.Availability = vehicle.Availability;

            var baseSuplier = supplierBaseService.GetAllSupplierBases();
            var baseSuplierItem = baseSuplier.FirstOrDefault(p => p.SupplierBaseId == vehicleDto.ID);
            if (baseSuplierItem != null)
            {
                vehicleDto.SupplierBase = baseSuplierItem.SupplierBaseName;
            }

            var vehicleType = vehicleTypeDataService.GetVehicleType();
            var vehicleTypeItem = vehicleType.FirstOrDefault(p => p.ID == vehicleDto.VehicleTypeId);
            if (baseSuplierItem != null)
            {
                vehicleDto.VehicleType = vehicleTypeItem.Name;
            }


            return vehicleDto;
        }


        public void UpdateVehicle(VehicleDto vehicleDto)
        {
            var vehicle = this.vehicleService.GetVehicle(vehicleDto.ID);
            vehicle.SupplierBaseId = vehicleDto.SupplierBaseId;
            vehicle.DriverContactNo = vehicleDto.DriverContactNo;
            vehicle.NumberPlate = vehicleDto.NumberPlate;
            vehicle.VehicleTypeId = vehicleDto.VehicleTypeId;
            vehicle.ColorCode = vehicleDto.ColorCode;
            vehicle.MaximumCapacity = vehicleDto.MaximumCapacity;
            vehicle.Availability = vehicleDto.Availability;

            this.vehicleService.SaveChanges();
        }

    }
}