import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from '@angular/material';
import { CommissionConfigurationDto } from '../commission-configuration/commissionConfigurationDto';
import { CommissionConfigurationService } from '../commission-configuration/commission-configuration.service';

@Component({
  selector: 'app-commission-configuration-item',
  templateUrl: './commission-configuration-item.component.html',
  styleUrls: ['./commission-configuration-item.component.css']
})
export class CommissionConfigurationItemComponent implements OnInit {

  commissionItem:CommissionConfigurationDto;
  header:string;

  formControl = new FormControl('', [
    Validators.required,
  ]);

  commissionForm:FormGroup;

  validation_messages = {

    'name': [{ type: 'required', message: 'Commission Name is required' }],
    
    'fromDate': [{ type: 'required', message: 'From Date is required' }],

    'toDate': [{ type: 'required', message: 'To Date is required' }],

    'fromTime': [{ type: 'required', message: 'From Time is required' }],

    'toTime': [{ type: 'required', message: 'To Time is required' }],

    'rate': [{ type: 'required', message: 'Rate is required' }],
    };

  constructor(private commission:CommissionConfigurationService,private router:Router,
    public dialogRef: MatDialogRef<CommissionConfigurationItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CommissionConfigurationDto,
    public snackBar: MatSnackBar, private formBuilder: FormBuilder) 
    { 
      if(data)
      {
        this.commissionItem = data;
        this.header="Edit Commission Configuration";
      }
        else{
          this.commissionItem = new CommissionConfigurationDto();
          this.header="Add a Commission Configuration";
      }
    }
    
    @ViewChild('commissionItemName') commissionItemNameInput: MatInput;

  ngOnInit() {
    this.commissionForm = this.formBuilder.group({

      name: [this.commissionItem.name,[Validators.required]],
      fromDate: [this.commissionItem.fromDate,[Validators.required]],
      toDate: [this.commissionItem.toDate,[Validators.required]],
      fromTime: [this.commissionItem.fromTime,[Validators.required]],
      toTime: [this.commissionItem.toTime,[Validators.required]],
      rate: [this.commissionItem.rate,[Validators.required]],
    });
      
    this.commissionItemNameInput.focus();

  }

  save() {
  
    //update 
    if(this.commissionItem.id > 0)
    {
      this.commission.edit(this.commissionItem).subscribe(() => {
       this.dialogRef.close(true);
       this.snackBar.open('Successfully updated the Commission Configuration', '', {
        duration: 3000
      });
      });
    } 
    
    //insert
    else {
      this.commission.add(this.commissionItem).subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new Commission Configuration', '', {
          duration: 3000
        });
       });
        }   
 }

}
