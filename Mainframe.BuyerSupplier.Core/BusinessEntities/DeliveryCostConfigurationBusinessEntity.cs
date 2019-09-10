using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
     public interface IDeliveryCostConfigurationBusinessEntity
    {
        List<DeliveryCostConfigurationDto> GetAllDeliveryCostConfiguration();
        List<DeliveryCostConfigurationDto> GetAllActiveDeliveryCostConfiguration();
        void AddDeliveryCostConfiguration(DeliveryCostConfigurationDto value);
        void DeleteDeliveryCostConfiguration(int value);

        DeliveryCostConfigurationDto GetDeliveryCostConfiguration(int id);

        void UpdateDeliveryCostConfiguration(DeliveryCostConfigurationDto deliveryCostConfigurationDto);
    }


    public class DeliveryCostConfigurationBusinessEntity : IDeliveryCostConfigurationBusinessEntity
    {
        private IDeliveryCostConfigurationDataService deliveryCostConfigurationService;
        ISupplierBaseService supplierBaseService;

        public DeliveryCostConfigurationBusinessEntity(IDeliveryCostConfigurationDataService deliveryCostConfigurationService,
                                                       ISupplierBaseService supplierBaseService)
        {
            this.deliveryCostConfigurationService = deliveryCostConfigurationService;
            this.supplierBaseService = supplierBaseService;
        }


        public List<DeliveryCostConfigurationDto> GetAllDeliveryCostConfiguration()
        {
            var deliveryCostConfigurationDtos = deliveryCostConfigurationService.GetAllActiveDeliveryCostConfiguration();
            var supplierBaseDtos = supplierBaseService.GetAllSupplierBases();

            var deliveryCostConfigurationDtoList = deliveryCostConfigurationDtos.Select(d => new DeliveryCostConfigurationDto()
            { ID = d.ID, Name = d.Name, Description = d.Description, BaseFare = d.BaseFare, BaseDistance = d.BaseDistance, AdditionalRate = d.AdditionalRate, BaseLocationID = d.BaseLocationID }).ToList();

            deliveryCostConfigurationDtoList.ForEach(p => {
                var supplierBase = supplierBaseDtos.FirstOrDefault(s => s.SupplierBaseId == p.BaseLocationID);

                if (supplierBase != null)
                {
                    p.BaseLocation = supplierBase.SupplierBaseName;
                }
            });

            return deliveryCostConfigurationDtoList;
        } 

        public List<DeliveryCostConfigurationDto> GetAllActiveDeliveryCostConfiguration()
        {
            var deliveryCostConfigurationDtos = deliveryCostConfigurationService.GetAllActiveDeliveryCostConfiguration();
            var supplierBaseDtos = supplierBaseService.GetAllSupplierBases();

            var deliveryCostConfigurationDtoList = deliveryCostConfigurationDtos.Select(d => new DeliveryCostConfigurationDto()
            { ID = d.ID, Name = d.Name, Description = d.Description, BaseFare = d.BaseFare, BaseDistance = d.BaseDistance, AdditionalRate = d.AdditionalRate, BaseLocationID = d.BaseLocationID }).ToList();

            deliveryCostConfigurationDtoList.ForEach(p => {
                var supplierBase = supplierBaseDtos.FirstOrDefault(s => s.SupplierBaseId == p.BaseLocationID);

                if (supplierBase != null)
                {
                    p.Name = supplierBase.SupplierBaseName;
                }
            });

            return deliveryCostConfigurationDtoList;
        }
        public void AddDeliveryCostConfiguration(DeliveryCostConfigurationDto value)
        {
            var deliveryCostConfiguration = new DeliveryCostConfiguration();

            deliveryCostConfiguration.Name = value.Name;
            deliveryCostConfiguration.Description = value.Description;
            deliveryCostConfiguration.BaseLocationID = value.BaseLocationID;
            deliveryCostConfiguration.BaseFare = value.BaseFare;
            deliveryCostConfiguration.BaseDistance = value.BaseDistance;
            deliveryCostConfiguration.AdditionalRate = value.AdditionalRate;


            this.deliveryCostConfigurationService.AddDeliveryCostConfiguration(deliveryCostConfiguration);
        }

        public void DeleteDeliveryCostConfiguration(int ID)
        {
            var deliveryCostConfiguration = this.deliveryCostConfigurationService.GetDeliveryCostConfiguration(ID);
            deliveryCostConfiguration.IsDeleted = true;

            this.deliveryCostConfigurationService.SaveChanges();
        }


        public DeliveryCostConfigurationDto GetDeliveryCostConfiguration(int id)
        {
            var deliveryCostConfiguration = this.deliveryCostConfigurationService.GetDeliveryCostConfiguration(id);

            var deliveryCostConfigurationDto = new DeliveryCostConfigurationDto();

            deliveryCostConfigurationDto.ID = deliveryCostConfiguration.ID;
            deliveryCostConfigurationDto.Name = deliveryCostConfiguration.Name;
            deliveryCostConfigurationDto.Description = deliveryCostConfiguration.Description;
            deliveryCostConfigurationDto.BaseLocationID = deliveryCostConfiguration.BaseLocationID;
            deliveryCostConfigurationDto.BaseFare = deliveryCostConfiguration.BaseFare;
            deliveryCostConfigurationDto.BaseDistance = deliveryCostConfiguration.BaseDistance;
            deliveryCostConfigurationDto.AdditionalRate = deliveryCostConfiguration.AdditionalRate;

            var baseSuplier = supplierBaseService.GetAllSupplierBases();
            var baseSuplierItem = baseSuplier.FirstOrDefault(p => p.SupplierBaseId == deliveryCostConfigurationDto.ID);
            if (baseSuplierItem != null)
            {
                deliveryCostConfigurationDto.BaseLocation = baseSuplierItem.SupplierBaseName;
            }

            return deliveryCostConfigurationDto;
        }


        public void UpdateDeliveryCostConfiguration(DeliveryCostConfigurationDto deliveryCostConfigurationDto)
        {
            var deliveryCostConfiguration = this.deliveryCostConfigurationService.GetDeliveryCostConfiguration(deliveryCostConfigurationDto.ID);
            deliveryCostConfiguration.Name = deliveryCostConfigurationDto.Name;
            deliveryCostConfiguration.Description = deliveryCostConfigurationDto.Description;
            deliveryCostConfiguration.BaseLocationID = deliveryCostConfigurationDto.BaseLocationID;
            deliveryCostConfiguration.BaseFare = deliveryCostConfigurationDto.BaseFare;
            deliveryCostConfiguration.BaseDistance = deliveryCostConfigurationDto.BaseDistance;
            deliveryCostConfiguration.AdditionalRate = deliveryCostConfigurationDto.AdditionalRate;

            this.deliveryCostConfigurationService.SaveChanges();
        }

    }
}
