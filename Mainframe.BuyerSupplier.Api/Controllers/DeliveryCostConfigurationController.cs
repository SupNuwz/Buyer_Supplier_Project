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
    [Route("api/DeliveryCostConfiguration")]
    public class DeliveryCostConfigurationController : Controller
    {
        private IDeliveryCostConfigurationBusinessEntity deliveryCostConfigurationService;
        public DeliveryCostConfigurationController(IDeliveryCostConfigurationBusinessEntity deliveryCostConfigurationService)
        {
            this.deliveryCostConfigurationService = deliveryCostConfigurationService;
        }


        // GET
        [HttpGet]
        public IEnumerable<DeliveryCostConfigurationDto> Get()
        {
            return this.deliveryCostConfigurationService.GetAllDeliveryCostConfiguration();
        }

        // GET
        [HttpGet("ActiveConfiguration")]
        public IEnumerable<DeliveryCostConfigurationDto> GetActiveDeliveryCostConfiguration()
        {
            return this.deliveryCostConfigurationService.GetAllActiveDeliveryCostConfiguration();
        }

        // GET: api/Zone/5
        [HttpGet("{id}")]
        public DeliveryCostConfigurationDto Get(int id)
        {
            return deliveryCostConfigurationService.GetDeliveryCostConfiguration(id);
        }

        // POST
        [HttpPost]
        public void AddDeliveryCostConfiguration([FromBody]DeliveryCostConfigurationDto value)
        {
            deliveryCostConfigurationService.AddDeliveryCostConfiguration(value);

        }

        // POST
        [HttpPost("Update")]
        public IEnumerable<DeliveryCostConfigurationDto> UpdateDeliveryCostConfiguration([FromBody]DeliveryCostConfigurationDto value)
         
        {
            deliveryCostConfigurationService.UpdateDeliveryCostConfiguration(value);
            return this.deliveryCostConfigurationService.GetAllActiveDeliveryCostConfiguration();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IEnumerable<DeliveryCostConfigurationDto> DeleteDeliveryCostConfiguration(int id)
        {
            deliveryCostConfigurationService.DeleteDeliveryCostConfiguration(id);
            return this.deliveryCostConfigurationService.GetAllDeliveryCostConfiguration();
        }
    }
}
    