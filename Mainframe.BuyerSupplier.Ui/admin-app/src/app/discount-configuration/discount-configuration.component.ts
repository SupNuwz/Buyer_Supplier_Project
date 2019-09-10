import { Component, OnInit, ViewChild } from '@angular/core';
import { DiscountConfigurationDto } from './discountConfigurationDto';
import { DiscountConfigurationService } from './discount-configuration.service';
import {MatDialog,MatSort, MatTableDataSource, MatPaginator,MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { DiscountConfigurationItemComponent } from '../discount-configuration-item/discount-configuration-item.component';


@Component({
  selector: 'app-discount-configuration',
  templateUrl: './discount-configuration.component.html',
  styleUrls: ['./discount-configuration.component.css']
})
export class DiscountConfigurationComponent implements OnInit {
  displayedColumns= ['id', 'name', 'fromDate', 'toDate','fromTime','toTime','rate','edit','delete'];
  discountList :MatTableDataSource<DiscountConfigurationDto>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue: string;
  messageDto: any;
  
  applyFilter() {
   this.discountList.filter = this.filterValue; 
  }

  constructor(private discount:DiscountConfigurationService ,public dialog: MatDialog,public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.discount.getDiscount().subscribe(data=>
      {
       this.refreshGrid(data);
      });
  }
  refreshGrid(data:DiscountConfigurationDto[]){
    this.discountList = new MatTableDataSource(data);
    this.discountList.sort = this.sort;

    this.discountList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
       return data[sortHeaderId].toLocaleLowerCase();
      }
     
      return data[sortHeaderId];
     };
     this.discountList.paginator = this.paginator;
     this.applyFilter();
};

  add(){
  const dialogRef = this.dialog.open(DiscountConfigurationItemComponent, {
   disableClose:true,
   autoFocus:true
  });
​
  dialogRef.afterClosed().subscribe(result => {
  if(result == true)
  {
    this.discount.getDiscount().subscribe(data=>{this.refreshGrid(data);})
  }
  });
 } 

 delete(item:DiscountConfigurationDto){
  this.messageDto = new MessageDialogDto();
      this.messageDto.messageCaption = "Discount Configuration Delete Confirmation";
      this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
      
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        disableClose:true,
        data : this.messageDto
      });
   
  dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.discount.delete(item.id).subscribe(
        data=>{
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex-1;
          }
          this.refreshGrid(data);
        });
      this.snackBar.open('Successfully deleted the Discount Configuration', '', {
        duration: 3000
      });

    }});
  }
  
  edit(item:DiscountConfigurationDto){
    const dialogRef = this.dialog.open(DiscountConfigurationItemComponent, { 
     disableClose:true,
     data: item
    });
  ​
    dialogRef.afterClosed().subscribe(result => {
     if(result == true)
     {
       this.discount.getDiscount().subscribe(data=>{this.refreshGrid(data);})
     }
     });
   }
}
