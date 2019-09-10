using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IDeliveryCostConfigurationDataService : IBaseDataService
    {
        IEnumerable<DeliveryCostConfiguration> GetAllDeliveryCostConfiguration();
        IEnumerable<DeliveryCostConfiguration> GetAllActiveDeliveryCostConfiguration();
        void AddDeliveryCostConfiguration(DeliveryCostConfiguration deliveryCostConfiguration);
        DeliveryCostConfiguration GetDeliveryCostConfiguration(int ID);
        DeliveryCostConfiguration GetSupplierBaseWiseDeliveryCostConfiguration(int supplierBaseId);
    }

    public class DeliveryCostConfigurationDataService : BaseDataService, IDeliveryCostConfigurationDataService
    {
        private DatabaseContext dataContext;

        public DeliveryCostConfigurationDataService(DatabaseContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<DeliveryCostConfiguration> GetAllDeliveryCostConfiguration()
        {
            var deliveryCostConfiguration = (from d in dataContext.DeliveryCostConfiguration
                                             select d).ToList();
            return deliveryCostConfiguration;
        }

        public IEnumerable<DeliveryCostConfiguration> GetAllActiveDeliveryCostConfiguration()
        {
            var deliveryCostConfiguration = (from d in dataContext.DeliveryCostConfiguration
                                             where d.IsDeleted == false
                                             select d).ToList();
            return deliveryCostConfiguration;
        }

        public void AddDeliveryCostConfiguration(DeliveryCostConfiguration deliveryCostConfiguration)
        {
            dataContext.DeliveryCostConfiguration.Add(deliveryCostConfiguration);
            dataContext.SaveChanges();
        }

        public DeliveryCostConfiguration GetDeliveryCostConfiguration(int ID)
        {
            var deliveryCostConfiguration = from d in dataContext.DeliveryCostConfiguration
                                            where d.ID == ID
                                            select d;

            return deliveryCostConfiguration.FirstOrDefault();
        }

        public DeliveryCostConfiguration GetSupplierBaseWiseDeliveryCostConfiguration(int supplierBaseId)
        {
            var deliveryCostConfiguration = from d in dataContext.DeliveryCostConfiguration
                                            where d.BaseLocationID == supplierBaseId
                                            select d;

            return deliveryCostConfiguration.FirstOrDefault();
        }
    }
}
