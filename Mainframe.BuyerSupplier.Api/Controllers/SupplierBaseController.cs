using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/supplier")]
    public class SupplierBaseController : Controller
    {
        private ISupplierBaseBusinessEntity SupplierBaseService;
        public SupplierBaseController(ISupplierBaseBusinessEntity SupplierBaseService)
        {
            this.SupplierBaseService = SupplierBaseService;
        }
        // GET: api/Zone
        [HttpGet]
        public IEnumerable<SupplierBaseDto> Get()
        {
            return this.SupplierBaseService.GetAllSupplierBases();
        }

        [HttpGet("ActiveSupplier")]
        public IEnumerable<SupplierBaseDto> GetActiveSupplierBases()
        {
            return this.SupplierBaseService.GetAllActiveSupplierBases();
        }

        // GET: api/Zone/5
        [HttpGet("{id}")]
        public SupplierBaseDto Get(int id)
        {
            return SupplierBaseService.GetSupplierBase(id);
        }

        // POST: api/Zone
        [HttpPost]
        public void AddSupplierBase([FromBody]SupplierBaseDto value)
        {
            SupplierBaseService.AddSupplierBase(value);
           
        }

        [HttpPost("Update")]
        public void UpdateSupplierBase([FromBody]SupplierBaseDto value)
        {
            SupplierBaseService.UpdateSupplierBase(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IEnumerable<SupplierBaseDto> DeleteSupplierBase(int id)
        {
            SupplierBaseService.DeleteSupplierBase(id);
            return this.SupplierBaseService.GetAllSupplierBases();
        }

        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.SupplierBaseService.IsItemAvailable(itemName, itemID);
        }
    }
}