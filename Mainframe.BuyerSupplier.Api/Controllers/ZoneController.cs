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
    [Route("api/Zone")]
    public class ZoneController : ControllerBase
    {
        private IZoneBusinessEntity zoneService;

        public ZoneController(IZoneBusinessEntity zoneService)
        {
            this.zoneService = zoneService;
        }

        [HttpGet]
        public IEnumerable<ZoneDto> GetZone()
        {
            return this.zoneService.GetZones();
        }

        [HttpPost]
        public void AddZone([FromBody]ZoneDto value)
        {
            zoneService.AddZone(value);
        }

        [HttpGet("{id}")]
        public ZoneDto Get(int id)
        {
            return zoneService.GetZone(id);
        }

        [HttpDelete("{id}")]
        public IEnumerable<ZoneDto> DeleteZone(int id)
        {
            zoneService.DeleteZone(id);
            return this.zoneService.GetZones();
        }

        [HttpPut]
        public IEnumerable<ZoneDto> UpdateZone([FromBody]ZoneDto value)
        {
            zoneService.UpdateZone(value);
            return this.zoneService.GetZones();
        }
    }
}