import { Component, OnInit, ViewChild } from '@angular/core';
import { DeliverySlotsDto } from './DeliverySlotsDto';
import { DeliverySlotsService } from './delivery-slots.service';
import { MatDialog, MatSort, MatTableDataSource, MatPaginator, MatSnackBar } from '@angular/material';
import { DeliverySlotItemComponent } from '../delivery-slot-item/delivery-slot-item.component';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-delivery-slots',
  templateUrl: './delivery-slots.component.html',
  styleUrls: ['./delivery-slots.component.css']
})
export class DeliverySlotsComponent implements OnInit {
  displayedColumns = ['id', 'slotName','cutoffTime', 'startTime','countdownTime',
  'orderAcceptTime','orderCofirmTime','dispatchesConfirmTime','endTime','edit','delete'];
  deliverySlotsList :MatTableDataSource<DeliverySlotsDto>;
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue:string; 
  messageDto: MessageDialogDto;

  constructor(private deliverySlots:DeliverySlotsService, public dialog: MatDialog,
     public snackBar: MatSnackBar) { }

  ngOnInit(){
    let d:DeliverySlotsDto = new DeliverySlotsDto();
    this.deliverySlots.getDeliverySlot().subscribe(data => {
      this.refreshGrid(data);
    });
  }
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.deliverySlotsList.filter = filtetString;
  }

  refreshGrid(data:DeliverySlotsDto[]){
      this.deliverySlotsList = new MatTableDataSource(data);
    this.deliverySlotsList.sort = this.sort;
      this.deliverySlotsList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
        if (typeof data[sortHeaderId] === 'string') {
          return data[sortHeaderId].toLocaleLowerCase();
        }
      
        return data[sortHeaderId];
      };
      this.deliverySlotsList.paginator = this.paginator;
    this.applyFilter();
  }

  add(){
    const dialogRef = this.dialog.open(DeliverySlotItemComponent, {
      disableClose:true,
      autoFocus:true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.deliverySlots.getDeliverySlot().subscribe(data => {this.refreshGrid(data);})
      }
    });
  }

  delete(item:DeliverySlotsDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Delivery Slots Delete Confirmation";
        this.messageDto.messageBody = "This " + item.slotName + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
    
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.deliverySlots.deleteDeliverySlot(item.id).subscribe(
          data=>{
            var maxPageIndex =  data.length / this.paginator.pageSize; 
            if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
              this.paginator.pageIndex = maxPageIndex-1;
            }
            this.refreshGrid(data);
          }
        );  
        
        this.snackBar.open('Successfully deleted the delivery slot', '', {
          duration: 3000
        });
      }
      });
  }

  edit(item:DeliverySlotsDto){
    const dialogRef = this.dialog.open(DeliverySlotItemComponent, {  
      disableClose:true,
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.deliverySlots.getDeliverySlot().subscribe(data=>{this.refreshGrid(data);})
      }
      });
  }

}
