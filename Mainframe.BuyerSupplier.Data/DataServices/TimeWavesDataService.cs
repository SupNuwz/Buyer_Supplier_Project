using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.Data_Services
{
    public interface ITimeWavesDataService : IBaseDataService
    {
        IEnumerable<TimeWave> GetTimeWaves();
        void AddWaves(TimeWave timeWave);
        TimeWave GetWaveById(int ID);
        bool IsItemAvailable(string itemName, int itemID);

    }

    public class TimeWavesDataService : BaseDataService, ITimeWavesDataService
    {
        private DatabaseContext databaseContext;
        public TimeWavesDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<TimeWave> GetTimeWaves()
        {
            var timeWave = (from c in databaseContext.TimeWave
                            where c.IsDeleted == false
                            select c).ToList();
            return timeWave;
        }

        public void AddWaves(TimeWave timeWave)
        {
            databaseContext.TimeWave.Add(timeWave);
            databaseContext.SaveChanges();
        }

        public TimeWave GetWaveById(int ID)
        {
            var timeWave = from c in databaseContext.TimeWave
                       where c.ID == ID
                       select c;
            return timeWave.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var timeWaveItem = from s in databaseContext.TimeWave
                               where ((s.Name == itemName)
                                      && (itemID == 0 || (itemID != s.ID && s.IsDeleted == false)))
                                select s;

            return timeWaveItem.Any();
        }

    }
}
