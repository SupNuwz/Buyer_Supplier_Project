using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IUserBusinessEntity
    {
        int AddUser(UserDto value);
        List<UserDto> GetUsers();
        UserDto GetUser(int id);
        void DeleteUser(int userId);
        void UpdateUser(UserDto userDto);

        UserCreationInitialDataDto GetCreationUserCreationInitialData(int userId);

    }
    public class UserBusinessEntity:IUserBusinessEntity

    {
        private IUserService userService;
        ISupplierBaseBusinessEntity supplierBaseBusinessEntity;
        IDeliverySlotsBusinessEntity deliverySlotsBusinessEntity;
        IStandardInventoryBusinessEntity standardInventoryBusinessEntity;
        ISupplierStandardInventoryBusinessEntity supplierStandardInventoryBusinessEntity;
        IZoneBusinessEntity zoneBusinessEntity;


        public UserBusinessEntity(IUserService userService, 
            ISupplierBaseBusinessEntity supplierBaseBusinessEntity, 
            IDeliverySlotsBusinessEntity deliverySlotsBusinessEntity,
            IStandardInventoryBusinessEntity standardInventoryBusinessEntity,
            ISupplierStandardInventoryBusinessEntity supplierStandardInventoryBusinessEntity,
            IZoneBusinessEntity zoneBusinessEntity)

        {
            this.userService = userService;
            this.supplierBaseBusinessEntity = supplierBaseBusinessEntity;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
            this.deliverySlotsBusinessEntity = deliverySlotsBusinessEntity;
            this.supplierStandardInventoryBusinessEntity = supplierStandardInventoryBusinessEntity;
            this.zoneBusinessEntity = zoneBusinessEntity;
        }
        public int AddUser(UserDto value)
        {
            var user = new Users();
            user.ID = value.ID;
            user.Name = value.Name;
            user.Address = value.Address;
            user.Email = value.Email;
            user.ContactNo = value.ContactNo;
            user.UserType = value.UserType;
            user.DefaultSupplierBaseId = value.DefaultSupplierBaseId;
            user.DeliverySlotId = value.DeliverySlotId;
            user.Category = value.Category;
            user.StandaradInventoryID = value.StandaradInventoryID;
            user.RelevantZoneId = value.RelevantZoneId;
           
            return this.userService.AddUser(user);
        }
        public List<UserDto> GetUsers()
        {
            var userDtos = userService.GetUsers();

            var supplierBases = supplierBaseBusinessEntity.GetAllActiveSupplierBases();
            var deliverySlots = deliverySlotsBusinessEntity.GetDeliverySlots();
            var standaradInventory = standardInventoryBusinessEntity.GetStandardInventories();
            var zones = zoneBusinessEntity.GetZones();

            var userDtoList = userDtos.Select(p => new UserDto()
            {   ID = p.ID, Name = p.Name, Address = p.Address, ContactNo = p.ContactNo,
                Email =p.Email,UserType=p.UserType,DefaultSupplierBaseId=p.DefaultSupplierBaseId,DeliverySlotId=p.DeliverySlotId,
               Category=p.Category,StandaradInventoryID=p.StandaradInventoryID,RelevantZoneId=p.RelevantZoneId
            }).ToList();

            userDtoList.ForEach(p=> {
                var supplierBase = supplierBases.FirstOrDefault(s => s.SupplierBaseId == p.DefaultSupplierBaseId);

                if(supplierBase != null)
                {
                    p.DefaultSupplierBaseName = supplierBase.SupplierBaseName;
                }

            });
            userDtoList.ForEach(p => {
                var deliverySlot = deliverySlots.FirstOrDefault(s => s.ID == p.DeliverySlotId);

                if (deliverySlot != null)
                {
                    p.DeliverySlotName = deliverySlot.SlotName;
                }

            });
            userDtoList.ForEach(p => {
                var supplierStandardInventories = standaradInventory.FirstOrDefault(s => s.ID == p.StandaradInventoryID);

                if (supplierStandardInventories != null)
                {
                    p.StandaradInventoryName = supplierStandardInventories.ItemName;
                }

            });
            userDtoList.ForEach(p =>
            {
                var zone = zones.FirstOrDefault(s => s.ID == p.RelevantZoneId);
                if (zone != null)
                {
                    p.RelevantZoneName = zone.Name;
                }
            });


            return userDtoList;
        }
      
        public UserDto GetUser(int id)
        {
            var user = this.userService.GetUser(id);

            var userDto = new UserDto();

            userDto.ID = user.ID;
            userDto.Name = user.Name;
            userDto.Address = user.Address;
            userDto.ContactNo = user.ContactNo;
            userDto.Email = user.Email;
            userDto.UserType = user.UserType;
            userDto.DefaultSupplierBaseId = user.DefaultSupplierBaseId;
            userDto.DeliverySlotId = user.DeliverySlotId;
            userDto.StandaradInventoryID = user.StandaradInventoryID;
            userDto.Category = user.Category;
            userDto.RelevantZoneId = user.RelevantZoneId;
         
            var SupplierBases = supplierBaseBusinessEntity.GetAllActiveSupplierBases();
            var supplierBase= SupplierBases.FirstOrDefault(p => p.SupplierBaseId == userDto.DefaultSupplierBaseId);

            var deliverySlots = deliverySlotsBusinessEntity.GetDeliverySlots();
            var deliverySlot = deliverySlots.FirstOrDefault(p => p.ID == userDto.DeliverySlotId);

            var supplierStandardInventories = supplierStandardInventoryBusinessEntity.GetSupplierStandardInventories(user.ID);
            var supplierStandardInventory = supplierStandardInventories.FirstOrDefault(p => p.StandardInventoryId == userDto.StandaradInventoryID);

            var zones = zoneBusinessEntity.GetZones();
            var zone = zones.FirstOrDefault(p => p.ID == userDto.RelevantZoneId);

            if(supplierBase != null)
            {
                userDto.DefaultSupplierBaseName = supplierBase.SupplierBaseName;
            }
            if (deliverySlot != null)
            {
                userDto.DeliverySlotName = deliverySlot.SlotName;
            }
            if (zone != null)
            {
                userDto.RelevantZoneName = zone.Name;
            }
            return userDto;

        }

        public UserCreationInitialDataDto GetCreationUserCreationInitialData(int userId)
        {
            UserCreationInitialDataDto userCreationInitialDataDto = new UserCreationInitialDataDto();
            userCreationInitialDataDto.SupplierBaseList = supplierBaseBusinessEntity.GetAllActiveSupplierBases();
            userCreationInitialDataDto.DeliverySlotList = deliverySlotsBusinessEntity.GetDeliverySlots();
            userCreationInitialDataDto.ZoneList = zoneBusinessEntity.GetZones();

            if (userId > 0)
            {
                userCreationInitialDataDto.StandardInventoryList = supplierStandardInventoryBusinessEntity.GetSupplierStandardInventories(userId);

            }
            else
            {
                userCreationInitialDataDto.StandardInventoryList = supplierStandardInventoryBusinessEntity.GetInitialSupplierStandardInventories();
            }

            return userCreationInitialDataDto;
        }
        public void DeleteUser(int userId)

        {
            var user = this.userService.GetUser(userId);
            user.IsDeleted = true;
            this.userService.SaveChanges();
        }
        public void UpdateUser(UserDto userDto)
        {
            var user = this.userService.GetUser(userDto.ID);
            user.ID = userDto.ID;
            user.Name = userDto.Name;
            user.Address = userDto.Address;
            user.ContactNo = userDto.ContactNo;
            user.Email = userDto.Email;
            user.UserType = userDto.UserType;
            user.DefaultSupplierBaseId = userDto.DefaultSupplierBaseId;
            user.DeliverySlotId = userDto.DeliverySlotId;
            user.StandaradInventoryID = user.StandaradInventoryID;
            user.Category = userDto.Category;
            user.RelevantZoneId = user.RelevantZoneId;
            this.userService.SaveChanges();
        }

    }
}
