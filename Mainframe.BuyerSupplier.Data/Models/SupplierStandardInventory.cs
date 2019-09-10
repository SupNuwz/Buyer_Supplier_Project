using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class SupplierStandardInventory
    {
        public int Id { get; set; }
        public int StandardInventoryId { get; set; }
        public int SupplierId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSelected { get; set; }
        public StandardInventory StandardInventory { get; set; }
    }
}
