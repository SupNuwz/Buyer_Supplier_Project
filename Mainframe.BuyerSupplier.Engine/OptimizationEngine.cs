using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Common.Utility;
using System.Threading;

namespace Mainframe.BuyerSupplier.Engine
{
    public interface IOptimizationEngine
    {
        List<OrderOptimizedPossibility> SearchBestAvailbalities(Order order, List<OrderDetail> orderDetails);
    }

    public class OptimizationEngine : IOptimizationEngine
    {
        private ISupplierInventoryDataService supplierInventoryDataService;
        private ISupplierStandardInventoryService supplierStandardInventoryService;
        private ISupplierBaseService supplierBaseService;
        private IDeliveryCostConfigurationDataService deliveryCostConfigurationDataService;
        private IUserService userService;
        private IWatchListDataService watchListDataService;

        public OptimizationEngine(ISupplierInventoryDataService supplierInventoryDataService,
            ISupplierStandardInventoryService supplierStandardInventoryService,
            ISupplierBaseService supplierBaseService,
            IDeliveryCostConfigurationDataService deliveryCostConfigurationDataService,
            IUserService userService, IWatchListDataService watchListDataService)
        {
            this.supplierInventoryDataService = supplierInventoryDataService;
            this.supplierStandardInventoryService = supplierStandardInventoryService;
            this.supplierBaseService = supplierBaseService;
            this.deliveryCostConfigurationDataService = deliveryCostConfigurationDataService;
            this.userService = userService;
            this.watchListDataService = watchListDataService;
        }

        int index = 0;

        #region Select Final Best Possibilities
        /*
         * Select Lowest Price, Highest Quality & Best Optimal Options
         */
        public List<OrderOptimizedPossibility> SearchBestAvailbalities(Order order, List<OrderDetail> orderDetails)
        {
            lock (this)
            {
                var orderOptimizedPossibilities = new List<OrderOptimizedPossibility>();

                List<SupplierBase> supplierBases = supplierBaseService.GetAllActiveSupplierBases().ToList();

                if (supplierBases == null) throw new Exception("No Supplier Bases found");

                foreach (SupplierBase supplierBase in supplierBases)
                {
                    var optimizedPossibilities = GetOrderPosibilitiesBySupplierBase(orderDetails, supplierBase.SupplierBaseId, order.BuyerId, order.DeliverySlotId, order.SupplierCategory);
                    if (optimizedPossibilities != null && optimizedPossibilities.Count > 0) orderOptimizedPossibilities.AddRange(optimizedPossibilities);
                }
                if (orderOptimizedPossibilities == null || orderOptimizedPossibilities.Count == 0) return null;

                var returnArray = new List<OrderOptimizedPossibility>();
                OrderOptimizedPossibility orderOptimizedPossibility;

                if (order.OrderType == 2 || (order.OrderType == 1 && order.AssignmentSelectionType == 1))
                {
                    //Lowest Price Possibility
                    //var orderOptimizedPossibility = orderOptimizedPossibilities.Where(x => x.OrderPossibilityType == OrderPossibilityType.PRICE).OrderBy(r => r.OrderValue).FirstOrDefault();
                    orderOptimizedPossibility = orderOptimizedPossibilities.OrderBy(r => r.OrderValue).FirstOrDefault();
                    returnArray.Add(orderOptimizedPossibility);
                }

                if (order.OrderType == 2 || (order.OrderType == 1 && order.AssignmentSelectionType == 2))
                {
                    //Highest Quality Possibility
                    orderOptimizedPossibility = orderOptimizedPossibilities.Where(x => x.OrderPossibilityType == OrderPossibilityType.QUALITY).OrderByDescending(r => r.AverageSupplierQuality).FirstOrDefault();
                    returnArray.Add(orderOptimizedPossibility);
                }

                if (order.OrderType == 2 || (order.OrderType == 1 && order.AssignmentSelectionType == 3))
                {
                    var optimalPossibilities = orderOptimizedPossibilities.Where(x => x.OrderPossibilityType == OrderPossibilityType.OPTIMAL).ToList();
                    returnArray.AddRange(GetBestOptimals(optimalPossibilities));
                }

                UpdateSupplierInventoryProcessingQty(returnArray);

                Thread.Sleep(10000);

                return returnArray;
            }
        }
        #endregion

