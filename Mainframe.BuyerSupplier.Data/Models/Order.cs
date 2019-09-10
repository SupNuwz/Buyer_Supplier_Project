using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderRefNo { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ExpectedDiliveredDate { get; set; }
        public int OrderType { get; set; }
        public int Status { get; set; }
        public int DeliverySlotId { get; set; }
        public string SupplierCategory { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public bool IsDeleted { get; set; }
        public int AssignmentSelectionType { get; set; }
    }
}
