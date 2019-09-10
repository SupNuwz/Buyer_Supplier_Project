import { Component, OnInit, ViewChild } from '@angular/core';
import { StandardInventoryDto } from './StandardInventoryDto';
import { StandardInventoryService } from './standard-inventory.service';
import {StandardInventoryItemComponent} from '../standard-inventory-item/standard-inventory-item.component'
import {MatDialog,MatSort, MatTableDataSource, MatPaginator,MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
​
@Component({
 selector: 'app-standard-inventory',
 templateUrl: './standard-inventory.component.html',
 styleUrls: ['./standard-inventory.component.css']
})
export class StandardInventoryComponent implements OnInit {
​
 displayedColumns= ['id', 'itemName', 'inventoryItemCategoryId', 'inventoryItemSubCategoryId','quantityUnitOfMesureId','seasonality','minimumInventory', 'edit','delete'];
 standardInventoryList :MatTableDataSource<StandardInventoryDto>;
 
 @ViewChild(MatSort) sort: MatSort;
 @ViewChild(MatPaginator) paginator: MatPaginator;
 filterValue: string;
 messageDto: any;
 
 applyFilter() {
  this.standardInventoryList.filter = this.filterValue; 
 }
 constructor( private standardInventory:StandardInventoryService, public dialog: MatDialog,public snackBar: MatSnackBar) { }
​
 ngOnInit() {

  this.standardInventory.getStandardInventory().subscribe(data=>
   {
    this.refreshGrid(data);
   });
  }
​
  refreshGrid(data:StandardInventoryDto[]){
   this.standardInventoryList = new MatTableDataSource(data);
   this.standardInventoryList.sort = this.sort;
    
    this.standardInventoryList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
     if (typeof data[sortHeaderId] === 'string') {
      return data[sortHeaderId].toLocaleLowerCase();
     }
    
     return data[sortHeaderId];
    };
    this.standardInventoryList.paginator = this.paginator;
    this.applyFilter();
   };  
 
 add(){
  const dialogRef = this.dialog.open(StandardInventoryItemComponent, {
   disableClose:true,
   autoFocus:true
  });
​
  dialogRef.afterClosed().subscribe(result => {
  if(result == true)
  {
    this.standardInventory.getStandardInventory().subscribe(data=>{this.refreshGrid(data);})
  }
  });
 }
​
 delete(item:StandardInventoryDto){
  this.messageDto = new MessageDialogDto();
    this.messageDto.messageCaption = "Standard Inventory Delete Confirmation";
    this.messageDto.messageBody = "This " + item.itemName + " may be used in other transactions. Do you still want to delete it?";
    
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
     disableClose:true,
     data : this.messageDto
    });
  
  dialogRef.afterClosed().subscribe(result => {
   if(result == true)
   {
    this.standardInventory.delete(item.id).subscribe(
     data=>{var maxPageIndex = data.length / this.paginator.pageSize; 
      if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
       this.paginator.pageIndex = maxPageIndex-1;
      }     
      this.refreshGrid(data);
     }
    );
    this.snackBar.open('Successfully deleted the standard inventory item', '', {
     duration: 3000
    }); 
   }
   });
 }
 
 edit(item:StandardInventoryDto){
  const dialogRef = this.dialog.open(StandardInventoryItemComponent, { 
   disableClose:true,
   data: item
  });
​
  dialogRef.afterClosed().subscribe(result => {
   if(result == true)
   {
     this.standardInventory.getStandardInventory().subscribe(data=>{this.refreshGrid(data);})
   }
   });
 }
 }
​

