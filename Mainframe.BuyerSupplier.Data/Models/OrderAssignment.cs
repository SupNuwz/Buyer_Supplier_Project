using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class OrderAssignment
    {
        public int ID { get; set; }
        //public int OrderID { get; set; }
        public int OrderDetailID { get; set; }
        public int SupplierInventoryID { get; set; }
        public decimal Qty { get; set; }
        public bool SupplierAcknowledgement { get; set; }
        public bool VehicleAcknowledgement { get; set; }
        public bool BuyerAcknowledgement { get; set; }
        public bool IsDeleted { get; set; }

    }
}
