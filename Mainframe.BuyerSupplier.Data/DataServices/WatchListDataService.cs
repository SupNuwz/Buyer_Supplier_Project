using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IWatchListDataService
    {
        void AddWatchListItem(WatchList watchList);
        void UpdateWatchListItem(WatchList watchList);
        IEnumerable<WatchList> GetWatchLists(int deliverySlotId);
        bool IsExistCheckStandardInventory(int standardInventoryId, int deliveryslotId);
    }
    public class WatchListDataService : BaseDataService, IWatchListDataService
    {
        private DatabaseContext databaseContext;
        public WatchListDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void AddWatchListItem(WatchList watchList)
        {
            if (!IsExistCheckStandardInventory(watchList.StandardInventoryId, watchList.DeliverySlotId))
            {
                databaseContext.WatchList.Add(watchList);
                databaseContext.SaveChanges();
            }
        }

        public bool IsExistCheckStandardInventory(int standardInventoryId, int deliveryslotId)
        {
            var watchList =  from w in databaseContext.WatchList
                              where w.StandardInventoryId == standardInventoryId && 
                              w.DeliverySlotId == deliveryslotId &&
                                      w.AddedDate == DateTime.Now.Date
                             select w;
            return watchList.Count()>0;
        }

        public IEnumerable<WatchList> GetWatchLists(int deliverySlotId)
        {
            var watchLists = (from e in databaseContext.WatchList
                                      where e.DeliverySlotId == deliverySlotId &&
                                      e.AddedDate == DateTime.Now.Date
                              select e).ToList();
            return watchLists;
        }

        public void UpdateWatchListItem(WatchList watchList)
        {
            databaseContext.WatchList.Add(watchList);
            databaseContext.SaveChanges();
        }
    }
}
