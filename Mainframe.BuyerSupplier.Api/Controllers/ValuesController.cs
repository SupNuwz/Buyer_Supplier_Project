using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<InventoryItemDto> Get()
        {
            return new List<InventoryItemDto>()
            {
                new InventoryItemDto(){ Id=1, Image = "carrots.png", Name = "Carrot"},
                new InventoryItemDto(){ Id=2, Image = "leeks.png", Name = "Leeks"},
                new InventoryItemDto(){ Id=3, Image = "beans.png", Name = "Beans"},
                new InventoryItemDto(){ Id=4, Image = "potatoes.png", Name = "Potatoes"},
                new InventoryItemDto(){ Id=5, Image = "tomatoes.png", Name = "Tomatoes"}
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
