using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class StandardInventory
    {
        public int ID { get; set; }

        public string ItemName { get; set; }

        public int InventoryItemCategoryId { get; set; }

        public int InventoryItemSubCategoryId { get; set; }

        public int QuantityUnitOfMesureId { get; set; }

        public string Seasonality { get; set; }

        public decimal MinimumInventory { get; set; }

        public bool IsDeleted { get; set; }

        public int FileServerDetailID { get; set; }
    }
}