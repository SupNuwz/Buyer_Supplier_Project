using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IDiscountConfigurationDataService : IBaseDataService
    {
        IEnumerable<DiscountConfiguration> GetAllDiscount();
        void AddDiscount(DiscountConfiguration discountConfiguration);
        DiscountConfiguration GetDiscount(int DiscountID);

    }
    public class DiscountConfigurationDataService: BaseDataService, IDiscountConfigurationDataService
    {
        private DatabaseContext databaseContext;
        public DiscountConfigurationDataService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<DiscountConfiguration> GetAllDiscount()
        {
            var discount = (from d in databaseContext.DiscountConfiguration
                        where d.IsDeleted == false
                        select d).ToList();
            return discount;
        }

        public void AddDiscount(DiscountConfiguration discountConfiguration)
        {
            databaseContext.DiscountConfiguration.Add(discountConfiguration);
            databaseContext.SaveChanges();
        }

        public DiscountConfiguration GetDiscount(int DiscountID)
        {
            var discounts = from d in databaseContext.DiscountConfiguration
                        where d.ID == DiscountID
                        select d;

            return discounts.FirstOrDefault();
        }
    }
}