        private void UpdateSupplierInventoryProcessingQty(List<OrderOptimizedPossibility> orderOptimizedPossibilities)
        {
            if (orderOptimizedPossibilities == null) return;
            foreach (var orderOptimizedPossibility in orderOptimizedPossibilities)
            {
                foreach (var r in orderOptimizedPossibility.OrderOptimizedDetails)
                {
                    var supllierInventory = supplierInventoryDataService.GetSupplierInventory(r.SupplierInventoryID);
                    supllierInventory.ProcessingQty = supllierInventory.ProcessingQty + r.Qty;
                    supplierInventoryDataService.UpdateSupplierInventory(supllierInventory);
                }
            }
        }

        private List<OrderOptimizedPossibility> GetBestOptimals(List<OrderOptimizedPossibility> orderOptimizedPossibilities)
        {
            var priceWiseList = orderOptimizedPossibilities.OrderBy(r => r.OrderValue).Select(x => x.OrderOptimizedPossibilityId).ToList();
            var qualityWiseList = orderOptimizedPossibilities.OrderByDescending(r => r.AverageSupplierQuality)
                .Select(x => x.OrderOptimizedPossibilityId).ToList();
            var sortedOrderOptimizedPossibilities = orderOptimizedPossibilities.Select(r =>
                                                new
                                                {
                                                    OrderOptimizedPossibility = r,
                                                    PriceRank = priceWiseList.IndexOf(r.OrderOptimizedPossibilityId),
                                                    QualityRank = qualityWiseList.IndexOf(r.OrderOptimizedPossibilityId),
                                                    TotalRank = priceWiseList.IndexOf(r.OrderOptimizedPossibilityId) + qualityWiseList.IndexOf(r.OrderOptimizedPossibilityId)
                                                }).ToList();

            var bestOrderOptimals = sortedOrderOptimizedPossibilities.OrderBy(r => r.TotalRank).Take(1).Select(x => x.OrderOptimizedPossibility).ToList();
            return bestOrderOptimals;
        }

