using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/SupplierStandardInventory")]
    public class SupplierStandardInventoryController : Controller
    {

        private ISupplierStandardInventoryBusinessEntity supplierStandardInventoryService;
        public SupplierStandardInventoryController(ISupplierStandardInventoryBusinessEntity supplierStandardInventoryService)
        {
            this.supplierStandardInventoryService = supplierStandardInventoryService;
        }

        [HttpPost]
        public void AddSupplierStandardInventory([FromBody]SupplierStandardInventoryDto[] value)
        {
            supplierStandardInventoryService.AddSupplierStandardInventory(value);
        }

        [HttpGet("user/{supplierId}")]
        public IEnumerable<SupplierStandardInventoryDto> GetBySupplier(int supplierId)
        {
            return this.supplierStandardInventoryService.GetSupplierStandardInventories(supplierId);
        }

        [HttpGet("supplierwiseselected/{supplierId}")]
        public IEnumerable<SupplierStandardInventoryDto> GetStandardInventoriesBySupplier(int supplierId)
        {
            return this.supplierStandardInventoryService.GetSelectedSupplierStandardInventories(supplierId);
        }

        [HttpGet("{id}")]
        public SupplierStandardInventoryDto Get(int id)
        {
            return supplierStandardInventoryService.GetSupplierStandardInventory(id);
        }

        [HttpDelete("supplier/{supplierId}/{id}")]
        public IEnumerable<SupplierStandardInventoryDto> DeleteSupplierStandardInventory(int supplierId, int id)
        {
            supplierStandardInventoryService.DeleteSupplierstandardInventory(id);
            return this.supplierStandardInventoryService.GetSupplierStandardInventories(supplierId);
        }

        [HttpPut()]
        public HttpStatusCode UpdateSupplierStandardInventory([FromBody]SupplierStandardInventoryDto[] value)
        {
            supplierStandardInventoryService.UpdateSupplierstandardInventory(value);

            return HttpStatusCode.OK;
        }
    }
}