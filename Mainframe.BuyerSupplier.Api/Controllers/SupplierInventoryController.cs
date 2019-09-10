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
    [Route("api/SupplierInventory")]
    public class SupplierInventoryController : Controller
    {
        private ISupplierInventoryBusinessEntity supplierInventoryService;
        public SupplierInventoryController(ISupplierInventoryBusinessEntity supplierInventoryService)
        {
            this.supplierInventoryService = supplierInventoryService;
        }
        // GET: api/SupplierInventory
        [HttpGet]
        public IEnumerable<SupplierInventoryDto> GetAllSupplierInventories()
        {
            return this.supplierInventoryService.GetSupplierInventories();
        }

        // GET: api/SupplierInventory/5
        [HttpGet("{id}", Name = "Get")]
        public SupplierInventoryDto GetSupplierInventory(int id)
        {
            return this.supplierInventoryService.GetSupplierInventory(id);
        }

        // GET: api/SupplierInventory/5
        [HttpGet("{userId}")]
        public IEnumerable<SupplierInventoryDto> GetSupplierWiseSupplierInventory(int userId)
        {
            return supplierInventoryService.GetSupplierWiseSupplierInventories(userId);
        }
        
        // GET: api/SupplierInventory/standardInventory
        [HttpGet("standardInventory/{supplierStandardInventoryId}")]
        public IEnumerable<SupplierInventoryDto> GetSupplierStandardInventoryWiseSupplierInventory(int supplierStandardInventoryId)
        {
            return supplierInventoryService.GetSupplierStandardInventoryWiseSupplierInventory(supplierStandardInventoryId);
        }

        // POST: api/SupplierInventory
        [HttpPost]
        public void AddSupplierInventory ([FromBody]SupplierInventoryDto value)
        {
            this.supplierInventoryService.AddSupplierInventory(value);
        }
        
        // PUT: api/SupplierInventory/5
        [HttpPut("{id}")]
        public void UpdateSupplierInventory(int id, [FromBody]SupplierInventoryDto value)
        {
            this.supplierInventoryService.UpdateSupplierInventory(value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteSupplierInventory(int id)
        {
            this.supplierInventoryService.DeleteSupplierInventory(id);
        }
    }
}
