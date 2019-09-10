import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component'
import {TableComponent} from './table/table.component'
import {StandardInventoryComponent} from './standard-inventory/standard-inventory.component'
import { StandardInventoryItemComponent } from './standard-inventory-item/standard-inventory-item.component';
import { UsersComponent } from './Users-List/users-list.component';
import { UserComponent } from './user/user.component';
import { SupplierBaseManagementComponent } from './supplierBaseManagement/supplierBaseManagement.component';
import { SupplierBaseItemComponent } from './supplierBaseItem/supplierBaseItem.component';
import { DeliverySlotsComponent } from './delivery-slots/delivery-slots.component';
import { DeliverySlotItemComponent } from './delivery-slot-item/delivery-slot-item.component';
import { InventoryItemSubCategoryItemComponent } from './inventory-item-sub-category-item/inventory-item-sub-category-item.component';
import { InventoryItemSubCategoryComponent } from './inventory-item-sub-category/inventory-item-sub-category.component';
import { UnitOfMeasureComponent } from './unit-of-measure/unit-of-measure.component';
import { UnitOfMeasureItemComponent } from './unit-of-measure-item/unit-of-measure-item.component';
import { InventoryItemCategoriesComponent } from './inventory-item-categories/inventory-item-categories.component';
import { IneventoryItemCategoryItemComponent } from './ineventory-item-category-item/ineventory-item-category-item.component';
import { TimeWavesComponent } from './time-waves/time-waves.component';
import { TimeWavesItemComponent } from './time-waves-item/time-waves-item.component';
import { DeliveryCostConfigurationComponent } from './delivery-cost-configuration/delivery-cost-configuration.component';
import { DeliveryCostConfigurationItemComponent } from './delivery-cost-configuration-item/delivery-cost-configuration-item.component';
import { SupplierInventoryComponent } from './supplier-inventory/supplier-inventory.component';
import { SupplierInventoryItemComponent } from './supplier-inventory-item/supplier-inventory-item.component'
import { SupplierInventoryListComponent } from "./supplier-inventory-list/supplier-inventory-list.component";
import { OrderComponent } from './order/order.component';
import { SupplierWiseInventoryListComponent } from './supplier-wise-inventory-list/supplier-wise-inventory-list.component';
import { ZoneComponent } from './zone/zone.component';
import { ZoneItemComponent } from './zone-item/zone-item.component';
import { VehicleTypeComponent } from './vehicle-type/vehicle-type.component';
import { VehicleTypeItemComponent } from './vehicle-type-item/vehicle-type-item.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleItemComponent } from './vehicle-item/vehicle-item.component';
import { OrderBookComponent } from './order-book/order-book.component';
import { DetailedOrderBookComponent } from './detailed-order-book/detailed-order-book.component';
import { DiscountConfigurationComponent } from './discount-configuration/discount-configuration.component';
import { DiscountConfigurationItemComponent } from './discount-configuration-item/discount-configuration-item.component';
import { CommissionConfigurationComponent } from './commission-configuration/commission-configuration.component';
import { CommissionConfigurationItemComponent } from './commission-configuration-item/commission-configuration-item.component';
import { WatchListComponent } from './watch-list/watch-list.component';
import { WaveManagementComponent } from './wave-management/wave-management.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full' },
  {path: 'home', component: DashboardComponent },
  {path: 'table', component: TableComponent },
  {path: 'standard_Inventory', component: StandardInventoryComponent },
  {path: 'AddStandard_Inventory', component: StandardInventoryItemComponent },
  {path: 'UserRegistration',component: UsersComponent },
  {path: 'User',component: UserComponent },
  {path: 'supplier', component: SupplierBaseManagementComponent },
  {path: 'supplierItem', component: SupplierBaseItemComponent },
  {path: 'delivery_slots', component: DeliverySlotsComponent },
  {path: 'add_slot', component: DeliverySlotItemComponent },
  {path: 'inventory_item_categories', component: InventoryItemCategoriesComponent },
  {path: 'add_inventory_item_category', component: IneventoryItemCategoryItemComponent },
  {path: 'unitOfMeasure', component: UnitOfMeasureComponent },
  {path: 'addunitOfMeasure', component: UnitOfMeasureItemComponent },
  {path: 'inventory_item_sub_category', component: InventoryItemSubCategoryComponent },
  {path: 'AddInventory_item_sub_category', component: InventoryItemSubCategoryItemComponent },
  {path: 'waves', component: TimeWavesComponent },
  {path: 'add_waves', component: TimeWavesItemComponent },
  {path: 'delivery_cost_configuration', component: DeliveryCostConfigurationComponent },
  {path: 'add_delivery_cost_configuration_item', component: DeliveryCostConfigurationItemComponent },
  { path:'supplier_inventory', component:SupplierInventoryComponent },
  { path:'supplier_inventory_item', component:SupplierInventoryItemComponent},
  { path:'supplier_inventory_view', component:SupplierInventoryListComponent},  
  {path: 'order', component: OrderComponent},
  {path: 'supplier_wise_inventory_list', component: SupplierWiseInventoryListComponent},
  {path: 'zone', component: ZoneComponent},
  {path:'zoneItem',component:ZoneItemComponent},
  {path: 'supplier_wise_inventory_list', component: SupplierWiseInventoryListComponent},
  {path: 'vehicleType', component: VehicleTypeComponent },
  {path: 'vehicleTypeItem', component: VehicleTypeItemComponent },
  {path: 'vehicle', component: VehicleComponent},
  {path: 'vehicle', component: VehicleItemComponent},
  {path:'orderBook',component:OrderBookComponent},
  {path:'detailedOrderBook',component:DetailedOrderBookComponent},
  {path:'discountConfiguration',component:DiscountConfigurationComponent},
  {path:'discountConfigurationItem',component:DiscountConfigurationItemComponent},
  {path:'commissionConfiguration',component:CommissionConfigurationComponent},
  {path: 'watchList', component: WatchListComponent },  
  {path:'commissionConfigurationItem',component:CommissionConfigurationItemComponent},
  {path:'wave_management',component:WaveManagementComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
