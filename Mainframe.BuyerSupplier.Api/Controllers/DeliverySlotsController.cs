using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Mvc;


namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/DeliverySlots")]
    public class DeliverySlotsController : Controller
    {
        private IDeliverySlotsBusinessEntity deliverySlotsService;

        public DeliverySlotsController(IDeliverySlotsBusinessEntity deliverySlotsService)
        {
            this.deliverySlotsService = deliverySlotsService;
        }

        // GET
        [HttpGet]
        public IEnumerable<DeliverySlotsDto> GetDeliverySlots()
        {
            return this.deliverySlotsService.GetDeliverySlots();
        }

        // POST 
        [HttpPost]
        public void AddSlot([FromBody] DeliverySlotsDto value)
        {
            deliverySlotsService.AddSlot(value);
        }

        // GET
        [HttpGet("{id}")]
        public DeliverySlotsDto Get(int id)
        {
            return deliverySlotsService.GetSlot(id);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public IEnumerable<DeliverySlotsDto> DeleteSlot(int id)
        {
            deliverySlotsService.DeleteSlot(id);
            return this.deliverySlotsService.GetDeliverySlots();
        }

        //PUT
        [HttpPut]
        public IEnumerable<DeliverySlotsDto> UpdateSlot([FromBody]DeliverySlotsDto value)
        {
            deliverySlotsService.UpdateSlot(value);
            return this.deliverySlotsService.GetDeliverySlots();
        }

        //GET
        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.deliverySlotsService.IsItemAvailable(itemName, itemID);
        }
    }
}