        private List<OrderOptimizedPossibility> GetOrderPosibilitiesBySupplierBase(List<OrderDetail> orderDetails, int supplierBase, int buyer, int deliverySlot, string category, 
            int? orderAssignmentType = null)
        {
            if (orderDetails == null) return null;

            var orderOptimizedPosibilities = new List<OrderOptimizedPossibility>();

            IEnumerable<SupplierStandardInventory> supplierStandardInventories = supplierStandardInventoryService.GetSupplierStandardInventories();
            var suppliers = userService.GetUsers().Where(r => r.DefaultSupplierBaseId == supplierBase && r.UserType == "Supplier"
                                && r.DeliverySlotId == deliverySlot && r.Category == category).ToList();

            var deliveryCost = CalculateDeliveryCost(supplierBase, buyer);

            var supplierBaseSupplierInventories = supplierInventoryDataService.GetSupplierInventoriesBySupplierIds(suppliers.Select(x => x.ID).ToList());

            var bestOptimalValues = GetBestOptimals(orderDetails, supplierStandardInventories, suppliers, supplierBaseSupplierInventories,deliverySlot);

            if (bestOptimalValues == null || bestOptimalValues.Count == 0) return null;

            var orderOptimizedDetailList = new List<List<OrderOptimizedDetail>>();
            var orderOptimizedDetails = new List<OrderOptimizedDetail>();
            decimal itemCost, supplierQualityAvg;

            if (orderAssignmentType == null || orderAssignmentType == 1)
            {
                orderOptimizedDetailList = bestOptimalValues.Where(x => x.OrderPossibilityType == OrderPossibilityType.PRICE).Select(r => r.OrderOptimizedDetails).ToList();
                orderOptimizedDetailList.ForEach(r => orderOptimizedDetails.AddRange(r));
                itemCost = orderOptimizedDetails.Sum(a => a.Value);
                supplierQualityAvg = orderOptimizedDetails.Average(a => a.SupplierQuality);
                orderOptimizedPosibilities.Add(new OrderOptimizedPossibility()
                {
                    OrderOptimizedPossibilityId = ++index,
                    OrderOptimizedDetails = orderOptimizedDetails,
                    DeliveryCost = deliveryCost,
                    ItemCost = itemCost,
                    OrderValue = itemCost + deliveryCost,
                    SupplierBaseId = supplierBase,
                    AverageSupplierQuality = supplierQualityAvg,
                    OrderPossibilityType = OrderPossibilityType.PRICE
                });
            }

            if (orderAssignmentType == null || orderAssignmentType == 2)
            {
                orderOptimizedDetailList = bestOptimalValues.Where(x => x.OrderPossibilityType == OrderPossibilityType.QUALITY).Select(r => r.OrderOptimizedDetails).ToList();
                orderOptimizedDetails = new List<OrderOptimizedDetail>();
                orderOptimizedDetailList.ForEach(r => orderOptimizedDetails.AddRange(r));
                itemCost = orderOptimizedDetails.Sum(a => a.Value);
                supplierQualityAvg = orderOptimizedDetails.Average(a => a.SupplierQuality);
                orderOptimizedPosibilities.Add(new OrderOptimizedPossibility()
                {
                    OrderOptimizedPossibilityId = ++index,
                    OrderOptimizedDetails = orderOptimizedDetails,
                    DeliveryCost = deliveryCost,
                    ItemCost = itemCost,
                    OrderValue = itemCost + deliveryCost,
                    SupplierBaseId = supplierBase,
                    AverageSupplierQuality = supplierQualityAvg,
                    OrderPossibilityType = OrderPossibilityType.QUALITY
                });
            }

            if (orderAssignmentType == null || orderAssignmentType == 3)
            {
                orderOptimizedDetailList = bestOptimalValues.Where(x => x.OrderPossibilityType == OrderPossibilityType.OPTIMAL && x.Rank == 1).Select(r => r.OrderOptimizedDetails).ToList();
                orderOptimizedDetails = new List<OrderOptimizedDetail>();
                orderOptimizedDetailList.ForEach(r => orderOptimizedDetails.AddRange(r));
                itemCost = orderOptimizedDetails.Sum(a => a.Value);
                supplierQualityAvg = orderOptimizedDetails.Average(a => a.SupplierQuality);
                orderOptimizedPosibilities.Add(new OrderOptimizedPossibility()
                {
                    OrderOptimizedPossibilityId = ++index,
                    OrderOptimizedDetails = orderOptimizedDetails,
                    DeliveryCost = deliveryCost,
                    ItemCost = itemCost,
                    OrderValue = itemCost + deliveryCost,
                    SupplierBaseId = supplierBase,
                    AverageSupplierQuality = supplierQualityAvg,
                    OrderPossibilityType = OrderPossibilityType.OPTIMAL
                });
            }
            //orderOptimizedDetailList = bestOptimalValues.Where(x => x.OrderPossibilityType == OrderPossibilityType.OPTIMAL && x.Rank == 2).Select(r => r.OrderOptimizedDetails).ToList();
            //orderOptimizedDetails = new List<OrderOptimizedDetail>();
            //orderOptimizedDetailList.ForEach(r => orderOptimizedDetails.AddRange(r));
            //itemCost = orderOptimizedDetails.Sum(a => a.Value);
            //supplierQualityAvg = orderOptimizedDetails.Average(a => a.SupplierQuality);
            //orderOptimizedPosibilities.Add(new OrderOptimizedPossibility()
            //{
            //    OrderOptimizedPossibilityId = ++index,
            //    OrderOptimizedDetails = orderOptimizedDetails,
            //    DeliveryCost = deliveryCost,
            //    ItemCost = itemCost,
            //    OrderValue = itemCost + deliveryCost,
            //    SupplierBaseId = supplierBase,
            //    AverageSupplierQuality = supplierQualityAvg,
            //    OrderPossibilityType = OrderPossibilityType.OPTIMAL,
            //    OrderPossibilityRank = 2
            //});

            return orderOptimizedPosibilities;
        }


