import { Component, OnInit, ViewChild } from '@angular/core';
import {FormBuilder, FormGroup, Validators, FormArray} from '@angular/forms'
import { MatDatepickerInputEvent, MatDialog, MatSnackBar, MatTableDataSource, MatSort, MatPaginator, MatSelectChange } from '@angular/material';

import { OrderDto } from './OrderDto ';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { DeliverySlotsService } from '../delivery-slots/delivery-slots.service';
import { StandardInventoryService } from '../standard-inventory/standard-inventory.service';
import { DeliverySlotsDto } from '../delivery-slots/DeliverySlotsDto';
import { StandardInventoryDto } from '../standard-inventory/StandardInventoryDto';
import { OrderDetailDto } from './OrderDetailDto';
import { OrderDetailsArrayLengthValidation } from './OrderValidator';
import { OrderService } from './order.service';
import { OrderOptimizedPossibilityDto } from './OrderOptimizedPossibilityDto';
import { OrderAssignmentDto } from './OrderAssignmentDto';
import { OrderPossibilitySelectionDto } from './OrderPossibilitySelectionDto';
import { OrderPossibilitySelectionListDto } from './OrderPossibilitySelectionListDto';
import { timestamp } from 'rxjs/operators';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  isLinear = true;

  orderHeaderGroup: FormGroup;
  itemSelectionGroup: FormGroup;

  order:OrderDto;  
  minDate= new Date();
  messageDto: MessageDialogDto;
  standardInventoryItems: StandardInventoryDto[];
  deliverySlotList: DeliverySlotsDto[];
  orderOptimizedPossibilities:OrderOptimizedPossibilityDto[];
  orderAssignment:OrderAssignmentDto[];
  categories: string[] = ['Pola', 'Farm Direct', 'Organic'];
  breakpoint: number; 
  orderDetails: OrderDetailDto[];
  orderDetailsData:any;
  displayedColumns: string[] = ['itemName', 'qty'];
  validation_messages = {
    'expectedDiliveredDate': [
      { type: 'required', message: 'Expected Delivery Date is required' }
    ],
    'deliverySlot': [
      { type: 'required', message: 'Delivery Slot is required' }
    ],
    'supplierCategory': [
      { type: 'required', message: 'Supplier Category is required' }
    ],
    'qty': [
      { type: 'required', message: 'Quantity is required' }
    ],
    'orderAssignmentOption':[
      { type: 'required', message: 'Order Assignment Option is required for Pre-Orders' }
    ]
  };
  
  deliverySlot:DeliverySlotsDto;
  
  constructor(private formBuilder: FormBuilder, 
    private standardInventoryService:StandardInventoryService,
    private deliverySlotsService: DeliverySlotsService, 
    private orderService:OrderService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  ngOnInit() {
    
    this.breakpoint = (window.innerWidth <= 400) ? 1 : 6;

    this.order = new OrderDto();
    this.orderDetails = new Array();

    this.orderHeaderGroup = this.formBuilder.group({
      id:[this.order.id],
        orderRefNo:[this.order.orderRefNo],
        buyerId:[this.order.buyerId],
        orderedDate:[this.order.orderedDate],
        expectedDiliveredDate:[this.order.expectedDiliveredDate,
          [Validators.required]],
        orderType:[this.order.orderType],
        status:[this.order.status],
        deliverySlot:[[DeliverySlotsDto],
          [Validators.required]],
        supplierCategory: [this.order.supplierCategory,
          [Validators.required]],
          orderAssignmentOption:[this.order.assignmentSelectionType]
    });

    this.itemSelectionGroup = this.formBuilder.group({
      orderDetailsArray:this.formBuilder.array([OrderDetailDto])
      },
    {
      validator: OrderDetailsArrayLengthValidation('orderDetailsArray', this.orderDetails)
    });
    this.loadInitialData();
    this.orderHeaderGroup.get('orderType').setValue(2);
  }

  get orderDetailsArray():FormArray {
    return this.itemSelectionGroup.get('orderDetailsArray') as FormArray;
  }

  onResize(event) {
    //this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 4;
    this.breakpoint = event.target.innerWidth/300;
  }

  loadInitialData(){
    this.standardInventoryService.getStandardInventory().subscribe(data=>{
      this.standardInventoryItems = data;
    });
    this.deliverySlotsService.getDeliverySlot().subscribe(data=>{
      this.deliverySlotList = data;
      var curTime= new Date().getTime();        
      this.deliverySlotList.forEach(r=>{
        var delSlotInventoryUpdated = new Date(new Date().toDateString() + " " + r.cutoffTime).getTime();
        var timediff= curTime-delSlotInventoryUpdated;
        r.disabled = timediff>0;
      })
    });
  }

  DateChange(event: MatDatepickerInputEvent<Date>){
    const selectedDate = event.value;
    if(selectedDate){      
      var curDate= new Date();

      if((selectedDate.getDate() != curDate.getDate()) 
      || (selectedDate.getMonth() != curDate.getMonth()) 
      || (selectedDate.getFullYear() != curDate.getFullYear()) && (this.minDate>selectedDate)){
 
        this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Pre Order Confirmation";
        this.messageDto.messageBody = "Selected Expected Delivery Date is in future. Do you want to proceed this as a Pre-Order?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });

        dialogRef.afterClosed().subscribe(result => {
          if(result == true)
          {
            this.orderHeaderGroup.get('orderType').setValue(1);
            this.orderHeaderGroup.controls['orderAssignmentOption'].setValidators([Validators.required]);
            this.orderHeaderGroup.controls['orderAssignmentOption'].updateValueAndValidity();
          }
          else{
            this.orderHeaderGroup.get('expectedDiliveredDate').setValue(new Date());

              }
        });

      }
      else
      {
        this.orderHeaderGroup.get('orderType').setValue(2);
      }
    }
  }

  OnDeliverySlotChange(event:MatSelectChange)
  {
    const deliverySlot=event.value;
    if(this.orderHeaderGroup.get('orderType').value==1) return;
    if(deliverySlot==undefined) return;
    var curTime= new Date().getTime();
    var dateString=new Date().toDateString() + " " + deliverySlot.firstWaveTime;
    var delSlotCutoffTime = new Date(dateString).getTime();
    var timediff= curTime-delSlotCutoffTime;

    if(timediff<=0){
      this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Pre Order Confirmation";
        this.messageDto.messageBody = "As per the selected Delivery Slot this Order identified as pre order. Do you want to proceed this as a Pre-Order?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });

        dialogRef.afterClosed().subscribe(result => {
          if(result == true)
          {
            this.orderHeaderGroup.get('orderType').setValue(1);
            this.orderHeaderGroup.controls['orderAssignmentOption'].setValidators([Validators.required]);
            this.orderHeaderGroup.controls['orderAssignmentOption'].updateValueAndValidity();
          }
          else
          {
            this.orderHeaderGroup.get('deliverySlot').setValue(null);
          }
        });
    }
    else{
      this.orderHeaderGroup.get('orderType').setValue(2);
    }
  }

  onAddProduct(standardInventoryItem:StandardInventoryDto){

    if(standardInventoryItem.qty>0){
      var orderDetail = new OrderDetailDto();
      orderDetail.standardInventoryId = standardInventoryItem.id;
      orderDetail.itemName = standardInventoryItem.itemName;
      orderDetail.qty = standardInventoryItem.qty;
      standardInventoryItem.added = true;
      this.orderDetails.push(orderDetail);
      var frmArray =   this.itemSelectionGroup.get('orderDetailsArray') as FormArray;
      // while (frmArray.length) {
      //   frmArray.removeAt(0);
      // }
      frmArray.patchValue(this.orderDetails);

      this.RefreshOrderGrid();
    }
    else{
      this.snackBar.open('Can not add without Quantity', '', {
        duration: 3000
      });
    }
  }

  onRemoveProduct(standardInventoryItem:StandardInventoryDto){
    
    this.messageDto = new MessageDialogDto();
    this.messageDto.messageCaption = "Order Item Removal";
    this.messageDto.messageBody = "Do you want to remove " + standardInventoryItem.itemName + " from the Current Order?";
        
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
      disableClose:true,
      data : this.messageDto
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        const index = this.orderDetails.findIndex(r=> r.standardInventoryId == standardInventoryItem.id);
        this.orderDetails.splice(index,1);
        standardInventoryItem.qty = 0;
        standardInventoryItem.added = false;
      }
    });
    this.RefreshOrderGrid();
  }

  RefreshOrderGrid(){
    this.orderDetailsData = new MatTableDataSource(this.orderDetails);
        this.orderDetailsData.sort = this.sort;
        this.orderDetailsData.paginator = this.paginator;
  }

  SaveOrder(){
    this.order.expectedDiliveredDate=this.orderHeaderGroup.get('expectedDiliveredDate').value;
    this.order.supplierCategory=this.orderHeaderGroup.get('supplierCategory').value;
    this.order.deliverySlotId = this.orderHeaderGroup.get('deliverySlot').value.id;
    this.order.orderDetails = this.orderDetails;
    this.order.buyerId = 2;
    this.order.status=1; //Pending
    this.order.orderType=this.orderHeaderGroup.get('orderType').value;
    this.order.assignmentSelectionType=this.orderHeaderGroup.get('orderAssignmentOption').value == null? 0:this.orderHeaderGroup.get('orderAssignmentOption').value;
    this.orderService.addOrder(this.order).subscribe(result=>{
      if(this.order.orderType == 1){
        this.snackBar.open('Successfully added the order', '', {
          duration: 3000});
      } 
      else
      {
        if (result==null){
          this.snackBar.open('One or more items do not have daily inventory updation. Saved as the pre-order', '', {
            duration: 3000});
        }
        else{
          this.orderOptimizedPossibilities = result;
        }
      }     
    });
  }

  OrderHeaderSubmit(){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Order Details";
        this.messageDto.messageBody = this.orderHeaderGroup.value['expectedDiliveredDate'] 
        + "     " + this.orderHeaderGroup.value['deliverySlot'] + "     "
        + this.orderHeaderGroup.value['supplierCategory'] + "     " + this.order.expectedDiliveredDate 
        + "             " + this.order.deliverySlotId + "          " + this.order.supplierCategory;
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
  }

  ConfirmOrderOptimization(orderOptimizedPossibilityDto:OrderOptimizedPossibilityDto)
  {
    var orderPossibilitySelections:OrderPossibilitySelectionDto[];
    orderPossibilitySelections = new Array();
    var orderPossibilitySelection= new OrderPossibilitySelectionDto();
    orderPossibilitySelection.orderOptimizedPossibilityDto=orderOptimizedPossibilityDto;
    orderPossibilitySelection.isSelected=true;
    orderPossibilitySelections.push(orderPossibilitySelection);
    var remainOrderPossibilities = this.orderOptimizedPossibilities.filter(u=>u.orderOptimizedPossibilityId != orderOptimizedPossibilityDto.orderOptimizedPossibilityId);
    // var remainOrderPossibilities = this.orderOptimizedPossibilities;
    // const index = this.orderOptimizedPossibilities.findIndex(r=> r.orderOptimizedPossibilityId == orderOptimizedPossibilityDto.orderOptimizedPossibilityId);
    // remainOrderPossibilities.splice(index,1);
    remainOrderPossibilities.forEach (r=>{
      orderPossibilitySelection= new OrderPossibilitySelectionDto();
    orderPossibilitySelection.orderOptimizedPossibilityDto=r;
    orderPossibilitySelection.isSelected=false;
    orderPossibilitySelections.push(orderPossibilitySelection);
    });
  
    var orderPossibilitySelectionListDto=new OrderPossibilitySelectionListDto();
    orderPossibilitySelectionListDto.key=1;
    orderPossibilitySelectionListDto.orderPossibilitySelectionDtos=orderPossibilitySelections;

    var orderId = this.orderHeaderGroup.get('id').value;

    this.orderService.addOrderAssignments(orderPossibilitySelectionListDto).subscribe(result=>{
      this.snackBar.open('Order Assigned Successfully', '', {
        duration: 3000});   
    });
    
  }

}
