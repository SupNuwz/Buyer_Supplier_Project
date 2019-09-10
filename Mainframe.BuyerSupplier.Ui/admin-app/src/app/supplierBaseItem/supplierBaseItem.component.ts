import { Component, OnInit , Inject, ViewChild } from '@angular/core';
import {SupplierBaseManagementService } from '../supplierBaseManagement/supplier-base-management.service';
import { Router } from '@angular/router';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import {MatDialogRef,MatDialog,MAT_DIALOG_DATA, MatSnackBar, MatInput } from '@angular/material';
import { FormControl, Validators,FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-supplierBaseItem',
  templateUrl: './supplierBaseItem.component.html',
  styleUrls: ['./supplierBaseItem.component.css']
})
export class SupplierBaseItemComponent implements OnInit {

  deliverySlots: string[] = ['Morning', 'Evening'];
  supplierBaseItem:SupplierBaseDto;
  header:string;
  formControl = new FormControl('', [
  Validators.required, 
   ]);

  supplierBaseForm:FormGroup;

  validation_messages = {

    'supplierBaseName': [{ type: 'required', message: 'Supplier Base Name is required' }],
    
    'deliverySlot': [{ type: 'required', message: 'Delivery Slot is required' }],
    
    };
    

  constructor(private SupplierBaseManagement: SupplierBaseManagementService ,private router:Router, 
    public dialogRef: MatDialogRef<SupplierBaseItemComponent>,@Inject(MAT_DIALOG_DATA) public data: SupplierBaseDto,
    private formBuilder: FormBuilder, public snackBar: MatSnackBar) { 

  if(data)
  {
    this.supplierBaseItem = data;
    this.header="Edit Supplier Base List";
  }
    else{
      this.supplierBaseItem = new SupplierBaseDto();
      this.header="Add an item to Supplier Base List";
    }
  }
 
@ViewChild('basename') BasenameInput: MatInput;

ngOnInit() {

  this.supplierBaseForm = this.formBuilder.group({

    supplierBaseName: [this.supplierBaseItem.supplierBaseName,[Validators.required]],
    
    deliverySlot: [this.supplierBaseItem.deliverySlot,[Validators.required]],
    
  });

  this.BasenameInput.focus();

  }

  save(){
    this.SupplierBaseManagement.getInventoryItemAvailability(this.supplierBaseItem.supplierBaseName, this.supplierBaseItem.supplierBaseId).subscribe(data => {
      if(data==true){
        alert("This supplier base item is already available in the supplier base list");
      }  
      else{
        this.saveData();
      }      
    });    
  }
  

  saveData() {

    //update 
    if(this.supplierBaseItem.supplierBaseId > 0)
    {
      this.SupplierBaseManagement.update(this.supplierBaseItem).subscribe(() => {
       this.dialogRef.close(true);
       this.snackBar.open('Successfully updated the supplier base', '', {
        duration: 3000
      });
      });

    } 
    //insert
    else {
      this.SupplierBaseManagement.add(this.supplierBaseItem).subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new supplier base', '', {
          duration: 3000
        });
       });
    }  
  

 }
 
}


  


