using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;
using Mainframe.BuyerSupplier.Engine;
using Mainframe.BuyerSupplier.Common.Utility;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IOrderBusinessEntity
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrder(int orderId);
        OrderDetailDto GetOrderDetail(int orderDetailId);
        OrderAssignmentDto GetOrderAssignment(int orderAssignmentId);
        IEnumerable<OrderOptimizedPossibilityDto> AddOrder(OrderDto order);
        void AddOrderAssignment(OrderOptimizedPossibilityDto orderOptimizedPossibility);
        void UpdateOrder(OrderDto order);
        void UpdateOrderAssignment(OrderAssignmentDto orderAssignmentDto);
        void DeleteOrder(OrderDto order);
        void DeleteOrderAssignment(OrderAssignmentDto orderAssignmentDto);

        IEnumerable<OrderOptimizedPossibilityDto> SearchPossibilities(Order order, List<OrderDetail> orderDetails);
        void UpdateSupplierInventories(List<OrderOptimizedPossibilityDto> orderOptimizedPossibilityDtos);

        void UpdateOrderAssignmentType(int orderId, OrderPossibilityType orderPossibilityType);

        bool ManageWaves(int deliverySlotId);
    }

    public class OrderBusinessEntity : IOrderBusinessEntity
    {
        private IOrderDataService orderDataService;
        private IOptimizationEngine optimizationEngine;
        private IStandardInventoryBusinessEntity standardInventoryBusinessEntity;
        private ISupplierBaseService supplierBaseService;
        private IDeliverySlotsBusinessEntity deliverySlotBusinessEntity;
        private IUserBusinessEntity userBusinessEntity;
        private ISupplierInventoryDataService supplierInventoryDataService;
        private IWaveManagement waveManagement;

        public OrderBusinessEntity(IOrderDataService orderDataService,
                                  IStandardInventoryBusinessEntity standardInventoryBusinessEntity,
                                  IDeliverySlotsBusinessEntity deliverySlotBusinessEntity,
                                  IUserBusinessEntity userBusinessEntity, IOptimizationEngine optimizationEngine,
                                  ISupplierBaseService supplierBaseService, 
                                  ISupplierInventoryDataService supplierInventoryDataService,
                                  IWaveManagement waveManagement)
        {
            this.orderDataService = orderDataService;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
            this.deliverySlotBusinessEntity = deliverySlotBusinessEntity;
            this.userBusinessEntity = userBusinessEntity;
            this.optimizationEngine = optimizationEngine;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
            this.supplierBaseService = supplierBaseService;
            this.supplierInventoryDataService = supplierInventoryDataService;
            this.waveManagement = waveManagement;
        }

        public List<OrderDto> GetAllOrders()
        {
            var orders = orderDataService.GetOrders().ToList();
            var standardInventories = standardInventoryBusinessEntity.GetAllStandardInventories();
            var deliverySlots = deliverySlotBusinessEntity.GetDeliverySlots();
            var buyers = userBusinessEntity.GetUsers();

            var orderDetailIds = orders.SelectMany(p => p.OrderDetails).Select(p => p.ID);

            var orderAssigments = orderDataService.GetOrderAssignmentsByOrderDetailByIds(orderDetailIds);

            var orderDtoList = new List<OrderDto>();


            orders.ForEach(r =>
            {
                var deliverySlot = deliverySlots.FirstOrDefault(s => s.ID == r.DeliverySlotId);

                var orderDto = new OrderDto()
                {
                    ID = r.ID,
                    OrderRefNo = r.OrderRefNo,
                    BuyerId = r.BuyerId,
                    OrderedDate = r.OrderedDate,
                    ExpectedDiliveredDate = r.ExpectedDiliveredDate,
                    OrderType = r.OrderType,
                    Status = r.Status,
                    DeliverySlotId = r.DeliverySlotId,
                    SupplierCategory = r.SupplierCategory,
                    IsDeleted = r.IsDeleted,
                    AssignmentSelectionType=r.AssignmentSelectionType,
                    DeliverySlotName = deliverySlot != null ? deliverySlot.SlotName : "",
                    OrderDetails = new List<OrderDetailDto>()
                };


                r.OrderDetails.ForEach(d =>
               {
                   var standardInventory = standardInventories.FirstOrDefault(s => s.ID == d.ID);

                   var orderDetailDto = new OrderDetailDto()
                   
                   {
                       ID = d.ID,
                       OrderID = r.ID,
                       StandardInventoryId = d.StandardInventoryId,
                       Qty = d.Qty,
                       ItemName = standardInventory != null ? standardInventory.ItemName : "",
                       OrderAssignments = orderAssigments.Where(p => p.OrderDetailID == d.ID).Select(a => new OrderAssignmentDto
                       {
                           ID = a.ID,
                           OrderDetailID = d.ID,
                           SupplierInventoryID = a.SupplierInventoryID,
                           Qty = a.Qty,
                           SupplierAcknowledgement = a.SupplierAcknowledgement,
                           VehicleAcknowledgement = a.VehicleAcknowledgement,
                           BuyerAcknowledgement = a.BuyerAcknowledgement
                       })
                   };

                   orderDto.OrderDetails.Add(orderDetailDto);
               });
                
                orderDtoList.Add(orderDto);
            }

           );

            return orderDtoList;
        }

        public OrderDto GetOrder(int orderId)
        {
            var orderDto = orderDataService.GetOrder(orderId);
            var order = new OrderDto
            {
                ID = orderDto.ID,
                OrderRefNo = orderDto.OrderRefNo,
                BuyerId = orderDto.BuyerId,
                OrderedDate = orderDto.OrderedDate,
                ExpectedDiliveredDate = orderDto.ExpectedDiliveredDate,
                OrderType = orderDto.OrderType,
                DeliverySlotId = orderDto.DeliverySlotId,
                SupplierCategory = orderDto.SupplierCategory,
                IsDeleted = orderDto.IsDeleted,
                AssignmentSelectionType = orderDto.AssignmentSelectionType,
                Status = orderDto.Status,
                OrderDetails = orderDataService.GetOrderDetailsByOrder(orderDto.ID).Select(d => new OrderDetailDto
                {
                    ID = d.ID,
                    OrderID = orderDto.ID,
                    StandardInventoryId = d.StandardInventoryId,
                    Qty = d.Qty,
                    OrderAssignments = orderDataService.GetOrderAssignmentsByOrderDetail(d.ID).Select(a => new OrderAssignmentDto
                    {
                        ID = a.ID,
                        OrderDetailID = d.ID,
                        SupplierInventoryID = a.SupplierInventoryID,
                        Qty = a.Qty,
                        SupplierAcknowledgement = a.SupplierAcknowledgement,
                        VehicleAcknowledgement = a.VehicleAcknowledgement,
                        BuyerAcknowledgement = a.BuyerAcknowledgement
                    })
                }).ToList()
            };

            return order;
        }

        public OrderDetailDto GetOrderDetail(int orderDetailId)
        {
            var orderDetailDto = orderDataService.GetOrderDetail(orderDetailId);
            var orderDetail = new OrderDetailDto
            {
                ID = orderDetailDto.ID,
                OrderID = orderDetailDto.OrderID,
                    StandardInventoryId = orderDetailDto.StandardInventoryId,
                Qty = orderDetailDto.Qty,
                OrderAssignments = orderDataService.GetOrderAssignmentsByOrderDetail(orderDetailDto.ID).Select(a => new OrderAssignmentDto
                {
                    ID = a.ID,
                    OrderDetailID = orderDetailDto.ID,
                    SupplierInventoryID = a.SupplierInventoryID,
                    Qty = a.Qty,
                    SupplierAcknowledgement = a.SupplierAcknowledgement,
                    VehicleAcknowledgement = a.VehicleAcknowledgement,
                    BuyerAcknowledgement = a.BuyerAcknowledgement
                })

            };

            return orderDetail;
        }

        public OrderAssignmentDto GetOrderAssignment(int orderAssignmentId)
        {
            var orderAssignmentDto = orderDataService.GetOrderAssignment(orderAssignmentId);
            var orderAssignment = new OrderAssignmentDto { };
            return orderAssignment;
        }

        public IEnumerable<OrderOptimizedPossibilityDto> AddOrder(OrderDto orderDto)
        {
            IEnumerable<OrderOptimizedPossibilityDto> orderOptimizedPossibilityDtos = null;
            var order = new Order
            {
                ID = orderDto.ID,
                OrderRefNo = orderDto.OrderRefNo,
                BuyerId = orderDto.BuyerId,
                OrderedDate = DateTime.Today,
                ExpectedDiliveredDate = orderDto.ExpectedDiliveredDate,
                OrderType = orderDto.OrderType,
                Status = orderDto.Status,   //1-Active, 2-Hold, 3-Procedded, 4- Cancelled
                AssignmentSelectionType = orderDto.AssignmentSelectionType,
                DeliverySlotId = orderDto.DeliverySlotId,
                IsDeleted = orderDto.IsDeleted,
                SupplierCategory = orderDto.SupplierCategory
            };

            var orderDetails = orderDto.OrderDetails.Select(od => new OrderDetail()
            {
                ID = od.ID,
                StandardInventoryId = od.StandardInventoryId,
                Qty = od.Qty,
                IsDeleted = false
            }).ToList();

            var orderVal= this.orderDataService.AddOrder(order, orderDetails);

            if (order.OrderType==2)
            {
                orderOptimizedPossibilityDtos = SearchPossibilities(orderVal.Key, orderVal.Value);
            }
            return orderOptimizedPossibilityDtos;
        }

        public void AddOrderAssignment(OrderOptimizedPossibilityDto orderOptimizedPossibility)
        {
            var orderAssignmentList = new List<OrderAssignment>();

            foreach (var r in orderOptimizedPossibility.OrderOptimizedDetails)
            {
                var orderAssignment= new OrderAssignment();

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

        public void UpdateOrder(OrderDto orderDto)
        {
            var order = this.orderDataService.GetOrder(orderDto.ID);
            order.ID = orderDto.ID;
            order.OrderRefNo = orderDto.OrderRefNo;
            order.BuyerId = orderDto.BuyerId;
            order.OrderedDate = orderDto.OrderedDate;
            order.ExpectedDiliveredDate = orderDto.ExpectedDiliveredDate;
            order.OrderType = orderDto.OrderType;
            order.Status = orderDto.Status;
           order. IsDeleted = orderDto.IsDeleted;
            order.AssignmentSelectionType = orderDto.AssignmentSelectionType;
            order.DeliverySlotId = orderDto.DeliverySlotId;
            order.SupplierCategory = orderDto.SupplierCategory;

            var savedOrderDetails = this.orderDataService.GetOrderDetailsByOrder(orderDto.ID);

            foreach (var item in savedOrderDetails)
            {
                var orderItemDto = orderDto.OrderDetails.FirstOrDefault(p => p.ID == item.ID);

                if (orderItemDto != null)
                    item.Qty = orderItemDto.Qty;
                else
                    item.IsDeleted = true;
            }

            //this.orderDataService.SaveChanges();

            var newOrderDetails = orderDto.OrderDetails.Where(p => p.ID == 0).Select(od => new OrderDetail()
            {
                ID = od.ID,
                OrderID = orderDto.ID,
                StandardInventoryId = od.StandardInventoryId,
                Qty = od.Qty,
                IsDeleted = od.IsDeleted
            });

            this.orderDataService.AddOrderDetasils(newOrderDetails);
        }

        public void UpdateOrderAssignment(OrderAssignmentDto orderAssignmentDto)
        {
            var orderAssignment = this.orderDataService.GetOrderAssignment(orderAssignmentDto.ID);
            orderAssignment.Qty = orderAssignmentDto.Qty;
            orderAssignment.SupplierInventoryID = orderAssignmentDto.SupplierInventoryID;
            orderAssignment.SupplierAcknowledgement = orderAssignmentDto.SupplierAcknowledgement;
            orderAssignment.VehicleAcknowledgement = orderAssignmentDto.VehicleAcknowledgement;
            orderAssignment.BuyerAcknowledgement = orderAssignmentDto.BuyerAcknowledgement;
            this.orderDataService.UpdateOrderAssignment(orderAssignment);
        }

        public void DeleteOrder(OrderDto orderDto)
        {
            var order = this.orderDataService.GetOrder(orderDto.ID);
            order.Status = orderDto.Status;

            var savedOrderDetails = this.orderDataService.GetOrderDetailsByOrder(orderDto.ID);
            savedOrderDetails.ToList().ForEach(od => od.IsDeleted = true);

            this.orderDataService.UpdateOrder();
        }

        public void UpdateOrderAssignmentType(int orderId, OrderPossibilityType orderPossibilityType)
        {
            var order = this.orderDataService.GetOrder(orderId);

            int assignmentType;
            int.TryParse(orderPossibilityType.ToString(), out assignmentType);
            order.AssignmentSelectionType = assignmentType;
            order.Status = 2;

            this.orderDataService.UpdateOrder();
        }

        public void DeleteOrderAssignment(OrderAssignmentDto orderAssignmentDto)
        {
            var orderAssignment = this.orderDataService.GetOrderAssignment(orderAssignmentDto.ID);
            orderAssignment.IsDeleted = true;
            this.orderDataService.UpdateOrderAssignment(orderAssignment);
        }

        public IEnumerable<OrderOptimizedPossibilityDto> SearchPossibilities(Order order, List<OrderDetail> orderDetails)
        {
            //var order = new Order
            //{
            //    ID = orderDto.ID,
            //    OrderRefNo = orderDto.OrderRefNo,
            //    BuyerId = orderDto.BuyerId,
            //    OrderedDate = DateTime.Today,
            //    ExpectedDiliveredDate = orderDto.ExpectedDiliveredDate,
            //    IsPreOrder = orderDto.IsPreOrder,
            //    Status = orderDto.Status,   //1-Active, 2-Hold, 3-Procedded, 4- Cancelled
            //    DeliverySlotId = orderDto.DeliverySlotId,
            //    SupplierCategory = orderDto.SupplierCategory
            //};

            //var orderDetails = orderDto.OrderDetails.Select(od => new OrderDetail()
            //{
            //    ID = od.ID,
            //    StandardInventoryId = od.ItemID,
            //    Qty = od.Qty,
            //    IsDeleted = false
            //}).ToList();

            var standardInventories = standardInventoryBusinessEntity.GetStandardInventories();
            var orderDetailStandardInventoryData = from od in orderDetails
                                                   join si in standardInventories on od.StandardInventoryId equals si.ID
                                                   select new
                                                   {
                                                       OrderDetailId = od.ID,
                                                       StandardInventoryId = si.ID,
                                                       si.ItemName,
                                                       si.QuantityUnitOfMeasureName
                                                   };
            var supplierBases = supplierBaseService.GetAllSupplierBases();

            var orderPossibilities = optimizationEngine.SearchBestAvailbalities(order, orderDetails);

            if (orderPossibilities == null || orderPossibilities.Count() == 0) return null;
            var orderPossibilityDtos  = orderPossibilities.Select(
                p => new OrderOptimizedPossibilityDto() {
                    SupplierBaseId = p.SupplierBaseId,
                    SupplierBase = supplierBases.Where(x=>x.SupplierBaseId == p.SupplierBaseId).FirstOrDefault().SupplierBaseName,
                    DeliveryCost = p.DeliveryCost,
                    ItemCost = p.ItemCost,
                    OrderValue = p.OrderValue,
                    OrderOptimizedPossibilityId = p.OrderOptimizedPossibilityId,
                    OrderPossibilityType=p.OrderPossibilityType,
                    OrderOptimizedDetails = p.OrderOptimizedDetails.Select(
                        r => new OrderOptimizedDetailDto() {
                            OrderDetailID = r.OrderDetailID,
                            ItemName = orderDetailStandardInventoryData.Where(a=>a.OrderDetailId== r.OrderDetailID).FirstOrDefault().ItemName,
                            UnitOfMeasureName = orderDetailStandardInventoryData.Where(a => a.OrderDetailId == r.OrderDetailID).FirstOrDefault().QuantityUnitOfMeasureName,
                            SupplierQuality=r.SupplierQuality,
                            SupplierInventoryID =r.SupplierInventoryID,
                            Qty=r.Qty,
                            UnitPrice=r.UnitPrice,
                            Value= r.Value
                        }).ToList()
                });

            return orderPossibilityDtos;
        }

        public void UpdateSupplierInventories(List<OrderOptimizedPossibilityDto> orderOptimizedPossibilityDtos)
        {
            foreach (var orderOptimizedPossibility in orderOptimizedPossibilityDtos)
            {
                foreach (var r in orderOptimizedPossibility.OrderOptimizedDetails)
                {
                   var supllierInventory = supplierInventoryDataService.GetSupplierInventory(r.SupplierInventoryID);
                    supllierInventory.ProcessingQty = supllierInventory.ProcessingQty - r.Qty;
                    supplierInventoryDataService.UpdateSupplierInventory(supllierInventory);
                }
            }
        }

        public bool ManageWaves(int deliverySlotId)
        {
            return waveManagement.ProcessUnassignedOrders(deliverySlotId);
        }
    }
}