import { Component, OnInit,ViewChild } from '@angular/core';
import { TableDataSource } from '../table/table-datasource';
import { InventoryItemSubCategoryDto } from './InventoryItemSubCategoryDto';
import { InventoryItemSubCategoryItemComponent } from '../inventory-item-sub-category-item/inventory-item-sub-category-item.component';
import {InventoryItemSubCategoryService} from './inventory-item-sub-category.service';
import {MatDialog, MatPaginator, MatSort, MatTableDataSource, MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-inventory-item-sub-category',
  templateUrl: './inventory-item-sub-category.component.html',
  styleUrls: ['./inventory-item-sub-category.component.css']
})
export class InventoryItemSubCategoryComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource:any;
  
displayedColumns: string[] = ['id','inventoryItemCategoryID','name', 'description','edit','delete'];
InventoryItemSubComponentList; 
messageDto: MessageDialogDto;
filterValue:string; 



  constructor(private inventoryItemSubCategory: InventoryItemSubCategoryService ,public dialog: MatDialog,
     public snackBar: MatSnackBar){ }

  ngOnInit() {

    let so:InventoryItemSubCategoryDto=new InventoryItemSubCategoryDto();
    this.inventoryItemSubCategory.getNewCategory().subscribe(data=>
    
      {
        this.refreshGrid(data);
        this.dataSource = new MatTableDataSource(data);

        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        
        this.dataSource.paginator = this.paginator;
      }
      );
  }
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.dataSource.filter = filtetString;
  }

  refreshGrid(data:InventoryItemSubCategoryDto[]){
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.sort = this.sort;
    this.dataSource.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
        return data[sortHeaderId].toLocaleLowerCase();
      }
    
      return data[sortHeaderId];
    };
    this.dataSource.paginator = this.paginator;
    
    this.applyFilter();
  }

  add(){

    const dialogRef = this.dialog.open(InventoryItemSubCategoryItemComponent, {
      disableClose:true,
      autoFocus:true
    });    


  dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.inventoryItemSubCategory.getNewCategory().subscribe(data=>{
        this.refreshGrid(data);
      })
      
    }
      });
  }
  
  delete(item:InventoryItemSubCategoryDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Inventory Item Sub Category Delete Confirmation";
        this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
    
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.inventoryItemSubCategory.delete(item.id).subscribe(
          data=>{
            var maxPageIndex =  data.length / this.paginator.pageSize; 
            if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
              this.paginator.pageIndex = maxPageIndex-1;
            }
            this.refreshGrid(data);
          });
        this.snackBar.open('Successfully deleted the inventory item sub category', '', {
          duration: 2000
        });

      }});
  }
   update(item:InventoryItemSubCategoryDto){
    const dialogRef = this.dialog.open(InventoryItemSubCategoryItemComponent, { 
      data:item,
      disableClose:true,
      autoFocus:true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.inventoryItemSubCategory.getNewCategory().subscribe(data=>{this.refreshGrid(data);})
      }
      });
  }  
}
