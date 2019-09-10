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
    [Route("api/DiscountConfiguration")]
    public class DiscountConfigurationController : ControllerBase
    {
        private IDiscountConfigurationBusinessEntity discountService;
        public DiscountConfigurationController(IDiscountConfigurationBusinessEntity discountService)
        {
            this.discountService = discountService;
        }

        [HttpGet]
        public IEnumerable<DiscountConfigurationDto> GetDiscountConfiguration()
        {
            return this.discountService.GetDiscountConfiguration();
        }

        [HttpPost]
        public void AddDiscount([FromBody]DiscountConfigurationDto value)
        {
            discountService.AddDiscount(value);
        }

        [HttpGet("{id}")]
        public DiscountConfigurationDto Get(int id)
        {
            return discountService.GetDiscount(id);
        }

        [HttpDelete("{id}")]
        public IEnumerable<DiscountConfigurationDto> DeleteDiscount(int id)
        {
            discountService.DeleteDiscount(id);
            return this.discountService.GetDiscountConfiguration();
        }

        [HttpPut]
        public IEnumerable<DiscountConfigurationDto> UpdateDiscount([FromBody]DiscountConfigurationDto value)
        {
            discountService.UpdateDiscount(value);
            return this.discountService.GetDiscountConfiguration();
        }
    }
}