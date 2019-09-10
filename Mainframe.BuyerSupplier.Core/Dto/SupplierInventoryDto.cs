using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class SupplierInventoryDto
    {
        public int ID { get; set; }
        public int SupplierStandardInventoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal AvailableQty { get; set; }
        public decimal ProcessingQty { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFreeze { get; set; }
        public DateTime InventoryDate { get; set; }

        public string InventoryItemName { get; set; }

        public string SupplierName { get; set; }
    }
}
