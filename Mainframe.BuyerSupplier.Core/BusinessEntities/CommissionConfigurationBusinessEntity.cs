using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface ICommissionConfigurationBusinessEntity
    {
        List<CommissionConfigurationDto> GetCommissionConfiguration();
        void AddCommission(CommissionConfigurationDto value);
        CommissionConfigurationDto GetCommission(int id);
        void DeleteCommission(int id);
        void UpdateCommission(CommissionConfigurationDto commissionConfigurationDto);
    }

    public class CommissionConfigurationBusinessEntity: ICommissionConfigurationBusinessEntity
    {
        private ICommissionConfigurationDataService iCommissionConfigurationDataService;

        public CommissionConfigurationBusinessEntity(ICommissionConfigurationDataService iCommissionConfigurationDataService)
        {
            this.iCommissionConfigurationDataService = iCommissionConfigurationDataService;
        }

        public List<CommissionConfigurationDto> GetCommissionConfiguration()
        {
            var commissions = iCommissionConfigurationDataService.GetAllCommission();

            var commissionDtoList = commissions.Select(c => new CommissionConfigurationDto()
            {
                ID = c.ID,
                Name = c.Name,
                FromDate = c.FromDate,
                ToDate = c.ToDate,
                FromTime = c.FromTime,
                ToTime = c.ToTime,
                Rate = c.Rate
            }).ToList();
            return commissionDtoList;
        }

        public void AddCommission(CommissionConfigurationDto value)
        {
            var commissionConfiguration = new CommissionConfiguration();

            commissionConfiguration.ID = value.ID;
            commissionConfiguration.Name = value.Name;
            commissionConfiguration.FromDate = value.FromDate;
            commissionConfiguration.ToDate = value.ToDate;
            commissionConfiguration.FromTime = value.FromTime;
            commissionConfiguration.ToTime = value.ToTime;
            commissionConfiguration.Rate = value.Rate;

            this.iCommissionConfigurationDataService.AddCommission(commissionConfiguration);
        }

        public CommissionConfigurationDto GetCommission(int id)
        {
            var commission = this.iCommissionConfigurationDataService.GetCommission(id);

            var commissionDto = new CommissionConfigurationDto();

            commissionDto.ID = commission.ID;
            commissionDto.Name = commission.Name;
            commissionDto.FromDate = commission.FromDate;
            commissionDto.ToDate = commission.ToDate;
            commissionDto.FromTime = commission.FromTime;
            commissionDto.ToTime = commission.ToTime;
            commissionDto.Rate = commission.Rate;

            return commissionDto;
        }

        public void DeleteCommission(int id)
        {
            var commission = this.iCommissionConfigurationDataService.GetCommission(id);
            commission.IsDeleted = true;
            this.iCommissionConfigurationDataService.SaveChanges();
        }

        public void UpdateCommission(CommissionConfigurationDto commissionConfigurationDto)
        {
            var commission = this.iCommissionConfigurationDataService.GetCommission(commissionConfigurationDto.ID);
            commission.ID = commissionConfigurationDto.ID;
            commission.Name = commissionConfigurationDto.Name;
            commission.FromDate = commissionConfigurationDto.FromDate;
            commission.ToDate = commissionConfigurationDto.ToDate;
            commission.FromTime = commissionConfigurationDto.FromTime;
            commission.ToTime = commissionConfigurationDto.ToTime;
            commission.Rate = commissionConfigurationDto.Rate;

            this.iCommissionConfigurationDataService.SaveChanges();
        }
    }
}
