using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface ISupplierBaseService : IBaseDataService
    {
        IEnumerable<SupplierBase> GetAllSupplierBases();
        IEnumerable<SupplierBase> GetAllActiveSupplierBases();
        SupplierBase GetSupplierBase(int SupplierBaseId);
        void AddSupplierBase(SupplierBase SupplierBase);
        IEnumerable<UserSupplierBase> GetUserWiseSupplierBases(int user);
        IEnumerable<UserSupplierBase> GetUserSupplierBase(int SupplierBaseId);
        bool IsItemAvailable(string itemName, int itemID);

    }
    public class SupplierBaseDataService : BaseDataService, ISupplierBaseService
    {
        private DatabaseContext dataContext;

        public SupplierBaseDataService(DatabaseContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SupplierBase> GetAllSupplierBases()
        {
            var SupplierBase = (from e in dataContext.SupplierBase
                        select e).ToList();
            return SupplierBase;
        }

        public IEnumerable<SupplierBase> GetAllActiveSupplierBases()
        {
            var SupplierBase = (from e in dataContext.SupplierBase
                        where e.IsDeleted == false
                        select e).ToList();
            return SupplierBase;
        }

        public void AddSupplierBase(SupplierBase SupplierBase)
        {
            dataContext.SupplierBase.Add(SupplierBase);
            dataContext.SaveChanges();
        }



        public SupplierBase GetSupplierBase(int SupplierBaseId)
        {
            var SupplierBase = from e in dataContext.SupplierBase
                       where e.SupplierBaseId == SupplierBaseId
                               select e;

            return SupplierBase.FirstOrDefault();
        }

        public IEnumerable<UserSupplierBase> GetUserWiseSupplierBases(int user)
        {
            var userSupplierBases = (from e in dataContext.UserSupplierBase
                                where e.UserID == user
                                select e).ToList();
            return userSupplierBases;
        }

        public  IEnumerable<UserSupplierBase> GetUserSupplierBase(int SupplierBaseId)
        {
            var userSupplierBases = from e in dataContext.UserSupplierBase
                                   where e.SupplierBaseID == SupplierBaseId
                               select e;

            return userSupplierBases;
        }


        public bool IsItemAvailable(string itemName, int itemID)
        {


            var supplierBaseItem = from s in dataContext.SupplierBase
                                   where ((s.SupplierBaseName == itemName)
                                          && (itemID == 0 || (itemID != s.SupplierBaseId && s.IsDeleted == false)))
                                   select s;

            return supplierBaseItem.Any();
        }

    } 
}
