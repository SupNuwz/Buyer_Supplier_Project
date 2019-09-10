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
    [Route("api/UnitOfMeasure")]
    public class UnitOfMeasureController : Controller
    {
        
        private IUnitOfMeasureBusinessEntity unitOfMeasureService; 

        public UnitOfMeasureController(IUnitOfMeasureBusinessEntity unitOfMeasureService)
        {
            this.unitOfMeasureService = unitOfMeasureService;
        }
        // GET: api/UnitOfMeasure
        [HttpGet]
        public IEnumerable<UnitOfMeasureDto> GetUnitOfMeasure()
        {
            return this.unitOfMeasureService.GetUnitOfMeasure();
        }
     

        // POST: api/UnitOfMeasure
        [HttpPost]

        public void AddUnitOfMeasure([FromBody]UnitOfMeasureDto value)
        {
            unitOfMeasureService.AddUnitOfMeasure(value);
        }
        

        [HttpGet("{id}")]
        public UnitOfMeasureDto Get(int id)
        {
            return unitOfMeasureService.GetUnit(id);
        }


        //DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IEnumerable<UnitOfMeasureDto> DeleteUnitOfMeasure(int id)
        {
            unitOfMeasureService.DeleteUnitOfMeasure(id);
            return this.unitOfMeasureService.GetUnitOfMeasure();
        }


        [HttpPut]
        public IEnumerable<UnitOfMeasureDto> UpdateUnitOfMeasure([FromBody]UnitOfMeasureDto value)
        {
            unitOfMeasureService.UpdateUnitOfMeasure(value);
            return this.unitOfMeasureService.GetUnitOfMeasure();
        }

        //GET
        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.unitOfMeasureService.IsItemAvailable(itemName, itemID);
        }

    }
}
