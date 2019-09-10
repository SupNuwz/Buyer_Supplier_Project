using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class SupplierStandardInventoryDto
    {
        public int Id { get; set; }
        public int StandardInventoryId { get; set; }
        public int SupplierId { get; set; }
        public bool IsSelected { get; set; }
        public string InventoryItemName { get; set; }
        //public string Group { get; set; }
        public int InventoryItemCategoryId { get; set; }
        public string InventoryItemCategoryName { get; set; }
    }
}
