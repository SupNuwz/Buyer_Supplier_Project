using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderManagement")]
    public class OrderController : Controller
    {
        private IOrderBusinessEntity orderBusinessEntity;
        public OrderController(IOrderBusinessEntity orderBusinessEntity)
        {
            this.orderBusinessEntity = orderBusinessEntity;
        }

        // GET: api/OrderManagement
        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            return orderBusinessEntity.GetAllOrders();
        }



        // GET: api/OrderManagement/5
        [HttpGet("{orderId}")]
        public OrderDto GetOrder(int orderId)
        {
            return orderBusinessEntity.GetOrder(orderId);
        }

        // GET: api/OrderManagement/5
        [HttpGet("orderDetailId/{orderDetailId}")]
        public OrderDetailDto GetOrderDetail(int orderDetailId)
        {
            return orderBusinessEntity.GetOrderDetail(orderDetailId);
        }


        // GET: api/OrderManagement/5
        [HttpGet("orderAssignmentId/{orderAssignmentId}")]
        public OrderAssignmentDto GetOrderAssignment(int orderAssignmentId)
        {
            return orderBusinessEntity.GetOrderAssignment(orderAssignmentId);
        }

        // POST: api/OrderManagement
        [HttpPost]
        public IEnumerable<OrderOptimizedPossibilityDto> AddOrder([FromBody]OrderDto value)
        {
            return this.orderBusinessEntity.AddOrder(value);

        }

        //[HttpPost("OrderAssignment/{orderId}")]
        //public void AddOrderAssignment(int orderId, [FromBody]OrderPossibilitySelectionListDto value)
       [HttpPost("OrderAssignment")]
        public void AddOrderAssignment(int orderId, [FromBody]OrderPossibilitySelectionListDto value)
        {
            var selectedPossibility = value.OrderPossibilitySelectionDtos.Where(r => r.IsSelected == true).Select(r => r.OrderOptimizedPossibilityDto).FirstOrDefault();
            this.orderBusinessEntity.AddOrderAssignment(selectedPossibility);
            var unselectedPossibilities = value.OrderPossibilitySelectionDtos.Where(r => r.IsSelected == false).Select(r => r.OrderOptimizedPossibilityDto).ToList();
            this.orderBusinessEntity.UpdateSupplierInventories(unselectedPossibilities);
            //this.orderBusinessEntity.UpdateOrderAssignmentType(orderId, selectedPossibility.OrderPossibilityType);
        }


        // PUT: api/OrderManagement/5
        [HttpPut("{orderId}")]
        public void UpdateOrder(int orderId, [FromBody]OrderDto order)
        {
            this.orderBusinessEntity.UpdateOrder(order);
        }

        // PUT: api/OrderManagement/5
        [HttpPut("{orderAssignmentId}")]
        public void UpdateOrderAssignment(int orderId, [FromBody]OrderAssignmentDto orderAssignment)
        {
            this.orderBusinessEntity.UpdateOrderAssignment(orderAssignment);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        //// GET: api/OrderManagement/SearchPossibilities
        //[HttpPost("SearchPossibilities")]
        //public IEnumerable<OrderOptimizedPossibilityDto> SearchPossibilities([FromBody]OrderDto orderDto)
        //{
        //    return orderBusinessEntity.SearchPossibilities(orderDto);
        //}

        [HttpPost("WaveManagement")]
        public bool WaveProcessHandeling([FromBody]int deliverySlot)
        {
            if (deliverySlot <= 0) return false;
            return orderBusinessEntity.ManageWaves(deliverySlot);
        }
    }
}
