using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IUnitOfMeasureBusinessEntity
    {
        List<UnitOfMeasureDto> GetUnitOfMeasure();
        void AddUnitOfMeasure(UnitOfMeasureDto value);
        UnitOfMeasureDto GetUnit(int id);

        void DeleteUnitOfMeasure(int id);
        void UpdateUnitOfMeasure(UnitOfMeasureDto unitOfMeasureDto);
        bool IsItemAvailable(string itemName, int itemID);

    }
    public class UnitOfMeasureBusinessEntity : IUnitOfMeasureBusinessEntity
    {
        private IUnitOfMeasureDataService iunitOfMeasureDataService;
        public UnitOfMeasureBusinessEntity(IUnitOfMeasureDataService iunitOfMeasureDataService)
        {
            this.iunitOfMeasureDataService = iunitOfMeasureDataService;
        }

        public List<UnitOfMeasureDto> GetUnitOfMeasure()
        {
            var unitOfMeasure = iunitOfMeasureDataService.GetUnitOfMeasure();

            var unitOfMeasureDtoList = unitOfMeasure.Select(p => new UnitOfMeasureDto() { ID = p.ID, Name = p.Name,Description = p.Description }).ToList();
            return unitOfMeasureDtoList;
        }


        public void AddUnitOfMeasure(UnitOfMeasureDto value)
        {
            var unitOfMeasure = new UnitOfMeasure();

            unitOfMeasure.ID = value.ID;
            unitOfMeasure.Name = value.Name;
            unitOfMeasure.Description = value.Description;

            this.iunitOfMeasureDataService.AddUnitOfMeasure(unitOfMeasure);
        }


        public UnitOfMeasureDto GetUnit(int id)
        {
            var measure = this.iunitOfMeasureDataService.GetUnit(id);

            var measureDto = new UnitOfMeasureDto();

            measureDto.ID = measure.ID;
            measureDto.Name = measure.Name;
            measureDto.Description = measure.Description;
            return measureDto;
        }

        public void DeleteUnitOfMeasure(int id)
        {
            var measure = this.iunitOfMeasureDataService.GetUnit(id);
            measure.IsDeleted = true;
            this.iunitOfMeasureDataService.SaveChanges();
        }


        public void UpdateUnitOfMeasure(UnitOfMeasureDto unitOfMeasureDto) 
        {
            var measure = this.iunitOfMeasureDataService.GetUnit(unitOfMeasureDto.ID);
            measure.ID = unitOfMeasureDto.ID;
            measure.Name = unitOfMeasureDto.Name;
            measure.Description = unitOfMeasureDto.Description;

            this.iunitOfMeasureDataService.SaveChanges();
        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.iunitOfMeasureDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }


    }
}
