using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
   public class InventoryItemSubCategoryDto
    {
       
            public int ID { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int InventoryItemCategoryID { get; set; }
            public string CategoryName { get; set; }

    }
}
