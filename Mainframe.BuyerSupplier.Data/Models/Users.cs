using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
   public class Users    
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }

        public string UserType { get; set; }
        public int DefaultSupplierBaseId { get; set; }
        public int DeliverySlotId { get; set; }
        public string Category { get; set; }
        public int StandaradInventoryID { get; set; }
        public int RelevantZoneId { get; set; }
        public bool IsDeleted { get; set; }
        public decimal SupplierQuality { get; set; }
    }
}
