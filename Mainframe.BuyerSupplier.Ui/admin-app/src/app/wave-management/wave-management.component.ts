import { Component, OnInit } from '@angular/core';
import { DeliverySlotsService } from '../delivery-slots/delivery-slots.service';
import { DeliverySlotsDto } from '../delivery-slots/DeliverySlotsDto';
import { OrderService } from '../order/order.service';
import { MatSnackBar } from '@angular/material';
import { detectChangesInRootView } from '@angular/core/src/render3/instructions';

@Component({
  selector: 'app-wave-management',
  templateUrl: './wave-management.component.html',
  styleUrls: ['./wave-management.component.css']
})
export class WaveManagementComponent implements OnInit {

  deliverySlotList: DeliverySlotsDto[];
  selectedDeliverySlot:DeliverySlotsDto;

  constructor(
  private deliverySlotsService: DeliverySlotsService, 
  private orderService:OrderService,
  private snackBar: MatSnackBar) { }

  ngOnInit() {

    this.deliverySlotsService.getDeliverySlot().subscribe(data=>{
      this.deliverySlotList = data;
    });
    this.selectedDeliverySlot= new DeliverySlotsDto();
  }

  Click(){
    this.orderService.waveManagement(this.selectedDeliverySlot.id).subscribe(result=>{
      if(result){
        this.snackBar.open('Wave process successfully proceed with ' + this.selectedDeliverySlot.slotName, '', {
          duration: 3000});
      } 
      else
      {
        this.snackBar.open('Wave process failed with ' + this.selectedDeliverySlot.slotName, '', {
          duration: 3000});
      }     
    });
  }

}
