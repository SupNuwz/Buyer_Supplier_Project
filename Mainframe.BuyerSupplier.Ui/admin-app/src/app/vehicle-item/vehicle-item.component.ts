import { Component, OnInit,Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, MatInput, MatSlideToggleModule } from '@angular/material';
import { SupplierBaseManagementService } from '../supplierBaseManagement/supplier-base-management.service';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import { VehicleTypeService } from '../vehicle-type/vehicle-type.service';
import { VehicleTypeDto } from '../vehicle-type/VehicleTypeDto';
import { VehicleService } from '../vehicle/vehicle.service';
import { VehicleDto } from '../vehicle/VehicleDto';

@Component({
  selector: 'app-vehicle-item',
  templateUrl: './vehicle-item.component.html',
  styleUrls: ['./vehicle-item.component.css']
})
export class VehicleItemComponent implements OnInit {

  vehicleItem:VehicleDto;
  header:string;
  supplierBaseList:SupplierBaseDto[];
  vehicleTypeList:VehicleTypeDto[];
  vehicleForm: FormGroup;   
 isAvaialable:boolean;

  validationMessages = {                                                   
     'supplierBase': [{ type: 'required', message: 'Supplier Base is required' }],
     'driverContactNo': [{ type: 'required', message: 'Driver Contact Number is required' },{type:'pattern',message:'Enter valid phone number'}],
     'numberPlate': [{ type: 'required', message: 'Number Plate is required' }],                               
     'vehicleType': [{ type: 'required', message: 'Vehicle Type is required' }],
     'colorCode': [{ type: 'required', message: 'Color Code is required' }],
     'maximumCapacity': [{ type: 'required', message: 'Maximum Capacity is required' }]
  };

  constructor(private vehicle: VehicleService,
      private supplierBaseManagement:SupplierBaseManagementService ,
      private vehicleTypeManagement:VehicleTypeService ,private router:Router, 
      public dialogRef: MatDialogRef<VehicleItemComponent>,
      @Inject(MAT_DIALOG_DATA) public data: VehicleDto, 
      public snackBar: MatSnackBar, private formBuilder: FormBuilder) 
  
  {     
    if(data)
    {
      this.vehicleItem = data;
      this.header="Edit Vehicle";
    }
    else{
      this.vehicleItem = new VehicleDto();
      this.header="Add an item to Vehicle";
    }
  }
   
  ngOnInit() {
    this.supplierBaseManagement.getSuppliers().subscribe(data =>
      {
        this.supplierBaseList = data;
      });
    this.vehicleTypeManagement.getVehicleType().subscribe(data =>
      {
        this.vehicleTypeList = data;
      });

    this.vehicleForm = this.formBuilder.group({                            
      supplierBase: [this.vehicleItem.supplierBaseId, [Validators.required]],       
      driverContactNo: [this.vehicleItem.driverContactNo, [Validators.required,Validators.pattern("^[0-9]{9}")]],       
      numberPlate: [this.vehicleItem.numberPlate, [Validators.required]],                         
      vehicleType: [this.vehicleItem.vehicleTypeId, [Validators.required]],
      colorCode: [this.vehicleItem.colorCode, [Validators.required]],
      maximumCapacity: [this.vehicleItem.maximumCapacity, [Validators.required]],
      availability:[this.vehicleItem.availability, [Validators.required]]
    });
  }
  
  save() {
  
      //update 
      if(this.vehicleItem.id > 0)
      {
        this.vehicle.update(this.vehicleItem)
        .subscribe(() => {
          this.dialogRef.close(true);  
          this.snackBar.open('Successfully updated the vehicle', '', {
            duration: 2000
          }); 
        });
      } 

      //insert
      else {
        this.vehicle.add(this.vehicleItem)
        .subscribe(() => {
          this.dialogRef.close(true);    
          this.snackBar.open('Successfully added a new vehicle', '', {
            duration: 2000    
          });
        });
      }  
  }
}

