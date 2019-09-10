using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Common.Utility;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/FileOperation")]
    public class FileOperationController : Controller
    {
        private IFileServerBusinessEntity fileServer;
        public FileOperationController(IFileServerBusinessEntity fileServer)
        {
            this.fileServer = fileServer;
        }


        [HttpPost]
        public FileServerUrlDto AddFiles([FromBody]FileServerDto value)
        {
            int fileID =fileServer.AddFile(value);            

            string url= FileServerUtility.GetPresignedPutUrl(value.BucketName, value.Key).Result;

            return new FileServerUrlDto() { Id = fileID, Url = url };
        }


    }
}