import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MAT_DIALOG_DATA } from '@angular/material';
import { SupplierInventoryDto } from "../supplier-inventory/SupplierInventoryDto";
import { SupplierStandardInventoryDto } from '../user/SupplierStandardInventoryDto';

@Component({
  selector: 'app-supplier-inventory-list',
  templateUrl: './supplier-inventory-list.component.html',
  styleUrls: ['./supplier-inventory-list.component.css']
})
export class SupplierInventoryListComponent implements OnInit {

  displayedColumns: string[] = ['unitPrice', 'qty', 'availableQty' ];
  supplierInventoryList :any;
  supplierInventoryDataList :SupplierInventoryDto[];
  supplierStandardInventoryId:number;
  heading:string;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: SupplierStandardInventoryDto) { 

      if(data){
        this.heading = "Available Inventories of " + data.inventoryItemName;
        this.supplierInventoryDataList = data.supplierInventories;
      }
    }

  ngOnInit() {    
   this.loadData();
  }

  loadData(){
    
          this.supplierInventoryList = new MatTableDataSource(this.supplierInventoryDataList);
          this.supplierInventoryList.sort = this.sort;
          this.supplierInventoryList.paginator = this.paginator;        
      }
}
