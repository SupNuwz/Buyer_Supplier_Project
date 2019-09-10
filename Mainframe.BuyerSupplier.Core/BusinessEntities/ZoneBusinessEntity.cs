using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IZoneBusinessEntity
    {
        List<ZoneDto> GetZones();
        void AddZone(ZoneDto value);
        ZoneDto GetZone(int id);
        void DeleteZone(int id);
        void UpdateZone(ZoneDto zoneDto);
    }
    public class ZoneBusinessEntity: IZoneBusinessEntity
    {
        private IZoneDataService iZoneDataService;
        ISupplierBaseBusinessEntity supplierBaseBusinessEntity;

        public ZoneBusinessEntity(IZoneDataService iZoneDataService,
            ISupplierBaseBusinessEntity supplierBaseBusinessEntity
            )
        {
            this.iZoneDataService = iZoneDataService;
            this.supplierBaseBusinessEntity = supplierBaseBusinessEntity;
        }

        public List<ZoneDto> GetZones()
        {
            var zone = iZoneDataService.GetAllZone();
            var supplierBase = supplierBaseBusinessEntity.GetAllActiveSupplierBases();

            var zoneDtoList = zone.Select(z => new ZoneDto()
            {
                ID = z.ID,
                SupplierBaseID = z.SupplierBaseID,
                Name = z.Name,
                Description = z.Description
            }).ToList();

            zoneDtoList.ForEach(z =>
            {
                var supplierBases = supplierBase.FirstOrDefault(s => s.SupplierBaseId == z.SupplierBaseID);
                if (supplierBases != null)
                {
                    z.SupplierBaseName = supplierBases.SupplierBaseName;
                }
            });
            return zoneDtoList;
        }

        public void AddZone(ZoneDto value)
        {
            var zone = new Zone();
            zone.ID = value.ID;
            zone.SupplierBaseID = value.SupplierBaseID;
            zone.Name = value.Name;
            zone.Description = value.Description;

            this.iZoneDataService.AddZone(zone);
        }

        public ZoneDto GetZone(int id)
        {
            var zones = this.iZoneDataService.GetZone(id);

            var zoneDto = new ZoneDto();
            zoneDto.ID = zones.ID;
            zoneDto.SupplierBaseID = zones.SupplierBaseID;
            zoneDto.Name = zones.Name;
            zoneDto.Description = zones.Description;

            var supplierBase = supplierBaseBusinessEntity.GetAllActiveSupplierBases();
            var supplierBases = supplierBase.FirstOrDefault(z => z.SupplierBaseName == zoneDto.SupplierBaseName);
            return zoneDto;

        }

        public void DeleteZone(int id)
        {
            var zones = this.iZoneDataService.GetZone(id);
            zones.IsDeleted = true;
            this.iZoneDataService.SaveChanges();
        }

        public void UpdateZone(ZoneDto zoneDto)
        {
            var zones = this.iZoneDataService.GetZone(zoneDto.ID);
            zones.ID = zoneDto.ID;
            zones.SupplierBaseID = zoneDto.SupplierBaseID;
            zones.Name = zoneDto.Name;
            zones.Description = zoneDto.Description;

            this.iZoneDataService.SaveChanges();
        }
    }
}
