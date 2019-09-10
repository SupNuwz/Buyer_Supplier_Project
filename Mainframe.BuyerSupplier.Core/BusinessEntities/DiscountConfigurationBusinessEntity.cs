using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IDiscountConfigurationBusinessEntity
    {
        List<DiscountConfigurationDto> GetDiscountConfiguration();
        void AddDiscount(DiscountConfigurationDto value);
        DiscountConfigurationDto GetDiscount(int id);
        void DeleteDiscount(int id);
        void UpdateDiscount(DiscountConfigurationDto discountConfigurationDto);
    }

    public class DiscountConfigurationBusinessEntity : IDiscountConfigurationBusinessEntity
    {
        private IDiscountConfigurationDataService iDiscountConfigurationDataService;

        public DiscountConfigurationBusinessEntity(IDiscountConfigurationDataService iDiscountConfigurationDataService)
        {
            this.iDiscountConfigurationDataService = iDiscountConfigurationDataService;
        }

        public List<DiscountConfigurationDto> GetDiscountConfiguration()
        {
            var discounts = iDiscountConfigurationDataService.GetAllDiscount();

            var discountDtoList = discounts.Select(d => new DiscountConfigurationDto()
            {
                ID = d.ID,
                Name = d.Name,
                FromDate = d.FromDate,
                ToDate = d.ToDate,
                FromTime = d.FromTime,
                ToTime = d.ToTime,
                Rate = d.Rate
            }).ToList();
            return discountDtoList;
        }

        public void AddDiscount(DiscountConfigurationDto value)
        {
            var discountConfiguration = new DiscountConfiguration();

            discountConfiguration.ID = value.ID;
            discountConfiguration.Name = value.Name;
            discountConfiguration.FromDate = value.FromDate;
            discountConfiguration.ToDate = value.ToDate;
            discountConfiguration.FromTime = value.FromTime;
            discountConfiguration.ToTime = value.ToTime;
            discountConfiguration.Rate = value.Rate;
            
            this.iDiscountConfigurationDataService.AddDiscount(discountConfiguration);
        }

        public DiscountConfigurationDto GetDiscount(int id)
        {
            var discount = this.iDiscountConfigurationDataService.GetDiscount(id);

            var discountDto = new DiscountConfigurationDto();

            discountDto.ID = discount.ID;
            discountDto.Name = discount.Name;
            discountDto.FromDate = discount.FromDate;
            discountDto.ToDate = discount.ToDate;
            discountDto.FromTime = discount.FromTime;
            discountDto.ToTime = discount.ToTime;
            discountDto.Rate = discount.Rate;
            
            return discountDto;
        }

        public void DeleteDiscount(int id)
        {
            var discount = this.iDiscountConfigurationDataService.GetDiscount(id);
            discount.IsDeleted = true;
            this.iDiscountConfigurationDataService.SaveChanges();
        }

        public void UpdateDiscount(DiscountConfigurationDto discountConfigurationDto)
        {
            var discount = this.iDiscountConfigurationDataService.GetDiscount(discountConfigurationDto.ID);
            discount.ID = discountConfigurationDto.ID;
            discount.Name = discountConfigurationDto.Name;
            discount.FromDate = discountConfigurationDto.FromDate;
            discount.ToDate = discountConfigurationDto.ToDate;
            discount.FromTime = discountConfigurationDto.FromTime;
            discount.ToTime = discountConfigurationDto.ToTime;
            discount.Rate = discountConfigurationDto.Rate;

            this.iDiscountConfigurationDataService.SaveChanges();
        }
    }
}