
import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { UserDto } from '../Users-List/UserDto';
import { UserService } from '../Users-List/user.service';
import {SelectionModel} from '@angular/cdk/collections';
import { DeliverySlotsDto } from '../delivery-slots/DeliverySlotsDto';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import { SupplierStandardInventoryDto } from './SupplierStandardInventoryDto';
import { SupplierStandardInventoryService } from './supplier-standard-inventory.service';
import { FormControl, Validators,FormBuilder,FormGroup, MaxLengthValidator } from '@angular/forms';
import { UserCreationInitialDataDto } from '../Users-List/UserCreationInitialDataDto';
import { ZoneDto } from '../zone/ZoneDto';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user : UserDto;
  userTypes: string[] = ['Admin', 'Buyer', 'Supplier'];
  categories: string[] = ['Pola', 'Farm Direct', 'Organic'];
  heading:string;

  displayedColumns: string[] = ['select', 'inventoryItemName', 'group'];
  selection = new SelectionModel(true, []);
  standardInventoryList :any;
  standardInventoryDataList: SupplierStandardInventoryDto[];

  deliverySlotList : DeliverySlotsDto[];
  supplierBaseList : SupplierBaseDto[];
  zoneList : ZoneDto[];

  userForm:FormGroup;
  disabledUserType : boolean;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  validationMessages = {
    'userType': [ { type:'required', message: 'User Type is required' }],
    'name': [ { type:'required', message: 'Name is required'},{type:'pattern',message:'Enter a valid name'}],
    'address': [{ type:'required', message: 'Address is required' }],
    'contactNo': [{ type:'required', message: 'Contact No is required'},{type:'pattern',message:'Enter valid phone number'} ],
    'email': [{ type:'required', message: 'Email is required' },{type:'pattern',message:'Enter a valid email'}],
    'supplierbase':[{ type:'required', message: 'Supplier base is required' }],
    'relevantZone':[{type:'required', message: 'Relevant Zone is required'}],
    'deliverySlot':[{ type:'required', message: 'Delivery Slot is required' }],
    'category':[{ type:'required', message: 'category is required' }],
  }

  constructor(private router:Router, private userService:UserService,public dialogRef: MatDialogRef<UserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDto, public snackBar: MatSnackBar,
    private supplierStandardInventory:SupplierStandardInventoryService,private formBuilder: FormBuilder ) {
      if(data)
      {
        this.user = data;
        this.heading="Edit user";
      }
      else{        
          this.heading="Add new User";
          this.user = new UserDto();
        }
  }
  ngOnInit() 
    {
      this.getInitialuserdata();

      this.userForm = this.formBuilder.group({
        userType: [this.user.userType, [Validators.required]],
        name: [this.user.name,[Validators.required,Validators.pattern("^[a-zA-Z ]*$")]],
        address: [this.user.address,[Validators.required]],
        contactNo: [this.user.contactNo,[Validators.required,Validators.pattern("^[0-9]{9}")]],
        email: [this.user.email,[Validators.required,Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
        supplierbase: [this.user.defaultSupplierBaseId,[Validators.required]],
        relevantZone:[this.user.relevantZoneId,[Validators.required]],
        deliverySlot: [this.user.deliverySlotId,[Validators.required]],
        category: [this.user.category,[Validators.required]],        
        });

        const deliverySlot = this.userForm.get('deliverySlot');
        const category = this.userForm.get('category');
        const supplierbase = this.userForm.get('supplierbase');
        const relevantZone = this.userForm.get('relevantZone')

        if(this.user.id > 0)
        {
          this.disabledUserType = true;
        }
        
        this.userForm.get('userType').valueChanges.subscribe(
          (userType) => {
            
            deliverySlot.setValidators([Validators.required]);
            category.setValidators([Validators.required]);
            supplierbase.setValidators([Validators.required]);
            relevantZone.setValidators([Validators.required]);

            if(userType == 'Buyer'){
              deliverySlot.clearValidators();
              category.clearValidators();
            }
            else if(userType == 'Admin'){
              deliverySlot.clearValidators();
              category.clearValidators();
              supplierbase.clearValidators();
              relevantZone.clearValidators();
            }

            else if(userType == 'Supplier'){
            this.getInitialuserdata();
            relevantZone.clearValidators();
            }

            deliverySlot.updateValueAndValidity();
            category.updateValueAndValidity();
            supplierbase.updateValueAndValidity();
            relevantZone.updateValueAndValidity();
          });
  
    }
    
    getInitialuserdata()
    {    
      this.userService.getInitialUserData(this.user.id).subscribe(data=>
        {
      this.standardInventoryList = new MatTableDataSource(data.standardInventoryList);
      this.standardInventoryList.sort =this.sort;
      this.standardInventoryList.paginator = this.paginator;

      this.standardInventoryList.data.forEach(row =>
      {
        if(row.isSelected)
        {
          this.selection.select(row)
        }
      });
  
          this.deliverySlotList = data.deliverySlotList;
          this.supplierBaseList = data.supplierBaseList;
          this.zoneList = data.zoneList;
        }
      );
    }

    // refreshStandardInventory(data:SupplierStandardInventoryDto[]){
    //   var sortedData = data.sort((a, b) => {
    //     return compare(a.isSelected, b.isSelected, false);
    //   });
    // }

    isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.standardInventoryList.data.length;
      return numSelected === numRows;
    }
  
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
      this.isAllSelected() ?
          this.selection.clear() :
          this.standardInventoryList.data.forEach(row => this.selection.select(row));
    }
  
  save() {
    //Update
    if(this.user.id > 0)
    {
      
      this.userService.editUser(this.user)
      .subscribe(id => { 

        if(this.user.userType == "Supplier")
        {
          this.updateSupplierStandardInventory();      
        }
        
          this.dialogRef.close(true);  
          this.snackBar.open('Successfully updated the user', '', {
            duration: 3000
          });    
        });
    }

    // Insert
    else{
      this.userService.addUser(this.user)
        .subscribe(id => { 

          if(this.user.userType == "Supplier")
          {
            this.addSupplierStandardInventory(id);
          }

          this.dialogRef.close(true);  
          this.snackBar.open('Successfully added a new user', '', {
            duration: 3000
          });       
        });
    }  
  }

  addSupplierStandardInventory(userid:number)
  {
    this.standardInventoryList.data.forEach(row => {
      row.supplierID = userid,
      row.isSelected = this.selection.isSelected(row)});
      
    this.supplierStandardInventory.addSupplierStandardInventory(this.standardInventoryList.data)
    .subscribe(id => {});
  }

  updateSupplierStandardInventory()
  {
    this.standardInventoryList.data.forEach(row => {
      row.isSelected = this.selection.isSelected(row)});
      
    this.supplierStandardInventory.editSupplierStandardInventory(this.standardInventoryList.data)
    .subscribe(id => {});
  } 
}

// function compare(a: any, b: any, isAsc: boolean) {
//   return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
// }




