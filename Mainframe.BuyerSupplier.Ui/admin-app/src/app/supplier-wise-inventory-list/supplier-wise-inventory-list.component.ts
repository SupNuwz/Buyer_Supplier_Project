import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { UserService } from "../Users-List/user.service";
import { SupplierInventoryService } from '../supplier-inventory/supplier-inventory.service';
import { SupplierInventoryDto } from '../supplier-inventory/SupplierInventoryDto';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Observable, of } from 'rxjs';


@Component({
  selector: 'app-supplier-wise-inventory-list',
  templateUrl: './supplier-wise-inventory-list.component.html',
  styleUrls: ['./supplier-wise-inventory-list.component.css'],
animations: [
  trigger('detailExpand', [
    state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'collapse' })),
    state('expanded', style({ height: '*', visibility: 'visible' })),
    transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
  ]),
]
})

export class SupplierWiseInventoryListComponent implements OnInit {

  displayedColumns: string[] = ["supplier",'itemName', 'qty', 'unitPrice'];
  dataSource = new MatTableDataSource();

  supplierInventories:any[];
  suppliersList = new MatTableDataSource();
  group="";
  itemGroup="";
  constructor(private supplierInventoryService:SupplierInventoryService
  ) { 

  }

  ngOnInit() {
     this.supplierInventoryService.getAllSupplierInventories().subscribe(data=>
      {
        this.supplierInventories = [];

        let supplier ="";
   

        data.forEach(element => {

          if(supplier != element.supplierName)
          {
               this.supplierInventories.push({ detailRow: false, element });    
               supplier = element.supplierName;             
          }

          this.supplierInventories.push({ detailRow: true, element });
        });

        this.suppliersList= new MatTableDataSource(this.supplierInventories);
      });            
  } 

  isExpansionDetailRow(nmber, row):boolean
  { 
    return row.detailRow;
  }
  expandedElement: any;

  isGroup(index, item): boolean{

   return !item.detailRow;
  }

}


 