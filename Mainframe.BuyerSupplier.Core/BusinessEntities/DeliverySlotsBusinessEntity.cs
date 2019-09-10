using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IDeliverySlotsBusinessEntity
    {
        List<DeliverySlotsDto> GetDeliverySlots();
        void AddSlot(DeliverySlotsDto value);
        DeliverySlotsDto GetSlot(int id);
        void DeleteSlot(int id);
        void UpdateSlot(DeliverySlotsDto deliverySlotDto);
        bool IsItemAvailable(string itemName, int itemID);
    }

    public class DeliverySlotsBusinessEntity : IDeliverySlotsBusinessEntity
    {
        private IDeliverySlotsDataService iDeliverySlotsDataService;

        public DeliverySlotsBusinessEntity(IDeliverySlotsDataService iDeliverySlotDataService)
        {
            this.iDeliverySlotsDataService = iDeliverySlotDataService;
        }

        public List<DeliverySlotsDto> GetDeliverySlots()
        {
            var deliverySlot = iDeliverySlotsDataService.GetDeliverySlots();
            var deliverySlotDtoList = deliverySlot.Select(d => new DeliverySlotsDto()
            {
                ID = d.ID,
                SlotName = d.SlotName,
                FirstWaveTime = d.FirstWaveTime,
                SecondWaveTime = d.SecondWaveTime,
                StartTime = d.StartTime,
                CutoffTime = d.CutoffTime,
                CountdownTime = d.CountdownTime,
                OrderAcceptTime = d.OrderAcceptTime,
                OrderCofirmTime = d.OrderCofirmTime,
                DispatchesConfirmTime = d.DispatchesConfirmTime,
                EndTime = d.EndTime
            }).ToList();
            return deliverySlotDtoList;
        }

        public void AddSlot(DeliverySlotsDto value)
        {
            var deliveryslots = new DeliverySlots();

            deliveryslots.ID = value.ID;
            deliveryslots.SlotName = value.SlotName;
            deliveryslots.FirstWaveTime = value.FirstWaveTime;
            deliveryslots.SecondWaveTime = value.SecondWaveTime;
            deliveryslots.StartTime = value.StartTime;
            deliveryslots.CutoffTime = value.CutoffTime;
            deliveryslots.CountdownTime = value.CountdownTime;
            deliveryslots.OrderAcceptTime = value.OrderAcceptTime;
            deliveryslots.OrderCofirmTime = value.OrderCofirmTime;
            deliveryslots.DispatchesConfirmTime = value.DispatchesConfirmTime;
            deliveryslots.EndTime = value.EndTime;

            this.iDeliverySlotsDataService.AddSlot(deliveryslots);
        }

        public DeliverySlotsDto GetSlot(int id)
        {
            var slot = this.iDeliverySlotsDataService.GetSlot(id);

            var slotsDto = new DeliverySlotsDto();

            slotsDto.ID = slot.ID;
            slotsDto.SlotName = slot.SlotName;
            slotsDto.FirstWaveTime = slot.FirstWaveTime;
            slotsDto.SecondWaveTime = slot.SecondWaveTime;
            slotsDto.StartTime = slot.StartTime;
            slotsDto.CutoffTime = slot.CutoffTime;
            slotsDto.CountdownTime = slot.CountdownTime;
            slotsDto.OrderAcceptTime = slot.OrderAcceptTime;
            slotsDto.OrderCofirmTime = slot.OrderCofirmTime;
            slotsDto.DispatchesConfirmTime = slot.DispatchesConfirmTime;
            slotsDto.EndTime = slot.EndTime;
            return slotsDto;
        }

        public void DeleteSlot(int id)
        {
            var slot = this.iDeliverySlotsDataService.GetSlot(id);
            slot.IsDeleted = true;
            this.iDeliverySlotsDataService.SaveChanges();
        }

        public void UpdateSlot(DeliverySlotsDto deliverySlotDto)
        {
            var slot = this.iDeliverySlotsDataService.GetSlot(deliverySlotDto.ID);

            slot.ID = deliverySlotDto.ID;
            slot.SlotName = deliverySlotDto.SlotName;
            slot.FirstWaveTime = deliverySlotDto.FirstWaveTime;
            slot.SecondWaveTime = deliverySlotDto.SecondWaveTime;
            slot.StartTime = deliverySlotDto.StartTime;
            slot.CutoffTime = deliverySlotDto.CutoffTime;
            slot.CountdownTime = deliverySlotDto.CountdownTime;
            slot.OrderAcceptTime = deliverySlotDto.OrderAcceptTime;
            slot.OrderCofirmTime = deliverySlotDto.OrderCofirmTime;
            slot.DispatchesConfirmTime = deliverySlotDto.DispatchesConfirmTime;
            slot.EndTime = deliverySlotDto.EndTime;

            this.iDeliverySlotsDataService.SaveChanges();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.iDeliverySlotsDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }
    }
}
