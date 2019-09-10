import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { SupplierBaseDto } from './supplierBaseDto';
import {SupplierBaseManagementService} from './supplier-base-management.service';
import {MatDialog, MatPaginator, MatSort, MatTableDataSource,MatSnackBar} from '@angular/material';
import { SupplierBaseItemComponent } from '../supplierBaseItem/supplierBaseItem.component';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';


@Component({
  selector: 'app-supplierBaseManagement',
  templateUrl: './supplierBaseManagement.component.html',
  styleUrls: ['./supplierBaseManagement.component.css'],

})
export class SupplierBaseManagementComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  
  dataSource:MatTableDataSource<SupplierBaseDto>;
  filterValue:string;  
  displayedColumns = ['supplierBaseId', 'supplierBaseName','deliverySlot','edit','delete'];
  supplierBaseList:any; 
   messageDto: MessageDialogDto;


  constructor(private supplierBaseManagement: SupplierBaseManagementService ,
    public dialog: MatDialog,
    public snackBar: MatSnackBar){ }

  ngOnInit() {
    let so:SupplierBaseDto=new SupplierBaseDto();
    this.supplierBaseManagement.getSuppliers().subscribe(data=>    
    {
      this.refreshGrid(data);
    });
  }
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.dataSource.filter = filtetString;
  }

  refreshGrid(data:SupplierBaseDto[]){
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
    const dialogRef = this.dialog.open(SupplierBaseItemComponent, {
      disableClose:true,
      autoFocus:true
    });    
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.supplierBaseManagement.getSuppliers().subscribe(data=>{
          this.refreshGrid(data);
        })
      }
    });
  }
  
  delete(item:SupplierBaseDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Supplier Base Delete Confirmation";
        this.messageDto.messageBody = "This " + item.supplierBaseName + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
        
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.supplierBaseManagement.delete(item.supplierBaseId).subscribe(data=>{
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex-1;
          }
          this.refreshGrid(data);
          
          this.snackBar.open('Successfully deleted the supplier base', '', {
            duration: 3000
          });
      })
    }
      });
  }

  update(item:SupplierBaseDto){
    const dialogRef = this.dialog.open(SupplierBaseItemComponent, { 
      data:item,
      disableClose:true,
      autoFocus:true  
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.supplierBaseManagement.getSuppliers().subscribe(data=>{this.refreshGrid(data);})
      }
      });

   }  
  
}

