import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { OrderDto } from '../order/OrderDto ';
import { DeliverySlotsDto } from '../delivery-slots/DeliverySlotsDto';
import { OrderService } from '../order/order.service';
import { StandardInventoryService } from '../standard-inventory/standard-inventory.service';
import { DeliverySlotsService } from '../delivery-slots/delivery-slots.service';
import { StandardInventoryDto } from '../standard-inventory/StandardInventoryDto';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { OrderDetailDto } from '../order/OrderDetailDto';

@Component({
  selector: 'app-detailed-order-book',
  templateUrl: './detailed-order-book.component.html',
  styleUrls: ['./detailed-order-book.component.css']
})
export class DetailedOrderBookComponent implements OnInit {
  detailedorderbook:FormGroup;  
  orderBook:OrderDto;
  deliverySlot:DeliverySlotsDto;
  standardInventoryItems: StandardInventoryDto[];
  deliverySlotList: DeliverySlotsDto[];

  detailedOrderList;
  displayedColumns: string[] = ['itemId', 'qty'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;


  constructor(private formBuilder:FormBuilder,private orderService:OrderService,
              private standardInventoryService:StandardInventoryService,
              private deliverySlotsService: DeliverySlotsService, 
              public dialogRef: MatDialogRef<DetailedOrderBookComponent>,
                @Inject(MAT_DIALOG_DATA) public data: OrderDto)

             { 
               this.orderBook = data;
             
               }

  ngOnInit() {
    this.detailedOrderList = new MatTableDataSource( this.orderBook.orderDetails);
    this.detailedOrderList.sort = this.sort;
      this.detailedOrderList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
        if (typeof data[sortHeaderId] === 'string') {
          return data[sortHeaderId].toLocaleLowerCase();
        }
        return data[sortHeaderId];
      };
      this.detailedOrderList.paginator = this.paginator;
    this.loadInitialData();
}

loadInitialData(){
  this.standardInventoryService.getStandardInventory().subscribe(data=>{
    this.standardInventoryItems = data;
  });
  this.deliverySlotsService.getDeliverySlot().subscribe(data=>{
    this.deliverySlotList = data;
  });
}



}
