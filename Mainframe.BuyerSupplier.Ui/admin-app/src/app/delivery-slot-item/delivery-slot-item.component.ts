import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Router} from '@angular/router';
import { DeliverySlotsService } from '../delivery-slots/delivery-slots.service';
import { DeliverySlotsDto } from '../delivery-slots/DeliverySlotsDto';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA, MatInput } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-delivery-slot-item',
  templateUrl: './delivery-slot-item.component.html',
  styleUrls: ['./delivery-slot-item.component.css']
})
export class DeliverySlotItemComponent implements OnInit {

  slotItem : DeliverySlotsDto;
  header: string;
  DeliverySlotForm: FormGroup;          
 

  validationMessages = {                                                 
     'slotName': [{ type: 'required', message: 'Slot Name is required' }],                                                 
     'cutoffTime': [{ type: 'required', message: 'Order Cutoff Time is required' }],
     'startTime': [{ type: 'required', message: 'Starting Time is required' }],
     'countdownTime': [{ type: 'required', message: 'Beging of the Countdown is required' }],                                                 
     'orderAcceptTime': [{ type: 'required', message: 'Order Accept Time is required' }],
     'orderCofirmTime': [{ type: 'required', message: 'Confirmation Cutoff Time is required' }],                                                 
     'dispatchesConfirmTime': [{ type: 'required', message: 'Order Dispatch Time is required' }],
     'endTime': [{ type: 'required', message: 'Ending Time is required' }],
  };                                                                       


  constructor(private router:Router, private deliverySlotService:DeliverySlotsService,
      public dialogRef:MatDialogRef<DeliverySlotItemComponent>,
      @Inject(MAT_DIALOG_DATA) public data:DeliverySlotsDto,
      public snackBar:MatSnackBar, private formBuilder: FormBuilder)

  {
      if(data) 
      {
        this.slotItem = data;
        this.header = "Edit Delevery Slot";
      }
      else 
      {
        this.slotItem = new DeliverySlotsDto();
        this.header = "Add New Delivery Slot";
      }
  }
 
  @ViewChild('slotName') BasenameInput: MatInput;

  ngOnInit() {

    this.BasenameInput.focus();

    this.DeliverySlotForm = this.formBuilder.group({                       
      slotName: [this.slotItem.slotName, [Validators.required]],
      cutoffTime: [this.slotItem.cutoffTime, [Validators.required]],                       
      startTime: [this.slotItem.startTime, [Validators.required]],
      countdownTime: [this.slotItem.countdownTime, [Validators.required]],                       
      orderAcceptTime: [this.slotItem.orderAcceptTime, [Validators.required]],
      orderCofirmTime: [this.slotItem.orderCofirmTime, [Validators.required]],                       
      dispatchesConfirmTime: [this.slotItem.dispatchesConfirmTime, [Validators.required]],
      endTime: [this.slotItem.endTime, [Validators.required]],
      });
  }

  save(){
      this.deliverySlotService.getInventoryItemAvailability(this.slotItem.slotName, this.slotItem.id).subscribe(data => {
        if(data==true){
          alert("This delivery slots item is already available in the delivery slots list");
        }  
        else{
          this.saveData();
        }      
      });    
    }

  saveData() {
    //Update 
    if(this.slotItem.id>0) 
    {
      this.deliverySlotService.editDeliverySlot(this.slotItem)
      .subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully updated the delivery slot', '', {
          duration: 3000
        });
      });
    }

    //Insert
    else{
      this.deliverySlotService.addDeliverySlot(this.slotItem)
      .subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new delivery slot', '', {
          duration: 3000
        });
      });
    }
  }

}
