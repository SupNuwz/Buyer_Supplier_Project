using Mainframe.BuyerSupplier.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class OrderDto
    {
        public int ID { get; set; }
        public string OrderRefNo { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ExpectedDiliveredDate { get; set; }
        public int OrderType { get; set; } //1 - Pre-Order, 2 - Browse Order
        public int Status { get; set; }
        public int DeliverySlotId { get; set; }
        public string SupplierCategory { get; set; }
        public bool IsDeleted { get; set; }
        public int AssignmentSelectionType { get; set; } //1 - Low Cost , 2 - High Quality , 3 - Best Optimal based on High Quality & Low Cost

        public string DeliverySlotName { get; set; }

        public string BuyerName { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int StandardInventoryId { get; set; }
        public decimal Qty { get; set; }
        public bool IsDeleted { get; set; }
        public string ItemName { get; set; }
        public IEnumerable<OrderAssignmentDto> OrderAssignments { get; set; }
    }

    public class OrderAssignmentDto
    {
        public int ID { get; set; }
        //public int OrderID { get; set; }
        public int OrderDetailID { get; set; }
        public int SupplierInventoryID { get; set; }
        public decimal Qty { get; set; }
        public bool SupplierAcknowledgement { get; set; }
        public bool VehicleAcknowledgement { get; set; }
        public bool BuyerAcknowledgement { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class OrderOptimizedPossibilityDto
    {
        public int OrderOptimizedPossibilityId { get; set; }
        public decimal ItemCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal OrderValue { get; set; }
        public int SupplierBaseId { get; set; }
        public string SupplierBase { get; set; }
        public OrderPossibilityType OrderPossibilityType { get; set; }
        public decimal AverageSupplierQuality { get; set; }
        public List<OrderOptimizedDetailDto> OrderOptimizedDetails { get; set; }
    }

    public class OrderOptimizedDetailDto
    {
        public int OrderDetailID { get; set; }
        public string ItemName { get; set; }
        public string UnitOfMeasureName { get; set; }
        public decimal SupplierQuality { get; set; }
        public int SupplierInventoryID { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Value { get; set; }
    }

    public class OrderPossibilitySelectionDto {
        public OrderOptimizedPossibilityDto OrderOptimizedPossibilityDto { get; set; }
        public bool IsSelected { get; set; }
    }


    public class OrderPossibilitySelectionListDto
    {
        public int Key { get; set; }
        public List<OrderPossibilitySelectionDto> OrderPossibilitySelectionDtos { get; set; }
    }
}
