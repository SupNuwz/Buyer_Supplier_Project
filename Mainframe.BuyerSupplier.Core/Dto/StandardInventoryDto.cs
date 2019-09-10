using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class StandardInventoryDto
    {
        public int ID { get; set; }

        public string ItemName { get; set; }

        public int InventoryItemCategoryId { get; set; }

        public string InventoryItemCategoryName { get; set; }

        public int InventoryItemSubCategoryId { get; set; }

        public string InventoryItemSubCategoryName { get; set; }

        public int QuantityUnitOfMesureId { get; set; }

        public string QuantityUnitOfMeasureName { get; set; }
                
        public string Seasonality { get; set; }

        public decimal MinimumInventory { get; set; }

        public int FileID { get; set; }

        public string FileUrl { get; set; }

        public bool Added { get; set; }

    }
}
