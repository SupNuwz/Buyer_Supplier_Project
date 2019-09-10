import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { VehicleTypeDto } from '../vehicle-type/VehicleTypeDto';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { VehicleTypeService } from '../vehicle-type/vehicle-type.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from '@angular/material';

@Component({
  selector: 'app-vehicle-type-item',
  templateUrl: './vehicle-type-item.component.html',
  styleUrls: ['./vehicle-type-item.component.css']
})
export class VehicleTypeItemComponent implements OnInit {
  vehicleTypeItem:VehicleTypeDto;
  header:string;

  vehicleTypeForm:FormGroup;

  validation_messages = {
    'name': [{ type: 'required', message: 'name is required' }],
    'description': [{ type: 'required', message: 'Description is required' }],
  }

constructor(private router:Router, private vehicleTypeService:VehicleTypeService,
  public dialogRef: MatDialogRef<VehicleTypeItemComponent>,
  @Inject(MAT_DIALOG_DATA) public data: VehicleTypeDto,public snackBar: MatSnackBar, private formBuilder:FormBuilder) {

    if(data)
    {
    this.vehicleTypeItem = data;
    this.header= "Edit an unit of measure";
  }
    else{
      this.vehicleTypeItem = new VehicleTypeDto();
      this.header= "Add an vehicle type";
    }
}
 
@ViewChild('vehicleType') vehicleTypeInput: MatInput;

ngOnInit() {
  this.vehicleTypeForm = this.formBuilder.group({
    name: [this.vehicleTypeItem.name,[Validators.required]],
    description: [this.vehicleTypeItem.description,[Validators.required]]
  })

  this.vehicleTypeInput.focus();
}

save(){
  this.vehicleTypeService.getInventoryItemAvailability(this.vehicleTypeItem.name, this.vehicleTypeItem.id).subscribe(data => {
    if(data==true){
      alert("This vehicle type item is already available in the vehicle type list");
    }  
    else{
      this.saveData();
    }      
  });    
}

saveData() {
  //Update
  if(this.vehicleTypeItem.id > 0)
  {
    this.vehicleTypeService.editVehicleType(this.vehicleTypeItem)
      .subscribe(() => {
        this.dialogRef.close(true);  
        this.snackBar.open('Successfully updated the vehicle type', '', {
          duration: 2000
        });

      });
  }

  // Insert
  else{
    this.vehicleTypeService.addVehicleType(this.vehicleTypeItem)
      .subscribe(() => {
        this.dialogRef.close(true);    
        this.snackBar.open('Successfully added a new vehicle type', '', {
          duration: 2000    
      });
  });
}
}
}

