using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IZoneDataService : IBaseDataService
    {
        IEnumerable<Zone> GetAllZone();
        void AddZone(Zone zone);
        Zone GetZone(int ZoneID);

    }
    public class ZoneDataService : BaseDataService, IZoneDataService
    {
        private DatabaseContext databaseContext;
        public ZoneDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<Zone> GetAllZone()
        {
            var zone = (from e in databaseContext.Zone
                                 where e.IsDeleted == false
                                 select e).ToList();
            return zone;
        }
        public void AddZone(Zone zone)
        {
            databaseContext.Zone.Add(zone);
            databaseContext.SaveChanges();
        }
        public Zone GetZone(int ZoneID)
        {
            var zones = from e in databaseContext.Zone
                            where e.ID == ZoneID
                            select e;

            return zones.FirstOrDefault();
        }
    }
}
