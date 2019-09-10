import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from '@angular/material';
import { DiscountConfigurationDto } from '../discount-configuration/discountConfigurationDto';
import { DiscountConfigurationService } from '../discount-configuration/discount-configuration.service';

@Component({
  selector: 'app-discount-configuration-item',
  templateUrl: './discount-configuration-item.component.html',
  styleUrls: ['./discount-configuration-item.component.css']
})
export class DiscountConfigurationItemComponent implements OnInit {

  discountItem:DiscountConfigurationDto;
  header:string;

  formControl = new FormControl('', [
    Validators.required,
  ]);

  discountForm:FormGroup;

  validation_messages = {

    'name': [{ type: 'required', message: 'Discount Name is required' }],
    
    'fromDate': [{ type: 'required', message: 'From Date is required' }],

    'toDate': [{ type: 'required', message: 'To Date is required' }],

    'fromTime': [{ type: 'required', message: 'From Time is required' }],

    'toTime': [{ type: 'required', message: 'To Time is required' }],

    'rate': [{ type: 'required', message: 'Rate is required' }],
    };

  constructor(private discount:DiscountConfigurationService,private router:Router,
    public dialogRef: MatDialogRef<DiscountConfigurationItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DiscountConfigurationDto,
    public snackBar: MatSnackBar, private formBuilder: FormBuilder) 
    { 
      if(data)
      {
        this.discountItem = data;
        this.header="Edit Discount Configuration";
      }
        else{
          this.discountItem = new DiscountConfigurationDto();
          this.header="Add a Discount Configuration";
      }
    }
    
    @ViewChild('discountName') DiscountNameInput: MatInput;

  ngOnInit() {

    this.discountForm = this.formBuilder.group({

      name: [this.discountItem.name,[Validators.required]],
      fromDate: [this.discountItem.fromDate,[Validators.required]],
      toDate: [this.discountItem.toDate,[Validators.required]],
      fromTime: [this.discountItem.fromTime,[Validators.required]],
      toTime: [this.discountItem.toTime,[Validators.required]],
      rate: [this.discountItem.rate,[Validators.required]],
    });

    
  this.DiscountNameInput.focus();

  }
  
  save() {
  
    //update 
    if(this.discountItem.id > 0)
    {
      this.discount.edit(this.discountItem).subscribe(() => {
       this.dialogRef.close(true);
       this.snackBar.open('Successfully updated the Discount Configuration', '', {
        duration: 3000
      });
      });
    } 
    //insert
    else {
      this.discount.add(this.discountItem).subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new Discount Configuration', '', {
          duration: 3000
        });
       });
        }   
 }

}
