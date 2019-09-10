import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { UnitOfMeasureDto } from '../unit-of-measure/UnitOfMeasureDto';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { UnitofmeasureService } from '../unit-of-measure/unitofmeasure.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from '@angular/material';

@Component({
  selector: 'app-unit-of-measure-item',
  templateUrl: './unit-of-measure-item.component.html',
  styleUrls: ['./unit-of-measure-item.component.css']
})
export class UnitOfMeasureItemComponent implements OnInit {
  unitMeasureItem:UnitOfMeasureDto;
  header:string;

  unitOfMeasureForm:FormGroup;

  validation_messages = {
    'name': [{ type: 'required', message: 'name is required' }],
    'description': [{ type: 'required', message: 'Description is required' }],
  }

constructor(private router:Router, private unitOfMeasureService:UnitofmeasureService,
  public dialogRef: MatDialogRef<UnitOfMeasureItemComponent>,
  @Inject(MAT_DIALOG_DATA) public data: UnitOfMeasureDto,public snackBar: MatSnackBar, private formBuilder:FormBuilder) {

    if(data)
    {
    this.unitMeasureItem = data;
    this.header= "Edit an unit of measure";
  }
    else{
      this.unitMeasureItem = new UnitOfMeasureDto();
      this.header= "Add an unit of measure";
    }
}
 
@ViewChild('unitOfMeasure') unitOfMeasureInput: MatInput;

ngOnInit() {
  this.unitOfMeasureForm = this.formBuilder.group({
    name: [this.unitMeasureItem.name,[Validators.required]],
    description: [this.unitMeasureItem.description,[Validators.required]]
  })

  this.unitOfMeasureInput.focus();
}

save(){
  this.unitOfMeasureService.getInventoryItemAvailability(this.unitMeasureItem.name, this.unitMeasureItem.id).subscribe(data => {
    if(data==true){
      alert("This unit of measure item is already available in the unit of measure list");
    }  
    else{
      this.saveData();
    }      
  });    
}

saveData() {
  //Update
  if(this.unitMeasureItem.id > 0)
  {
    this.unitOfMeasureService.editUnitOfMeasure(this.unitMeasureItem)
      .subscribe(() => {
        this.dialogRef.close(true);  
        this.snackBar.open('Successfully updated the unit of measure', '', {
          duration: 2000
        });

      });
  }

  // Insert
  else{
    this.unitOfMeasureService.addUnitOfMeasure(this.unitMeasureItem)
      .subscribe(() => {
        this.dialogRef.close(true);    
        this.snackBar.open('Successfully added a new unit of measure', '', {
          duration: 2000    
      });
  });
}
}
}