        List<SupplierBaseWiseBestOptimals> GetBestOptimals(List<OrderDetail> orderDetails, IEnumerable<SupplierStandardInventory> supplierStandardInventories,
                List<Users> suppliers, List<SupplierInventory> supplierBaseSupplierInventories, int deliverySlot, int? orderAssignmentType = null)
        {
            List<SupplierBaseWiseBestOptimals> bestOptimalValues = new List<SupplierBaseWiseBestOptimals>();
            var orderOptimizedDetails = new List<OrderOptimizedDetail>();

            var supplierInventories = supplierBaseSupplierInventories.Where(r => (r.AvailableQty - r.ProcessingQty) > 0
                                            && r.IsDeleted == false && r.InventoryDate.Date == DateTime.Now.Date).ToList();

            var supplierIds = suppliers.Select(x => x.ID).ToList();
            foreach (OrderDetail orderDetail in orderDetails)
            {
                var selectedStandardInventories = supplierStandardInventories.Where(p => p.StandardInventoryId == orderDetail.StandardInventoryId &&
                                                        supplierIds.Contains(p.SupplierId)).Select(r => r.Id).ToList();

                var relevantSupplierInventories = supplierInventories.Where(r => selectedStandardInventories.Contains(r.SupplierStandardInventoryId)).ToList();

                if (relevantSupplierInventories.Count == 0) return null;
                else if (relevantSupplierInventories.Count == 1)
                {
                    var watchLit = new WatchList() {
                        DeliverySlotId = deliverySlot,
                        StandardInventoryId = orderDetail.StandardInventoryId,
                        SupplierId = supplierStandardInventories.Where(r=>r.Id == relevantSupplierInventories.FirstOrDefault().SupplierStandardInventoryId)
                                    .FirstOrDefault().SupplierId,
                        AddedDate = DateTime.Now.Date,
                        SupplierInventoryId = relevantSupplierInventories.FirstOrDefault().Id,
                        QOH = relevantSupplierInventories.FirstOrDefault().Qty,
                        UnitPrice= relevantSupplierInventories.FirstOrDefault().UnitPrice,
                        Status =1
                    };
                    watchListDataService.AddWatchListItem(watchLit);
                }
                
                    var priceRankedSupplierInventoryIds = relevantSupplierInventories.OrderBy(x => x.UnitPrice).Select(s => s.Id).ToList();

                var qualityRankedSupplierInventoryIds = (from si in relevantSupplierInventories
                                                         join ssi in supplierStandardInventories on si.SupplierStandardInventoryId equals ssi.Id
                                                         join s in suppliers on ssi.SupplierId equals s.ID
                                                         orderby s.SupplierQuality descending
                                                         select new { si.Id, s.SupplierQuality }).ToList();

                var supplierInventoryRankList = relevantSupplierInventories.Select(r =>
                                                new SupplierInventoryRanks()
                                                {
                                                    SupplierInventory = r,
                                                    PriceRank = priceRankedSupplierInventoryIds.IndexOf(r.Id),
                                                    QualityRank = qualityRankedSupplierInventoryIds.IndexOf(qualityRankedSupplierInventoryIds.Where(x => x.Id == r.Id).FirstOrDefault()),
                                                    SupplierQuality = qualityRankedSupplierInventoryIds.Where(x => x.Id == r.Id).Select(p => p.SupplierQuality).FirstOrDefault(),
                                                    TotalRank = priceRankedSupplierInventoryIds.IndexOf(r.Id) + qualityRankedSupplierInventoryIds.IndexOf(qualityRankedSupplierInventoryIds.Where(x => x.Id == r.Id).FirstOrDefault()),
                                                }).ToList();

                if (orderAssignmentType == null || orderAssignmentType == 1)
                {
                    var priceWiseSupplierInventories = supplierInventoryRankList.OrderBy(r => r.PriceRank).ToList();
                    bestOptimalValues.Add(new SupplierBaseWiseBestOptimals()
                    {
                        OrderPossibilityType = OrderPossibilityType.PRICE,
                        OrderOptimizedDetails = GetOrderOptimizedDetailsByOrderDetail(priceWiseSupplierInventories, orderDetail.Qty, orderDetail.ID),
                        Rank = 1
                    });
                }

                if (orderAssignmentType == null || orderAssignmentType == 2)
                {
                    var qualityWiseSupplierInventories = supplierInventoryRankList.OrderBy(r => r.QualityRank).ToList();
                    bestOptimalValues.Add(new SupplierBaseWiseBestOptimals()
                    {
                        OrderPossibilityType = OrderPossibilityType.QUALITY,
                        OrderOptimizedDetails = GetOrderOptimizedDetailsByOrderDetail(qualityWiseSupplierInventories, orderDetail.Qty, orderDetail.ID),
                        Rank = 1
                    });
                }

                if (orderAssignmentType == null || orderAssignmentType == 3)
                {
                    var optimalSupplierInventories = supplierInventoryRankList.OrderBy(r => r.TotalRank).ToList();
                    bestOptimalValues.Add(new SupplierBaseWiseBestOptimals()
                    {
                        OrderPossibilityType = OrderPossibilityType.OPTIMAL,
                        OrderOptimizedDetails = GetOrderOptimizedDetailsByOrderDetail(optimalSupplierInventories, orderDetail.Qty, orderDetail.ID),
                        Rank = 1
                    });
                }
                //optimalSupplierInventories = optimalSupplierInventories.Skip(1).ToList();
                //bestOptimalValues.Add(new SupplierBaseWiseBestOptimals()
                //{
                //    OrderPossibilityType = OrderPossibilityType.OPTIMAL,
                //    OrderOptimizedDetails = GetOrderOptimizedDetailsByOrderDetail(optimalSupplierInventories, orderDetail.Qty, orderDetail.ID),
                //    Rank = 2
                //});
            }


            return bestOptimalValues;
        }


