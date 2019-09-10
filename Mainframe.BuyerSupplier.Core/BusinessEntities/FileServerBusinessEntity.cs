using Mainframe.BuyerSupplier.Common.Utility;
using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IFileServerBusinessEntity
    {
        int AddFile(FileServerDto value);
        List<FileServerDto> GetFileDetails(IEnumerable<int> fileIds);
    }
    public class FileServerBusinessEntity : IFileServerBusinessEntity
    {
        private IFileServerDataService fileServerDataService;

        public FileServerBusinessEntity(IFileServerDataService fileServerDataService) { 
            this.fileServerDataService = fileServerDataService;
        }
        public int AddFile(FileServerDto value)
        {
            var file = new FileServerDetail();
            file.BucketName = value.BucketName;
            file.Key = value.Key;
            return this.fileServerDataService.AddFiles(file);            
        }

        public List<FileServerDto> GetFileDetails(IEnumerable<int> fileIds)
        {
            return this.fileServerDataService.GetAllByIds(fileIds).
                Select(p=> new FileServerDto(){ ID = p.ID, BucketName= p.BucketName, Key = p.Key }
                ).ToList();
        }
    }

}
