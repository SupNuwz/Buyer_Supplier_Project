using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Engine
{
    public interface IWaveManagement
    {
        bool ProcessUnassignedOrders(int deliverySlotId);
    }

    public class WaveManagement : IWaveManagement
    {
        private IOrderDataService orderDataService;
        private IOptimizationEngine optimizationEngine;
        private ISupplierInventoryDataService supplierInventoryDataService;

        public WaveManagement(IOrderDataService orderDataService, IOptimizationEngine optimizationEngine,
            ISupplierInventoryDataService supplierInventoryDataService)
        {
            this.orderDataService = orderDataService;
            this.optimizationEngine = optimizationEngine;
            this.supplierInventoryDataService = supplierInventoryDataService;
        }

        public bool ProcessUnassignedOrders(int deliverySlotId)
        {
            bool retVal = false;
            var orders = orderDataService.GetUnassignedOrdersbyDeliverySlot(deliverySlotId);
            foreach (var order in orders)
            {
                retVal = false;
                var orderPossibilities = optimizationEngine.SearchBestAvailbalities(order, order.OrderDetails);

                if (orderPossibilities != null && orderPossibilities.Count > 0)
                {
                    foreach (var orderPossibility in orderPossibilities)
                    {
                        var orderAssignmentList = new List<OrderAssignment>();

                        foreach (var r in orderPossibility.OrderOptimizedDetails)
                        {
                            var orderAssignment = new OrderAssignment();

                            orderAssignment.OrderDetailID = r.OrderDetailID;
                            orderAssignment.SupplierInventoryID = r.SupplierInventoryID;
                            orderAssignment.Qty = r.Qty;
                            orderAssignment.SupplierAcknowledgement = false;
                            orderAssignment.BuyerAcknowledgement = false;
                            orderAssignment.VehicleAcknowledgement = false;
                            orderAssignment.IsDeleted = false;

                            orderAssignmentList.Add(orderAssignment);

                            var supllierInventory = supplierInventoryDataService.GetSupplierInventory(r.SupplierInventoryID);
                            supllierInventory.ProcessingQty = supllierInventory.ProcessingQty - r.Qty;
                            supllierInventory.AvailableQty = supllierInventory.AvailableQty - r.Qty;
                            supplierInventoryDataService.UpdateSupplierInventory(supllierInventory);
                        }

                        this.orderDataService.AddOrderAssignment(orderAssignmentList);
                    }

                    var curOrder = this.orderDataService.GetOrder(order.ID);
                    order.Status = 2;
                    this.orderDataService.UpdateOrder();
                }
                retVal = true;
            }
            return retVal;
        }
    }
}
