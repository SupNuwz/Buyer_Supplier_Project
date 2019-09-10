using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IDeliverySlotsDataService : IBaseDataService
    {
        IEnumerable<DeliverySlots> GetDeliverySlots();
        void AddSlot(DeliverySlots deliverySlot);
        DeliverySlots GetSlot(int SlotID);
        bool IsItemAvailable(string itemName, int itemID);
    }
    public class DeliverySlotsDataService : BaseDataService, IDeliverySlotsDataService
    {
        private DatabaseContext databaseContext;
        public DeliverySlotsDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<DeliverySlots> GetDeliverySlots()
        {
            var deliverySlot = (from s in databaseContext.DeliverySlots
                                where s.IsDeleted == false
                                select s).ToList();
            return deliverySlot;
        }

        
        public void AddSlot(DeliverySlots deliverySlot)
        {
            databaseContext.DeliverySlots.Add(deliverySlot);
            databaseContext.SaveChanges();
        }

        public DeliverySlots GetSlot(int SlotID)
        {
            var slot = from s in databaseContext.DeliverySlots
                       where s.ID == SlotID
                       select s;
            return slot.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var deliverySlotsItem = from s in databaseContext.DeliverySlots
                                    where ((s.SlotName == itemName)
                                           && (itemID == 0 || (itemID != s.ID && s.IsDeleted == false)))
                                    select s;

            return deliverySlotsItem.Any();
        }
    }
}
