using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class SupplierBase
    {
        public int SupplierBaseId { get; set; }
        public string SupplierBaseName { get; set; }
        public string DeliverySlot { get; set; }

        public bool IsDeleted { get; set; }

    }
}
