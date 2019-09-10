import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SupplierInventoryDto } from '../supplier-inventory/SupplierInventoryDto';
import { SupplierInventoryService } from '../supplier-inventory/supplier-inventory.service';
import { SupplierStandardInventoryDto } from "../user/SupplierStandardInventoryDto";

@Component({
  selector: 'app-supplier-inventory-item',
  templateUrl: './supplier-inventory-item.component.html',
  styleUrls: ['./supplier-inventory-item.component.css']
})
export class SupplierInventoryItemComponent implements OnInit {

  heading:string;
  supplierInventoryItem:SupplierInventoryDto;
  supplierInventoryForm: FormGroup;  
  // grades: string[] = ['A','B'];
  formControl = new FormControl('', [ Validators.required, ]);

  validationMessages = {                                                  
    'unitPrice': [{ type: 'required', message: 'Unit price is required' }],
    'qty': [{ type: 'required', message: 'Quantity is required' }],
 };
  
  constructor(private router:Router, private supplierInventoryService:SupplierInventoryService,
    public dialogRef: MatDialogRef<SupplierInventoryItemComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: SupplierStandardInventoryDto, 
    public snackBar: MatSnackBar, private formBuilder: FormBuilder) { 

      if(data){
        this.supplierInventoryItem = new SupplierInventoryDto();
        this.supplierInventoryItem.supplierStandardInventoryId = data.id;
        this.heading = data.inventoryItemName;
      }
    }

  ngOnInit() {
    
    this.supplierInventoryForm = this.formBuilder.group({           
      unitPrice: [this.supplierInventoryItem.unitPrice, [Validators.required]],
      qty:[this.supplierInventoryItem.qty, [Validators.required]]
    });
  
  }

  save(){

    this.supplierInventoryItem.availableQty= this.supplierInventoryItem.qty;

    this.supplierInventoryService.addSupplierInventory(this.supplierInventoryItem)
      .subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully updated to the inventory', '', {
          duration: 3000
        });
      });
  }

}
