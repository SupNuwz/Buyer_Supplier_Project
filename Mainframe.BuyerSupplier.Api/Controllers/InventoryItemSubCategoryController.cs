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
    [Route("api/InventoryItemSubCategory")]
    public class InventoryItemSubCategoryController : Controller
    {
        private IInventoryItemSubCategoryBusinessEntity InventoryItemSubCategoryService;
        public InventoryItemSubCategoryController(IInventoryItemSubCategoryBusinessEntity InventoryItemSubCategoryService)
        {
            this.InventoryItemSubCategoryService = InventoryItemSubCategoryService;
        }
        // GET: api/Zone
        [HttpGet]
        public IEnumerable<InventoryItemSubCategoryDto> Get()
        {
            return this.InventoryItemSubCategoryService.GetAllInventoryItemSubCategory();
        }

        [HttpGet("ActiveSub")]
        public IEnumerable<InventoryItemSubCategoryDto> GetActiveInventoryItemSubCategory()
        {
            return this.InventoryItemSubCategoryService.GetAllActiveInventoryItemSubCategory();
        }

        // GET: api/Zone/5
        [HttpGet("{id}")]
        public InventoryItemSubCategoryDto Get(int id)
        {
            return InventoryItemSubCategoryService.GetInventoryItemSubCategory(id);
        }

        // POST: api/Zone
        [HttpPost]
        public void AddInventoryItemSubCategory([FromBody]InventoryItemSubCategoryDto value)
        {
            InventoryItemSubCategoryService.AddInventoryItemSubCategory(value);

        }

        [HttpPost("Update")]
        public void UpdateInventoryItemSubCategory([FromBody]InventoryItemSubCategoryDto value)
        {
            InventoryItemSubCategoryService.UpdateInventoryItemSubCategory(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IEnumerable<InventoryItemSubCategoryDto> DeleteInventoryItemSubCategory(int id)
        {
            InventoryItemSubCategoryService.DeleteInventoryItemSubCategory(id);
            return this.InventoryItemSubCategoryService.GetAllInventoryItemSubCategory();
        }
    }
}
