import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { InventoryItemCategoriesDto } from './InventoryItemCategoriesDto';
import { InventoryItemCategoriesService } from './inventory-item-categories.service';
import { MatDialog, MatSort, MatTableDataSource, MatPaginator, MatSnackBar } from '@angular/material';
import { IneventoryItemCategoryItemComponent } from '../ineventory-item-category-item/ineventory-item-category-item.component';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-inventory-item-categories',
  templateUrl: './inventory-item-categories.component.html',
  styleUrls: ['./inventory-item-categories.component.css']
})
export class InventoryItemCategoriesComponent implements OnInit {
  displayedColumns=['id', 'name', 'description', 'edit', 'delete'];
  inventoryItemCategoryList:MatTableDataSource<InventoryItemCategoriesDto>;
  messageDto: any;


  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue:string;  

  constructor(private inventoryItemCategories:InventoryItemCategoriesService,
     public dialog: MatDialog, public snackBar: MatSnackBar) { }

  ngOnInit(){
    let d:InventoryItemCategoriesDto = new InventoryItemCategoriesDto();

    this.inventoryItemCategories.getInventoryItemCategory().subscribe(data =>
      {
        this.refreshGrid(data);
      });
    }
    
    applyFilter(){
      let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.inventoryItemCategoryList.filter = filtetString;
    }
  
    refreshGrid(data:InventoryItemCategoriesDto[]){
        this.inventoryItemCategoryList = new MatTableDataSource(data);
      this.inventoryItemCategoryList.sort = this.sort;

        this.inventoryItemCategoryList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        this.inventoryItemCategoryList.paginator = this.paginator;
      this.applyFilter();
    }
  
    add(){
      const dialogRef = this.dialog.open(IneventoryItemCategoryItemComponent, {
        disableClose:true,
        autoFocus:true
      });
  
      dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.inventoryItemCategories.getInventoryItemCategory().subscribe(data=>{this.refreshGrid(data);})
      }
      });
    }
  
    delete(category:InventoryItemCategoriesDto){
      this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Inventory Item Category Delete Confirmation";
        this.messageDto.messageBody = "This " + category.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
      
      dialogRef.afterClosed().subscribe(result => {
        if(result == true)
        {
          this.inventoryItemCategories.deleteInventoryItemCategory(category.id).subscribe(
            data=>{
              var maxPageIndex =  data.length / this.paginator.pageSize; 
              if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
                this.paginator.pageIndex = maxPageIndex-1;
              }
              this.refreshGrid(data);
            });  
          this.snackBar.open('Successfully deleted the inventory item category', '', {
            duration: 2000
          });
        }
        });
    }
    
    edit(category:InventoryItemCategoriesDto){
      const dialogRef = this.dialog.open(IneventoryItemCategoryItemComponent, {  
        disableClose:true,
        data: category
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if(result == true)
        {
           this.inventoryItemCategories.getInventoryItemCategory().subscribe(data=>{this.refreshGrid(data);})
        }
        });
    }
    }