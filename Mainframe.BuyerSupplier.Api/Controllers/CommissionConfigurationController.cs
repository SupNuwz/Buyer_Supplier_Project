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
    [Route("api/CommissionConfiguration")]
    public class CommissionConfigurationController : ControllerBase
    {
        private ICommissionConfigurationBusinessEntity commissionService;
        public CommissionConfigurationController(ICommissionConfigurationBusinessEntity commissionService)
        {
            this.commissionService = commissionService;
        }

        [HttpGet]
        public IEnumerable<CommissionConfigurationDto> GetCommissionConfiguration()
        {
            return this.commissionService.GetCommissionConfiguration();
        }

        [HttpPost]
        public void AddCommission([FromBody]CommissionConfigurationDto value)
        {
            commissionService.AddCommission(value);
        }

        [HttpGet("{id}")]
        public CommissionConfigurationDto Get(int id)
        {
            return commissionService.GetCommission(id);
        }

        [HttpDelete("{id}")]
        public IEnumerable<CommissionConfigurationDto> DeleteCommission(int id)
        {
            commissionService.DeleteCommission(id);
            return this.commissionService.GetCommissionConfiguration();
        }

        [HttpPut]
        public IEnumerable<CommissionConfigurationDto> UpdateCommission([FromBody]CommissionConfigurationDto value)
        {
            commissionService.UpdateCommission(value);
            return this.commissionService.GetCommissionConfiguration();
        }
    }
}