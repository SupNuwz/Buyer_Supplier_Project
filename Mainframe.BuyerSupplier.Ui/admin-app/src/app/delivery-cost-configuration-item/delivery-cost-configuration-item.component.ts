import { Component, OnInit,Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, MatInput } from '@angular/material';
import { SupplierBaseManagementService } from '../supplierBaseManagement/supplier-base-management.service';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import { DeliveryCostConfigurationService } from '../delivery-cost-configuration/delivery-cost-configuration.service';
import { DeliveryCostConfigurationDto } from '../delivery-cost-configuration/DeliveryCostConfigurationDto';

@Component({
  selector: 'app-delivery-cost-configuration-item',
  templateUrl: './delivery-cost-configuration-item.component.html',
  styleUrls: ['./delivery-cost-configuration-item.component.css']
})
export class DeliveryCostConfigurationItemComponent implements OnInit {

  deliveryCostConfigurationItem:DeliveryCostConfigurationDto;
  header:string;
  supplierBaseList:SupplierBaseDto[];
  costConfigerForm: FormGroup;   
 

  validationMessages = {                                                   
     'name': [{ type: 'required', message: 'Name is required' }],
     'description': [{ type: 'required', message: 'Description is required' }],
     'baseLocation': [{ type: 'required', message: 'supplier Base location is required' }],
     'baseFare': [{ type: 'required', message: 'Base Fare is required' }],                               
     'baseDistance': [{ type: 'required', message: 'Base Distance is required' }],
     'additionalRate': [{ type: 'required', message: 'Additional rate per km is required' }],
  };

  constructor(private deliveryCostConfiguration: DeliveryCostConfigurationService,
      private supplierBaseManagement:SupplierBaseManagementService ,private router:Router, 
      public dialogRef: MatDialogRef<DeliveryCostConfigurationItemComponent>,
      @Inject(MAT_DIALOG_DATA) public data: DeliveryCostConfigurationDto, 
      public snackBar: MatSnackBar, private formBuilder: FormBuilder) 
  
  {     
    if(data)
    {
      this.deliveryCostConfigurationItem = data;
      this.header="Edit Delivery Cost Configuration";
    }
    else{
      this.deliveryCostConfigurationItem = new DeliveryCostConfigurationDto();
      this.header="Add an item to Delivery Cost Configuration";
    }
  }
 
  @ViewChild('deliveryCostConfigurationName') deliveryCostConfigurationNameInput: MatInput;
   
  ngOnInit() {
    this.supplierBaseManagement.getSuppliers().subscribe(data =>
      {
        this.supplierBaseList = data;
      });

    this.costConfigerForm = this.formBuilder.group({                       
      name: [this.deliveryCostConfigurationItem.name, [Validators.required]],
      description: [this.deliveryCostConfigurationItem.description, [Validators.required]],                       
      baseLocation: [this.deliveryCostConfigurationItem.baseLocationID, [Validators.required]],
      baseFare: [this.deliveryCostConfigurationItem.baseFare, [Validators.required]],                       
      baseDistance: [this.deliveryCostConfigurationItem.baseDistance, [Validators.required]],
      additionalRate: [this.deliveryCostConfigurationItem.additionalRate, [Validators.required]],
    });

    this.deliveryCostConfigurationNameInput.focus();
  }
  
  save() {
  
      //update 
      if(this.deliveryCostConfigurationItem.id > 0)
      {
        this.deliveryCostConfiguration.update(this.deliveryCostConfigurationItem)
        .subscribe(() => {
          this.dialogRef.close(true);  
          this.snackBar.open('Successfully updated the delivery cost configuration', '', {
            duration: 2000
          }); 
        });
      } 

      //insert
      else {
        this.deliveryCostConfiguration.add(this.deliveryCostConfigurationItem)
        .subscribe(() => {
          this.dialogRef.close(true);    
          this.snackBar.open('Successfully added a new delivery cost configuration', '', {
            duration: 2000    
          });
        });
      }  
  }
}
