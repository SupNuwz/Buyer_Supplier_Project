using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IUnitOfMeasureDataService : IBaseDataService
    {
        IEnumerable<UnitOfMeasure> GetUnitOfMeasure();
        void AddUnitOfMeasure(UnitOfMeasure unitOfMeasure);
        UnitOfMeasure GetUnit(int UnitID);
        bool IsItemAvailable(string itemName, int itemID);

    }

      public class UnitOfMeasureDataService: BaseDataService, IUnitOfMeasureDataService
    {
        private DatabaseContext databaseContext;
        public UnitOfMeasureDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public IEnumerable<UnitOfMeasure> GetUnitOfMeasure()
        {
            var unitOfMeasure = (from e in databaseContext.UnitOfMeasure
                                 where e.IsDeleted == false
                                      select e).ToList();
            return unitOfMeasure;
        }

        public void AddUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            databaseContext.UnitOfMeasure.Add(unitOfMeasure);
            databaseContext.SaveChanges();
        }

        public UnitOfMeasure GetUnit(int UnitID)
        {
            var measure = from e in databaseContext.UnitOfMeasure
                            where e.ID == UnitID
                            select e;

            return measure.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var unitOfMeasureItem = from s in databaseContext.UnitOfMeasure
                                    where ((s.Name == itemName)
                                           && (itemID == 0 || (itemID != s.ID && s.IsDeleted == false)))
                                    select s;

            return unitOfMeasureItem.Any();
        }

    }
}