import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatGridListModule,
          MatCardModule, MatProgressSpinnerModule, MatMenuModule, MatTableModule, 
          MatSortModule, MatInputModule, MatExpansionModule, 
          MatDatepickerModule, MatNativeDateModule, MatStepperModule, MatSlideToggleModule } from '@angular/material';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TableComponent } from './table/table.component';
import { MatListModule} from '@angular/material/list';
import { MatTabsModule} from '@angular/material/tabs';
import { MatBottomSheetModule} from '@angular/material/bottom-sheet';
import { TableContentComponent } from './table-content/table-content.component';
import { ScrollDispatchModule} from '@angular/cdk/scrolling';
import { StandardInventoryComponent } from './standard-inventory/standard-inventory.component';
import { HttpClientModule }    from '@angular/common/http';
import { StandardInventoryItemComponent } from './standard-inventory-item/standard-inventory-item.component';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatRadioModule} from '@angular/material/radio';
import { MatSelectModule} from '@angular/material/select';
import { MatDialogModule} from '@angular/material/dialog';
import { MatSnackBarModule} from '@angular/material/snack-bar';
import { SupplierBaseManagementComponent} from './supplierBaseManagement/supplierBaseManagement.component';
import { SupplierBaseItemComponent } from './supplierBaseItem/supplierBaseItem.component';
import { UsersComponent } from './Users-List/users-list.component';
import { UserComponent} from './user/user.component'
import { MatPaginatorModule} from '@angular/material/paginator';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { DeliverySlotsComponent } from './delivery-slots/delivery-slots.component';
import { DeliverySlotItemComponent } from './delivery-slot-item/delivery-slot-item.component';
import { UnitOfMeasureComponent } from './unit-of-measure/unit-of-measure.component';
import { UnitOfMeasureItemComponent } from './unit-of-measure-item/unit-of-measure-item.component';
import { InventoryItemSubCategoryItemComponent } from './inventory-item-sub-category-item/inventory-item-sub-category-item.component';
import { InventoryItemSubCategoryComponent } from './inventory-item-sub-category/inventory-item-sub-category.component';
import { InventoryItemCategoriesComponent } from './inventory-item-categories/inventory-item-categories.component';
import { IneventoryItemCategoryItemComponent } from './ineventory-item-category-item/ineventory-item-category-item.component';
import { TimeWavesComponent } from './time-waves/time-waves.component';
import { TimeWavesItemComponent } from './time-waves-item/time-waves-item.component';
import { DeliveryCostConfigurationComponent } from './delivery-cost-configuration/delivery-cost-configuration.component';
import { DeliveryCostConfigurationItemComponent } from './delivery-cost-configuration-item/delivery-cost-configuration-item.component';
import { SupplierInventoryComponent } from './supplier-inventory/supplier-inventory.component';
import { SupplierInventoryItemComponent } from './supplier-inventory-item/supplier-inventory-item.component';
import { SupplierInventoryListComponent } from './supplier-inventory-list/supplier-inventory-list.component';
import { OrderComponent } from './order/order.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { SupplierWiseInventoryListComponent } from './supplier-wise-inventory-list/supplier-wise-inventory-list.component';
import { CartItemComponent } from './order/cart-item/cart-item.component';
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
import { OrderPossibilityItemComponent } from './order/order-possibility-item/order-possibility-item.component';
import { WatchListComponent } from './watch-list/watch-list.component';
import { WaveManagementComponent } from './wave-management/wave-management.component';


@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    DashboardComponent,
    TableComponent,
    TableContentComponent,
    StandardInventoryComponent,
    StandardInventoryItemComponent,
    UsersComponent,
    UserComponent,
    SupplierBaseManagementComponent,
    SupplierBaseItemComponent,
    DeliverySlotsComponent,
    DeliverySlotItemComponent,
    UnitOfMeasureComponent,
    UnitOfMeasureItemComponent,
    DeliverySlotItemComponent,
    InventoryItemSubCategoryItemComponent,
    InventoryItemSubCategoryComponent,
    InventoryItemCategoriesComponent,
    IneventoryItemCategoryItemComponent,
    TimeWavesComponent,
    TimeWavesItemComponent,
    DeliveryCostConfigurationComponent,
    DeliveryCostConfigurationItemComponent,
    IneventoryItemCategoryItemComponent, 
    InventoryItemSubCategoryComponent,
    SupplierInventoryComponent,
    SupplierInventoryItemComponent,
    SupplierInventoryListComponent,
    TimeWavesItemComponent,
    OrderComponent,
	  SupplierWiseInventoryListComponent, 
    ConfirmationDialogComponent , 
    CartItemComponent, 
    ZoneComponent, 
    ZoneItemComponent,  
    VehicleTypeComponent, 
    VehicleTypeItemComponent, 
    VehicleComponent, 
    VehicleItemComponent, 
    OrderBookComponent, 
    DetailedOrderBookComponent, 
    DiscountConfigurationComponent, 
    DiscountConfigurationItemComponent, 
    CommissionConfigurationComponent, 
    CommissionConfigurationItemComponent, 
    WaveManagementComponent,
    OrderPossibilityItemComponent, 
    WatchListComponent],  

	  imports: [
	    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatTableModule,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
    MatTabsModule,
    MatBottomSheetModule,
    ScrollDispatchModule,
    HttpClientModule,
    MatFormFieldModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatSelectModule,
    MatDialogModule,
    MatSnackBarModule,
	  MatCheckboxModule,
    MatExpansionModule,    
    MatDatepickerModule,  
    MatNativeDateModule,
    FormsModule, 
    MatStepperModule,
    ReactiveFormsModule   ,
    MatSlideToggleModule
  ],

  entryComponents:[
    ConfirmationDialogComponent,
    DetailedOrderBookComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
