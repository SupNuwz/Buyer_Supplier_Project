using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Common.Dto
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
    }
}
