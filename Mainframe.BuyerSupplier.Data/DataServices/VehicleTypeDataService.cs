using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IVehicleTypeDataService : IBaseDataService
    {
        IEnumerable<VehicleType> GetVehicleType();
        void AddVehicleType(VehicleType vehicleType);
        VehicleType GetType(int TypeID);
        bool IsItemAvailable(string itemName, int itemID);

    }


    public class VehicleTypeDataService : BaseDataService, IVehicleTypeDataService
    {
        private DatabaseContext databaseContext;
        public VehicleTypeDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public IEnumerable<VehicleType> GetVehicleType()
        {
            var vehicleType = (from e in databaseContext.VehicleType
              where e.IsDeleted == false
             select e).ToList();
            return vehicleType;
            
        }

        public void AddVehicleType(VehicleType vehicleType)
        {
            databaseContext.VehicleType.Add(vehicleType);
            databaseContext.SaveChanges();
        }

        public VehicleType GetType(int TypeID)
        {
            var type = from e in databaseContext.VehicleType
                       where e.ID == TypeID
                       select e;

            return type.FirstOrDefault();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {


            var vehicleTypeItem = from s in databaseContext.VehicleType
                                  where ((s.Name == itemName)
                                         && (itemID == 0 || (itemID != s.ID && s.IsDeleted == false)))
                                  select s;

            return vehicleTypeItem.Any();
        }
    }
}