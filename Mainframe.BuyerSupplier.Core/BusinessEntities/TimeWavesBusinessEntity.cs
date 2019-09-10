using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface ITimeWavesBusinessEntity
    {
        List<TimeWavesDto> GetTimeWaves();
        void AddWaves(TimeWavesDto value);

        TimeWavesDto GetWaveById(int id);
        void DeleteWaves(int id);
        void UpdateWaves(TimeWavesDto timeWavesDto);
        bool IsItemAvailable(string itemName, int itemID);
    }
    public class TimeWavesBusinessEntity : ITimeWavesBusinessEntity
    {
        private ITimeWavesDataService timeWavesDataService;
        public TimeWavesBusinessEntity(ITimeWavesDataService timeWavesDataService)
        {
            this.timeWavesDataService = timeWavesDataService;
        }


        public List<TimeWavesDto> GetTimeWaves()
        {
            var timeWave = timeWavesDataService.GetTimeWaves();

            var timeWavesDtoList = timeWave.Select(w => new TimeWavesDto() { ID = w.ID, Name = w.Name, Time = w.Time, Description = w.Description }).ToList();
            return timeWavesDtoList;
        }

        public void AddWaves(TimeWavesDto value)
        {
            var timeWave = new TimeWave();

            timeWave.ID = value.ID;
            timeWave.Name = value.Name;
            timeWave.Time = value.Time;
            timeWave.Description = value.Description;

            this.timeWavesDataService.AddWaves(timeWave);

        }

        public TimeWavesDto GetWaveById(int id)
        {
            var wave = this.timeWavesDataService.GetWaveById(id);

            var timeWavesDto = new TimeWavesDto();

            timeWavesDto.ID = wave.ID;
            timeWavesDto.Name = wave.Name;
            timeWavesDto.Time = wave.Time;
            timeWavesDto.Description = wave.Description;
            return timeWavesDto;
        }

        public void DeleteWaves(int id)
        {
            var wave = this.timeWavesDataService.GetWaveById(id);
            wave.IsDeleted = true;
            this.timeWavesDataService.SaveChanges();
        }

        public void UpdateWaves(TimeWavesDto timeWavesDto)
        {
            var wave = this.timeWavesDataService.GetWaveById(timeWavesDto.ID);

            wave.ID = timeWavesDto.ID;
            wave.Name = timeWavesDto.Name;
            wave.Time = timeWavesDto.Time;
            wave.Description = timeWavesDto.Description;

            this.timeWavesDataService.SaveChanges();

        }

        public bool IsItemAvailable(string itemName, int itemID)
        {
            var itemAvailability = this.timeWavesDataService.IsItemAvailable(itemName, itemID);

            return itemAvailability;
        }
    }
}