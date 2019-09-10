using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
   public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
       public int StandardInventoryId { get; set; }
        public decimal Qty { get; set; }
        public bool IsDeleted { get; set; }
        public Order Order { get; set; }

    }
}