        List<OrderOptimizedDetail> GetOrderOptimizedDetailsByOrderDetail(List<SupplierInventoryRanks> supplierInventoryRanks, decimal orderQty, int orderDetailId)
        {
            var orderOptimizedDetails = new List<OrderOptimizedDetail>();

            foreach (SupplierInventoryRanks supplierInventoryRank in supplierInventoryRanks)
            {
                decimal availQty = supplierInventoryRank.SupplierInventory.AvailableQty - supplierInventoryRank.SupplierInventory.ProcessingQty;
                decimal assignedQty;
                if (availQty > orderQty) assignedQty = orderQty;
                else assignedQty = availQty;

                orderQty -= assignedQty;

                orderOptimizedDetails.Add(new OrderOptimizedDetail
                {
                    OrderDetailID = orderDetailId,
                    SupplierInventoryID = supplierInventoryRank.SupplierInventory.Id,
                    Qty = assignedQty,
                    UnitPrice = supplierInventoryRank.SupplierInventory.UnitPrice,
                    Value = assignedQty * supplierInventoryRank.SupplierInventory.UnitPrice,
                    SupplierQuality = supplierInventoryRank.SupplierQuality
                });

                if (orderQty <= 0) break;
            }

            return orderOptimizedDetails;
        }

        #region Calculate Delivery Cost

        public decimal CalculateDeliveryCost(int supplierBaseId, int buyerId)
        {
            decimal deliveryCost = 0.0M;
            var userSupplierBases = supplierBaseService.GetUserWiseSupplierBases(buyerId);
            if (userSupplierBases == null) throw new Exception("Buyer Supplier Distance Configuration not Entered for relevent user");
            var userSupplierBase = userSupplierBases.Where(r => r.SupplierBaseID == supplierBaseId).FirstOrDefault();
            if (userSupplierBase == null) throw new Exception("Buyer Supplier Distance Configuration not Entered for supplier base" + supplierBaseId);
            var deliveryCostConfig = deliveryCostConfigurationDataService.GetSupplierBaseWiseDeliveryCostConfiguration(supplierBaseId);
            if (deliveryCostConfig == null) throw new Exception("Delivery Cost Configuration not Entered");
            decimal remainDistance = (userSupplierBase.Distance > deliveryCostConfig.BaseDistance) ? userSupplierBase.Distance - deliveryCostConfig.BaseDistance : 0;
            deliveryCost = deliveryCostConfig.BaseFare + (remainDistance * deliveryCostConfig.AdditionalRate);
            return deliveryCost;
        }

        #endregion
    }

    public class SupplierInventoryRanks
    {
        public SupplierInventory SupplierInventory { get; set; }
        public int PriceRank { get; set; }
        public int QualityRank { get; set; }
        public decimal SupplierQuality { get; set; }
        public int TotalRank { get; set; }
    }

    public class SupplierBaseWiseBestOptimals
    {
        public OrderPossibilityType OrderPossibilityType { get; set; }
        public List<OrderOptimizedDetail> OrderOptimizedDetails { get; set; }
        public int Rank { get; set; }
    }



}


/*
 1. supplier base 
 2.get order details wise best optimals
    --1. lawestr price
    --2. heighest quality
    --3. 2 best from best optimals - lowest price & highest quality
     */


//foreach (OrderDetail orderDetail in orderDetails)
//            {
//                List<int> selectedStandardInventories = supplierStandardInventories.Where(p => p.StandardInventoryId == orderDetail.StandardInventoryId &&
//                                                            suppliers.Contains(p.SupplierId)).Select(r => r.StandardInventoryId).ToList();

//List<SupplierInventory> supplierInventories = supplierInventoryDataService.GetSupplierInventoriesByStandardInventoryIds(selectedStandardInventories);

//supplierInventories = supplierInventories.Where(r => (r.AvailableQty - r.ProcessingQty) > 0
//                                        && r.IsDeleted == false && r.InventoryDate.Date == DateTime.Now.Date)
//                                        .OrderByDescending(x => x.UnitPrice).ToList();

//decimal orderQty = orderDetail.Qty;

//                foreach (SupplierInventory supplierInventory in supplierInventories)
//                {
//                    decimal availQty = supplierInventory.AvailableQty - supplierInventory.ProcessingQty;
//decimal assignedQty;
//                    if (availQty > orderDetail.Qty) assignedQty = orderDetail.Qty;
//                    else assignedQty = supplierInventory.Qty;

//                    orderAssignments.Add(new OrderOptimizedDetail
//                    {
//                        OrderDetailID = orderDetail.ID,
//                        SupplierInventoryID = supplierInventory.Id,
//                        Qty = assignedQty,
//                        UnitPrice = supplierInventory.UnitPrice,
//                        Value = assignedQty* supplierInventory.UnitPrice

//                    });

//                    if (orderQty - assignedQty <= 0) break;
//                }
//            }