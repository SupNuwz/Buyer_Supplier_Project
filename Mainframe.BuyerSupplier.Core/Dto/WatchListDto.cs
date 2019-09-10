using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class WatchListDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int StandardInventoryId { get; set; }
        public string StandardInventoryName { get; set; }
        public decimal QuantityAvailable { get; set; }
        public decimal Price { get; set; }
    }
}
