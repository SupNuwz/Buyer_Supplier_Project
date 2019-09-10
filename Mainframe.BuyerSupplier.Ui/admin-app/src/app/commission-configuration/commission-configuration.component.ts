import { Component, OnInit, ViewChild } from '@angular/core';
import {MatDialog,MatSort, MatTableDataSource, MatPaginator,MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { CommissionConfigurationDto } from './commissionConfigurationDto';
import { CommissionConfigurationService } from './commission-configuration.service';
import { CommissionConfigurationItemComponent } from '../commission-configuration-item/commission-configuration-item.component';

@Component({
  selector: 'app-commission-configuration',
  templateUrl: './commission-configuration.component.html',
  styleUrls: ['./commission-configuration.component.css']
})
export class CommissionConfigurationComponent implements OnInit {

  displayedColumns= ['id', 'name', 'fromDate', 'toDate','fromTime','toTime','rate','edit','delete'];
  commissionList :MatTableDataSource<CommissionConfigurationDto>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue: string;
  messageDto: any;
  
  applyFilter() {
   this.commissionList.filter = this.filterValue; 
  }

  constructor(private commission:CommissionConfigurationService ,public dialog: MatDialog,public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.commission.getCommission().subscribe(data=>
      {
       this.refreshGrid(data);
      });
  }
  refreshGrid(data:CommissionConfigurationDto[]){
    this.commissionList = new MatTableDataSource(data);
    this.commissionList.sort = this.sort;

    this.commissionList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
       return data[sortHeaderId].toLocaleLowerCase();
      }
     
      return data[sortHeaderId];
     };
     this.commissionList.paginator = this.paginator;
     this.applyFilter();
};

add(){
  const dialogRef = this.dialog.open(CommissionConfigurationItemComponent, {
   disableClose:true,
   autoFocus:true
  });
​
  dialogRef.afterClosed().subscribe(result => {
  if(result == true)
  {
    this.commission.getCommission().subscribe(data=>{this.refreshGrid(data);})
  }
  });
 }
 
 
 delete(item:CommissionConfigurationDto){
  this.messageDto = new MessageDialogDto();
      this.messageDto.messageCaption = "Commission Configuration Delete Confirmation";
      this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
      
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        disableClose:true,
        data : this.messageDto
      });
   
  dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.commission.delete(item.id).subscribe(
        data=>{
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex-1;
          }
          this.refreshGrid(data);
        });
      this.snackBar.open('Successfully deleted the Commission Configuration', '', {
        duration: 3000
      });

    }});
  }
  
  edit(item:CommissionConfigurationDto){
    const dialogRef = this.dialog.open(CommissionConfigurationItemComponent, { 
     disableClose:true,
     data: item
    });
  ​
    dialogRef.afterClosed().subscribe(result => {
     if(result == true)
     {
       this.commission.getCommission().subscribe(data=>{this.refreshGrid(data);})
     }
     });
   }

}
