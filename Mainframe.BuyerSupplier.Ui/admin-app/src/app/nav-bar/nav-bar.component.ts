import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NavBarDto } from './nav-barDto';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

    navBarItems:NavBarDto[] = [
      {routerLink:'/supplier_inventory',title:'list',name:'Supplier Inventory',isMenuItem :false, childs:[]},
      {routerLink:'/order',title:'assignment',name:'Order',isMenuItem :false, childs:[]},
      {routerLink:'/UserRegistration',title:'account_box',name:'Users',isMenuItem :false, childs:[]},
      {routerLink:'/orderBook',title:'assignment',name:'Order Book',isMenuItem :false, childs:[]},
      {routerLink:'/wave_management',title:'access_alarm',name:'Waves Process Handeling',isMenuItem :false, childs:[]},
      {routerLink:'#',title:'settings',name:'Configurations',isMenuItem :true, 
       childs:[{routerLink:'/standard_Inventory',title:'list_alt',name:'Standard Inventory',isMenuItem :false, childs:[]},
               {routerLink:'/supplier',title:'location_city',name:'Supplier Base',isMenuItem :false, childs:[]},
               {routerLink:'/delivery_slots',title:'update',name:'Delivery Slots',isMenuItem :false, childs:[]},
               {routerLink:'/unitOfMeasure',title:'receipt',name:'Unit Of Measure',isMenuItem :false, childs:[]},
               {routerLink:'/inventory_item_categories',title:'ballot',name:'Inventory Item Categories',isMenuItem :false, childs:[]},
               {routerLink:'/inventory_item_sub_category',title:'subject',name:'Inventory Item Sub Category',isMenuItem :false, childs:[]},
               {routerLink:'/waves',title:'show_chart',name:'Time Waves',isMenuItem :false, childs:[]},
               {routerLink:'/discountConfiguration',title:'playlist_add_check',name:'Discount Configuration',isMenuItem :false, childs:[]},
               {routerLink:'/commissionConfiguration',title:'add_circle_outline',name:'Commission Configuration',isMenuItem :false, childs:[]},
               {routerLink:'/delivery_cost_configuration',title:'local_shipping',name:'Delivery Cost Configuration',isMenuItem :false, childs:[]},
               {routerLink:'/supplier_wise_inventory_list',title:'recent_actors',name:'Supplier Wise Inventory List View',isMenuItem :false, childs:[]},
               {routerLink:'/zone',title:'location_on',name:'Zone',isMenuItem :false, childs:[]},
               {routerLink:'/vehicleType',title:'drive_eta',name:'Vehicle Type',isMenuItem :false, childs:[]},
               {routerLink:'/vehicle',title:'directions_car',name:'Vehicle',isMenuItem :false, childs:[]},
               {routerLink:'/watchList',title:'filter_none',name:'Watch List',isMenuItem :false, childs:[]},
              ]},
    ];

  constructor(private breakpointObserver: BreakpointObserver) {}

  current:any;

}
