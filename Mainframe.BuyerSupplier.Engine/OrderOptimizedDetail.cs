using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Engine
{
    public class OrderOptimizedDetail
    {
        public int OrderDetailID { get; set; }
        public int SupplierInventoryID { get; set; }
        public decimal SupplierQuality { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Value { get; set; }


    }
}
