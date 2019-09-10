using Mainframe.BuyerSupplier.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.DataServices;

namespace Mainframe.BuyerSupplier.Core.BusinessEntities
{
    public interface IWatchListBusinessEntity
    {
        List<WatchListDto> GetAllRelatedSuppliers(int supplierBaseId);
    }

    public class WatchListBusinessEntity : IWatchListBusinessEntity
    {
        private
                //IWatchListBusinessEntity watchListBusinessEntity;
                IUserBusinessEntity userBusinessEntity;
                ISupplierStandardInventoryBusinessEntity supplierStandardInventoryBusinessEntity;
                ISupplierInventoryBusinessEntity supplierInventoryBusinessEntity;
                ISupplierBaseBusinessEntity supplierBaseBusinessEntity;
        private IStandardInventoryBusinessEntity standardInventoryBusinessEntity;

        public WatchListBusinessEntity(
                                       //IWatchListBusinessEntity watchListBusinessEntity,
                                       IUserBusinessEntity userBusinessEntity,
                                       ISupplierStandardInventoryBusinessEntity supplierStandardInventoryBusinessEntity,
                                       ISupplierInventoryBusinessEntity supplierInventoryBusinessEntity,
                                       ISupplierBaseBusinessEntity supplierBaseBusinessEntity,
                                       IStandardInventoryBusinessEntity standardInventoryBusinessEntity)
        {
            //this.watchListBusinessEntity = watchListBusinessEntity;
            this.userBusinessEntity = userBusinessEntity;
            this.supplierStandardInventoryBusinessEntity = supplierStandardInventoryBusinessEntity;
            this.supplierInventoryBusinessEntity = supplierInventoryBusinessEntity;
            this.supplierBaseBusinessEntity = supplierBaseBusinessEntity;
            this.standardInventoryBusinessEntity = standardInventoryBusinessEntity;
        }


        public List<WatchListDto> GetAllRelatedSuppliers(int supplierBaseId)
        {
            var suppliers = userBusinessEntity.GetUsers().Where(r => r.UserType == "Supplier" && r.DefaultSupplierBaseId == supplierBaseId);
            var supplierIds = suppliers.Select(r => r.ID).ToList();
            var supplierStandardInventories = supplierStandardInventoryBusinessEntity.GetInitialSupplierStandardInventories().Where(r => supplierIds.Contains(r.SupplierId)).ToList();
            var supplierInventories = supplierInventoryBusinessEntity.GetSupplierInventories().Where(r => !r.IsFreeze && !r.IsDeleted);
            var standardInventories = standardInventoryBusinessEntity.GetStandardInventories();

            var watchListItems = (from stdInv in standardInventories
                                  join supStdInv in supplierStandardInventories on stdInv.ID equals supStdInv.StandardInventoryId
                                  join supInv in supplierInventories on supStdInv.Id equals supInv.SupplierStandardInventoryId
                                  select new
                                  {
                                      StandardInventoryId = stdInv.ID,
                                      SupplierInventoryId = supInv.ID
                                  });
            var groupedData = from wli in watchListItems
                              group wli by wli.StandardInventoryId into grouped
                              select new { StandardInventoryId = grouped.Key, InvnetoryCount = grouped.Count() };

            var itemwiseSupplierInventories = (from sup in suppliers
                                               join supStdInv in supplierStandardInventories on sup.ID equals supStdInv.SupplierId
                                               join stdInv in standardInventories on supStdInv.StandardInventoryId equals stdInv.ID
                                               join supInv in supplierInventories on supStdInv.Id equals supInv.SupplierStandardInventoryId
                                               join grpData in groupedData on stdInv.ID equals grpData.StandardInventoryId
                                               where grpData.InvnetoryCount == 1
                                               select new WatchListDto() {
                                                   SupplierId = sup.ID,
                                                   SupplierName = sup.Name,
                                                   StandardInventoryId = stdInv.ID,
                                                   StandardInventoryName=stdInv.ItemName,
                                                   Price=supInv.UnitPrice,
                                                   QuantityAvailable = supInv.AvailableQty
                                               }).ToList();

            return itemwiseSupplierInventories;
        }

    }
}
