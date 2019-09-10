import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { SupplierStandardInventoryDto } from "../user/SupplierStandardInventoryDto";
import { SupplierStandardInventoryService } from '../user/supplier-standard-inventory.service';
import { SupplierInventoryItemComponent } from '../supplier-inventory-item/supplier-inventory-item.component';
import { SupplierInventoryListComponent } from "../supplier-inventory-list/supplier-inventory-list.component";
import { SupplierInventoryService } from "../supplier-inventory/supplier-inventory.service";

@Component({
  selector: 'app-supplier-inventory',
  templateUrl: './supplier-inventory.component.html',
  styleUrls: ['./supplier-inventory.component.css']
})
export class SupplierInventoryComponent implements OnInit {

  displayedColumns: string[] = ['inventoryItemName', 'itemGroup', 'add', 'view'];
  supplierStandardInventoryList :any;
  arraylength:number;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  applyFilter(filterValue: string) {
    this.supplierStandardInventoryList.filter = filterValue.trim().toLowerCase();
  }

  constructor(private supplierStandardInventory:SupplierStandardInventoryService, 
    private supplierInventoryService:SupplierInventoryService,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(){
    this.supplierStandardInventory.getSupplierStandardInventoryBySupplier(1500)
      .subscribe(data=>
        {
          this.supplierStandardInventoryList = new MatTableDataSource(data);
          this.supplierStandardInventoryList.sort = this.sort;
          this.supplierStandardInventoryList.paginator = this.paginator;
        });
      }
  
  add(supplierStandardInventoryDto:SupplierStandardInventoryDto){
    const dialogRef = this.dialog.open(SupplierInventoryItemComponent, {
      data: supplierStandardInventoryDto,
      disableClose:true,
      autoFocus:true
    });
  }

  view(supplierStandardInventoryDto:SupplierStandardInventoryDto){

    this.supplierInventoryService.getSupplierInventories(supplierStandardInventoryDto.id)
    .subscribe(result => 
        {            
            this.arraylength = result.length;

            if (this.arraylength > 0) {  
              
              supplierStandardInventoryDto.supplierInventories=result;

              this.dialog.open(SupplierInventoryListComponent, {
                data: supplierStandardInventoryDto
              });
            }

        }
      );    
  }

}
