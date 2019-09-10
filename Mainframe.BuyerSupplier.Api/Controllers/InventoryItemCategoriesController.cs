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
    [Route("api/InventoryItemCategories")]
    public class InventoryItemCategoriesController : Controller
    {
        private IInventoryItemCategoriesBusinessEntity inventoryItemCategoriesService;

        public InventoryItemCategoriesController(IInventoryItemCategoriesBusinessEntity inventoryItemCategoriesService)
        {
            this.inventoryItemCategoriesService = inventoryItemCategoriesService;
        }

        //GET
        [HttpGet]
        public IEnumerable<InventoryItemCategoriesDto> GetInventoryItemCategories()
        {
            return this.inventoryItemCategoriesService.GetInventoryItemCategories();
        }

        // POST 
        [HttpPost]
        public void AddCategory([FromBody] InventoryItemCategoriesDto value)
        {
            inventoryItemCategoriesService.AddCategory(value);
        }

        // GET
        [HttpGet("{id}")]
        public InventoryItemCategoriesDto Get(int id)
        {
            return inventoryItemCategoriesService.GetCategory(id);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public IEnumerable<InventoryItemCategoriesDto> DeleteCategory(int id)
        {
            inventoryItemCategoriesService.DeleteCategory(id);
            return this.inventoryItemCategoriesService.GetInventoryItemCategories();
        }

        //PUT
        [HttpPut]
        public IEnumerable<InventoryItemCategoriesDto> UpdateCategory([FromBody]InventoryItemCategoriesDto value)
        {
            inventoryItemCategoriesService.UpdateCategory(value);
            return this.inventoryItemCategoriesService.GetInventoryItemCategories();
        }

        //GET
        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.inventoryItemCategoriesService.IsItemAvailable(itemName, itemID);
        }
    }
}