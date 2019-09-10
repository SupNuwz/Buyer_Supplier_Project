using Mainframe.BuyerSupplier.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Engine
{
    public class OrderOptimizedPossibility
    {
        public int OrderOptimizedPossibilityId { get; set; }
        public decimal ItemCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal OrderValue { get; set; }
        public int SupplierBaseId { get; set; }
        public OrderPossibilityType OrderPossibilityType { get; set; }
        public decimal AverageSupplierQuality { get; set; }
        public List<OrderOptimizedDetail> OrderOptimizedDetails { get; set; }
    }
}
