import { Component, OnInit,ViewChild } from '@angular/core';
import { DeliveryCostConfigurationDto } from './DeliveryCostConfigurationDto';
import { DeliveryCostConfigurationItemComponent } from '../delivery-cost-configuration-item/delivery-cost-configuration-item.component';
import { DeliveryCostConfigurationService } from './delivery-cost-configuration.service';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource, MatSnackBar } from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-delivery-cost-configuration',
  templateUrl: './delivery-cost-configuration.component.html',
  styleUrls: ['./delivery-cost-configuration.component.css']
})
export class DeliveryCostConfigurationComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;  
  displayedColumns = ['id', 'name', 'description', 'baseLocationID', 'baseFare', 'baseDistance', 'additionalRate','edit','delete'];
  deliveryCostConfigurationList:MatTableDataSource<DeliveryCostConfigurationDto>; 
  filterValue:string;
  messageDto: MessageDialogDto;

  constructor(private deliveryCostConfiguration: DeliveryCostConfigurationService ,
    public dialog: MatDialog, public snackBar: MatSnackBar){ }

  ngOnInit() {

    let so:DeliveryCostConfigurationDto=new DeliveryCostConfigurationDto();
    this.deliveryCostConfiguration.getDeliveryCostConfigurations().subscribe(data=>{this.refreshGrid(data);});
  }

  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.deliveryCostConfigurationList.filter = filtetString;
  }
    
  refreshGrid(data:DeliveryCostConfigurationDto[]){
        this.deliveryCostConfigurationList = new MatTableDataSource(data);
        this.deliveryCostConfigurationList.sort = this.sort;
        this.deliveryCostConfigurationList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        this.deliveryCostConfigurationList.paginator = this.paginator;
    this.applyFilter();
      }

  add(){

    const dialogRef = this.dialog.open(DeliveryCostConfigurationItemComponent, {
      disableClose:true,
      autoFocus:true
    });    


    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.deliveryCostConfiguration.getDeliveryCostConfigurations()
        .subscribe(data=>{this.refreshGrid(data);})
      }
    });
  }
  
  delete(item:DeliveryCostConfigurationDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Delivery Cost Configuration Delete Confirmation";
        this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
    
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.deliveryCostConfiguration.delete(item.id)
        .subscribe(data=>{
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex - 1;
          }
          this.refreshGrid(data);
        });  
        this.snackBar.open('Successfully deleted the delivery cost configuration', '', {
          duration: 2000
        });
      }
    });
  }

  update(item:DeliveryCostConfigurationDto){
    const dialogRef = this.dialog.open(DeliveryCostConfigurationItemComponent, { 
      data:item,
      disableClose:true,
      autoFocus:true
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.deliveryCostConfiguration.getDeliveryCostConfigurations()
         .subscribe(data=>{this.refreshGrid(data);})
      }
    });
  }  
}

