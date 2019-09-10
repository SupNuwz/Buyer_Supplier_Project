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
    [Route("api/TimeWaves")]
    public class TimeWavesController : Controller
    {
        private ITimeWavesBusinessEntity timeWavesService;

        public TimeWavesController(ITimeWavesBusinessEntity timeWavesService)
        {
            this.timeWavesService = timeWavesService;
        }

        //GET
        [HttpGet]
        public IEnumerable<TimeWavesDto> GetTimeWaves()
        {
            return this.timeWavesService.GetTimeWaves();
        }

        // POST 
        [HttpPost]
        public void AddWaves([FromBody] TimeWavesDto value)
        {
            timeWavesService.AddWaves(value);
        }

        // GET
        [HttpGet("{id}")]
        public TimeWavesDto Get(int id)
        {
            return timeWavesService.GetWaveById(id);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public IEnumerable<TimeWavesDto> DeleteWaves(int id)
        {
            timeWavesService.DeleteWaves(id);
            return this.timeWavesService.GetTimeWaves();
        }

        //PUT
        [HttpPut]
        public IEnumerable<TimeWavesDto> UpdateWaves([FromBody]TimeWavesDto value)
        {
            timeWavesService.UpdateWaves(value);
            return this.timeWavesService.GetTimeWaves();
        }

        //GET
        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.timeWavesService.IsItemAvailable(itemName, itemID);
        }
    }
}