using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IFileServerDataService:IBaseDataService
    {
        int AddFiles(FileServerDetail file);
        List<FileServerDetail> GetAllByIds(IEnumerable<int> fileIds);    
    }
    public class FileServerDataService: BaseDataService, IFileServerDataService
    {
        private DatabaseContext databaseContext;
        public FileServerDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public int AddFiles(FileServerDetail file)
        {
            databaseContext.FileServerDetail.Add(file);
            databaseContext.SaveChanges();

            return file.ID;
        }

        public List<FileServerDetail> GetAllByIds(IEnumerable<int> fileIds)
        {
            var file = (from e in databaseContext.FileServerDetail
                                      where fileIds.Contains(e.ID)
                                      select e).ToList();
            return file;
        }

    }
}
