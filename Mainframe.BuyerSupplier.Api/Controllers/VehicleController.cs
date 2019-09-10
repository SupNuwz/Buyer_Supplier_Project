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
    [Route("api/Vehicle")]
    public class VehicleController : ControllerBase
    {
        private IVehicleBusinessEntity vehicleService;
        public VehicleController(IVehicleBusinessEntity vehicleService)
        {
            this.vehicleService = vehicleService;
        }


        // GET
        [HttpGet]
        public IEnumerable<VehicleDto> Get()
        {
            return this.vehicleService.GetAllVehicle();
        }

        // GET
        [HttpGet("ActiveConfiguration")]
        public IEnumerable<VehicleDto> GetActiveVehicle()
        {
            return this.vehicleService.GetAllActiveVehicle();
        }

        // GET: api/Zone/5
        [HttpGet("{id}")]
        public VehicleDto Get(int id)
        {
            return vehicleService.GetVehicle(id);
        }

        // POST
        [HttpPost]
        public void AddVehicle([FromBody]VehicleDto value)
        {
            vehicleService.AddVehicle(value);

        }

        // POST
        [HttpPost("Update")]
        public IEnumerable<VehicleDto> UpdateVehicle([FromBody]VehicleDto value)

        {
            vehicleService.UpdateVehicle(value);
            return this.vehicleService.GetAllActiveVehicle();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IEnumerable<VehicleDto> DeleteVehicle(int id)
        {
            vehicleService.DeleteVehicle(id);
            return this.vehicleService.GetAllVehicle();
        }
    }
}
 