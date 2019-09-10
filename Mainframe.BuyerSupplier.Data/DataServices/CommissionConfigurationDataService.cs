using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface ICommissionConfigurationDataService : IBaseDataService
    {
        IEnumerable<CommissionConfiguration> GetAllCommission();
        void AddCommission(CommissionConfiguration commissionConfiguration);
        CommissionConfiguration GetCommission(int CommissionID);
    }
    public class CommissionConfigurationDataService: BaseDataService, ICommissionConfigurationDataService
    {
        private DatabaseContext databaseContext;
        public CommissionConfigurationDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public IEnumerable<CommissionConfiguration> GetAllCommission()
        {
            var commission = (from d in databaseContext.CommissionConfiguration
                            where d.IsDeleted == false
                            select d).ToList();
            return commission;
        }

        public void AddCommission(CommissionConfiguration commissionConfiguration)
        {
            databaseContext.CommissionConfiguration.Add(commissionConfiguration);
            databaseContext.SaveChanges();
        }

        public CommissionConfiguration GetCommission(int CommissionID)
        {
            var commissions = from d in databaseContext.CommissionConfiguration
                            where d.ID == CommissionID
                            select d;

            return commissions.FirstOrDefault();
        }
    }
}
