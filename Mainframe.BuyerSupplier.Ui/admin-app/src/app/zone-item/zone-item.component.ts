import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ZoneDto } from '../zone/ZoneDto';
import { ZoneService } from '../zone/zone.service';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import{SupplierBaseManagementService} from '../supplierBaseManagement/supplier-base-management.service';

@Component({
  selector: 'app-zone-item',
  templateUrl: './zone-item.component.html',
  styleUrls: ['./zone-item.component.css']
})
export class ZoneItemComponent implements OnInit {

  zoneItem : ZoneDto;
  header:string;
  zoneSupplierList:SupplierBaseDto[];

  formControl = new FormControl('', [
    Validators.required,
  ]);

  zoneForm:FormGroup;

  validation_messages = {

    'supplierBaseID': [{ type: 'required', message: 'Supplier Base Name is required' }],
    
    'name': [{ type: 'required', message: 'Name is required' }],

    'description': [{ type: 'required', message: 'Description is required' }],

    };

  constructor(private zone:ZoneService, private supplierBaseManagementService:SupplierBaseManagementService,private router:Router,
    public dialogRef: MatDialogRef<ZoneItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ZoneDto,
    public snackBar: MatSnackBar, private formBuilder: FormBuilder) 
    { 
      if(data)
      {
        this.zoneItem = data;
        this.header="Edit Zone";
      }
        else{
          this.zoneItem = new ZoneDto();
          this.header="Add a zone";
      }
    }
   
  ngOnInit() {

    this.supplierBaseManagementService.getSuppliers().subscribe(data =>
      {
        this.zoneSupplierList = data;
      });

    this.zoneForm = this.formBuilder.group({

      supplierBaseID: [this.zoneItem.supplierBaseID,[Validators.required]],
      
      name: [this.zoneItem.name,[Validators.required]],

      description: [this.zoneItem.description,[Validators.required]],

      });
  }

  save() {
  
    //update 
    if(this.zoneItem.id > 0)
    {
      this.zone.edit(this.zoneItem).subscribe(() => {
       this.dialogRef.close(true);
       this.snackBar.open('Successfully updated the zone', '', {
        duration: 2000
      });
      });
    } 
    //insert
    else {
      this.zone.add(this.zoneItem).subscribe(() => {
        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new zone', '', {
          duration: 3000
        });
       });
        }   
 }
}