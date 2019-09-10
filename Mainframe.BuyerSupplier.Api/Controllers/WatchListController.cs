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
    [Route("api/WatchList")]
    public class WatchListController : ControllerBase
    {
        private IWatchListBusinessEntity watchListService;
        public WatchListController(IWatchListBusinessEntity watchListService)
        {
            this.watchListService = watchListService;
        }

        [HttpGet("{supplierBaseId}")]
        public IEnumerable<WatchListDto>  Get(int supplierBaseId)
        {
            return watchListService.GetAllRelatedSuppliers(supplierBaseId);
        }
    }
}