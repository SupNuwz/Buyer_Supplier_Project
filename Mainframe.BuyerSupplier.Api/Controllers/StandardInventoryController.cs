using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/StandardInventory")]
    public class StandardInventoryController : Controller
    {

        private IStandardInventoryBusinessEntity standardInventoryService;
   
        public StandardInventoryController(IStandardInventoryBusinessEntity standardInventoryService)
        {
            this.standardInventoryService = standardInventoryService;
          
        }
        // GET: api/StandardInventory
        [HttpGet]
        public IEnumerable<StandardInventoryDto> GetStandardInventory()
        {
            return this.standardInventoryService.GetStandardInventories();
        }

        [HttpGet("ActiveSupplier")]
        public IEnumerable<StandardInventoryDto> GetActiveStandardInventories()
        {
            return this.standardInventoryService.GetAllStandardInventories();
        }

        [HttpPost]
        public void AddInventory([FromBody]StandardInventoryDto value)
        {
            standardInventoryService.AddInventory(value);
        }

        [HttpGet("{id}")]
        public StandardInventoryDto Get(int id)
        {
            return standardInventoryService.GetInventory(id);
        }

        [HttpDelete("{id}")]
        public IEnumerable<StandardInventoryDto> DeleteInventory(int id)
        {
            standardInventoryService.DeleteInventory(id);
            return this.standardInventoryService.GetStandardInventories();
        }

        [HttpPut]
        public IEnumerable<StandardInventoryDto> UpdateInventory([FromBody]StandardInventoryDto value)
        {
            standardInventoryService.UpdateInventory(value);
            return this.standardInventoryService.GetStandardInventories();
        }

        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.standardInventoryService.IsItemAvailable(itemName, itemID);
        }

       
    }
}
