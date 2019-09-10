using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class WatchList
    {
        public int Id { get; set; }
        public int StandardInventoryId { get; set; }
        public int SupplierId { get; set; }
        public int DeliverySlotId { get; set; }
        public int SupplierInventoryId { get; set; }
        public decimal QOH { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime AddedDate { get; set; }
        public int Status { get; set; }
    }
}
