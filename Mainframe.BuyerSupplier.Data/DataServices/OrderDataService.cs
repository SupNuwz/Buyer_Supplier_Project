using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IOrderDataService: IBaseDataService
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<OrderDetail> GetOrderDetails();
        IEnumerable<OrderAssignment> GetOrderAssignments();
      //  IEnumerable<OrderDetail> GetOrderDetailsByOrders(IEnumerable<int> orderIds);
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId); 
        IEnumerable<OrderAssignment> GetOrderAssignmentsByOrder(int orderId);
        IEnumerable<OrderAssignment> GetOrderAssignmentsByOrderDetail(int orderDetailId);
        Order GetOrder(int orderId);
        OrderDetail GetOrderDetail(int orderDetailId);
        OrderAssignment GetOrderAssignment(int orderAssignmentId);
        KeyValuePair<Order, List<OrderDetail>> AddOrder(Order order, List<OrderDetail> orderDetails);
        void AddOrderDetasils(IEnumerable<OrderDetail> orderDetails);
        void AddOrderAssignment(List<OrderAssignment> orderAssignments);
        void UpdateOrder();
        void UpdateOrderAssignment(OrderAssignment orderAssignment);
        List<OrderAssignment> GetOrderAssignmentsByOrderDetailByIds(IEnumerable<int> orderDetailIds);
        IEnumerable<Order> GetUnassignedOrdersbyDeliverySlot(int deliverySlot);

    }
    public class OrderDataService:BaseDataService, IOrderDataService
    {
        private DatabaseContext databaseContext;

        public OrderDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = (from o in databaseContext.Order.Include(s => s.OrderDetails)
                          select o).ToList();
            return orders;
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            var orderDetails = (from od in databaseContext.OrderDetail
                          select od).ToList();
            return orderDetails;
        }

        public IEnumerable<OrderAssignment> GetOrderAssignments()
        {
            var orderAssignments = (from oa in databaseContext.OrderAssignment 
                                    select oa).ToList();
            return orderAssignments;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId)
        {
            var orderDetails = (from od in databaseContext.OrderDetail
                                where od.OrderID == orderId
                                select od).ToList();
            return orderDetails;
        }


        //public IEnumerable<OrderDetail> GetOrderDetailsByOrders(IEnumerable<int> orderIds)
        //{
        //    var orders = (from od in databaseContext.OrderDetail
        //                               where orderIds.Contains(od.Order.ID) && od.Order.OrderDetail != null
        //                               select od).ToList();
        //    return orders;
        //}
        public IEnumerable<OrderAssignment> GetOrderAssignmentsByOrder(int orderId)
        {
            var orderAssignments = (from oa in databaseContext.OrderAssignment
                                    join od in databaseContext.OrderDetail on oa.OrderDetailID equals od.ID
                                    where od.OrderID == orderId
                                    select oa).ToList();
            return orderAssignments;
        }

        public IEnumerable<OrderAssignment> GetOrderAssignmentsByOrderDetail(int orderDetailId)
        {
            var orderAssignments = (from oa in databaseContext.OrderAssignment
                                    where oa.OrderDetailID == orderDetailId
                                    select oa).ToList();
            return orderAssignments;
        }
        
        public Order GetOrder(int orderId)
        {
            var order = from o in databaseContext.Order
                        where o.ID == orderId
                        select o;

            return order.FirstOrDefault();
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            var orderDetails = from od in databaseContext.OrderDetail
                        where od.ID == orderDetailId
                               select od;

            return orderDetails.FirstOrDefault();
        }

        public OrderAssignment GetOrderAssignment(int orderAssignmentId)
        {
            var order = from oa in databaseContext.OrderAssignment
                        where oa.ID == orderAssignmentId
                        select oa;

            return order.FirstOrDefault();
        }

        public KeyValuePair< Order, List<OrderDetail>> AddOrder(Order order, List<OrderDetail> orderDetails)
        {
            databaseContext.Order.Add(order);            
            databaseContext.SaveChanges();
            orderDetails.ForEach(r => r.OrderID = order.ID);
            AddOrderDetasils(orderDetails);
            return new KeyValuePair<Order, List<OrderDetail>>(order, orderDetails);
        }

        public void AddOrderAssignment(List<OrderAssignment> orderAssignments)
        {
            databaseContext.OrderAssignment.AddRange(orderAssignments);
            databaseContext.SaveChanges();
        }

        public void UpdateOrder()
        {            
            databaseContext.SaveChanges();
        }

        public void UpdateOrderAssignment(OrderAssignment orderAssignment)
        {
            databaseContext.OrderAssignment.Update(orderAssignment);
            databaseContext.SaveChanges();
        }

        public void AddOrderDetasils(IEnumerable<OrderDetail> orderDetails)
        {
            databaseContext.OrderDetail.AddRange(orderDetails);
            databaseContext.SaveChanges();
        }

        public List<OrderAssignment> GetOrderAssignmentsByOrderDetailByIds(IEnumerable<int> orderDetailIds)
        {
            var orderDetails = from oa in databaseContext.OrderAssignment
                               where orderDetailIds.Contains(oa.OrderDetailID)
                               select oa;

            return orderDetails.ToList();
        }

        public IEnumerable<Order> GetUnassignedOrdersbyDeliverySlot(int deliverySlot)
        {
            var orders = (from o in databaseContext.Order.Include(s => s.OrderDetails)
                          where o.DeliverySlotId == deliverySlot && o.Status == 1
                          select o).ToList();
            return orders;
        }
    }
}
