using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IVehicleDataService : IBaseDataService
    {
        IEnumerable<Vehicle> GetAllVehicle();
        IEnumerable<Vehicle> GetAllActiveVehicle();
        void AddVehicle(Vehicle vehicle);
        Vehicle GetVehicle(int ID);
    }

    public class VehicleDataService : BaseDataService, IVehicleDataService
    {
        private DatabaseContext dataContext;

        public VehicleDataService(DatabaseContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Vehicle> GetAllVehicle()
        {
            var vehicle = (from d in dataContext.Vehicle
                                             select d).ToList();
            return vehicle;
        }

        public IEnumerable<Vehicle> GetAllActiveVehicle()
        {
            var vehicle = (from d in dataContext.Vehicle
                                             where d.IsDeleted == false
                                             select d).ToList();
            return vehicle;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            dataContext.Vehicle.Add(vehicle);
            dataContext.SaveChanges();
        }

        public Vehicle GetVehicle(int ID)
        {
            var vehicle = from d in dataContext.Vehicle
                                            where d.ID == ID
                                            select d;

            return vehicle.FirstOrDefault();
        }
    }
}
