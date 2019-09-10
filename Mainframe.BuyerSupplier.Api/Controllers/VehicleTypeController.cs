using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/VehicleType")]
    public class VehicleTypeController : Controller
    {

        private IVehicleTypeBusinessEntity vehicleTypeService;

        public VehicleTypeController(IVehicleTypeBusinessEntity vehicleTypeService)
        {
            this.vehicleTypeService = vehicleTypeService;
        }
        // GET: api/VehicleType
        [HttpGet]
        public IEnumerable<VehicleTypeDto> GetVehicleType()
        {
            return this.vehicleTypeService.GetVehicleType();
        }


        // POST: api/VehicleType
        [HttpPost]

        public void AddVehicleType([FromBody]VehicleTypeDto value)
        {
            vehicleTypeService.AddVehicleType(value);
        }


        [HttpGet("{id}")]
        public VehicleTypeDto Get(int id)
        {
            return vehicleTypeService.GetType(id);
        }


        //DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IEnumerable<VehicleTypeDto> DeleteVehicleType(int id)
        {
            vehicleTypeService.DeleteVehicleType(id);
            return this.vehicleTypeService.GetVehicleType();
        }


        [HttpPut]
        public IEnumerable<VehicleTypeDto> UpdateVehicleType([FromBody]VehicleTypeDto value)
        {
            vehicleTypeService.UpdateVehicleType(value);
            return this.vehicleTypeService.GetVehicleType();
        }

        //GET
        [HttpGet("{itemName}/{itemID}")]
        public bool IsItemAvailable(string itemName, int itemID)
        {
            return this.vehicleTypeService.IsItemAvailable(itemName, itemID);
        }

    }
}
