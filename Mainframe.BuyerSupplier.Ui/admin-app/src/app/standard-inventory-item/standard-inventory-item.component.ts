import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { StandardInventoryService } from '../standard-inventory/standard-inventory.service'
import { StandardInventoryDto } from '../standard-inventory/StandardInventoryDto';
import {MatDialogRef, MAT_DIALOG_DATA, MatInput } from '@angular/material';
import {MatSnackBar} from '@angular/material';
import {FormControl, Validators, FormGroup, FormBuilder} from '@angular/forms';
import { UnitOfMeasureDto } from '../unit-of-measure/UnitOfMeasureDto';
import { UnitofmeasureService } from '../unit-of-measure/unitofmeasure.service';
import { FileServerDto } from '../standard-inventory/FileServerDto';
import { ThrowStmt } from '@angular/compiler';
import { promise } from 'protractor';
import { InventoryItemCategoriesDto } from '../inventory-item-categories/InventoryItemCategoriesDto';
import { InventoryItemSubCategoryDto } from '../inventory-item-sub-category/InventoryItemSubCategoryDto';
import { InventoryItemCategoriesService } from '../inventory-item-categories/inventory-item-categories.service';
import { InventoryItemSubCategoryService } from '../inventory-item-sub-category/inventory-item-sub-category.service';

@Component({
  selector: 'app-standard-inventory-item',
  templateUrl: './standard-inventory-item.component.html',
  styleUrls: ['./standard-inventory-item.component.css']
})
export class StandardInventoryItemComponent implements OnInit {
 inventoryItem : StandardInventoryDto;
  
 quantityUnitOfMeasure:UnitOfMeasureDto[];
 itemCategories:InventoryItemCategoriesDto[];
 inventorySubCategory:InventoryItemSubCategoryDto[];
 selectedInventorySubCategory:InventoryItemSubCategoryDto[];

 seasonalityTypes: string[] = ['Season','Out-of-season'];
 header:string;
  url = '';
  file:any;
  fileServerDto:FileServerDto;

  standardInventoryForm:FormGroup;

  
validation_messages = {
  'itemName': [{ type: 'required', message: 'Item name is required' }],
  'inventoryItemCategoryId': [{ type: 'required', message: 'Inventory Item Category is required' }],
  'inventoryItemSubCategoryId': [{ type: 'required', message: 'Inventory Item Sub Category is required' }],
  'quantityUnitOfMesureId': [{ type: 'required', message: 'Order Quantity Basis is required' }],
  'seasonality': [{ type: 'required', message: 'Seasonality is required' }],
  'minimumInventory': [{ type: 'required', message: 'Minimum Inventory Level is required' }],
  };

  constructor(private router:Router, private standardInventoryService:StandardInventoryService, private unitOfMesureService: UnitofmeasureService,
    private inventoryItemCategoriesService:InventoryItemCategoriesService, private inventoryItemSubCategoryService:InventoryItemSubCategoryService,
    public dialogRef: MatDialogRef<StandardInventoryItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: StandardInventoryDto,public snackBar: MatSnackBar, private formBuilder:FormBuilder) {

      if(data)
      {
      this.inventoryItem = data;
      this.url = this.inventoryItem.fileUrl;
      this.header= "Edit a Standard Inventory Item";
    }
      else{
        this.inventoryItem = new StandardInventoryDto();
        this.header= "Add a Standard Inventory Item";
      }
    } 

    @ViewChild('itemName') ItemNameInput: MatInput;
    

  ngOnInit() {

    this.unitOfMesureService.getUnitOfMeasure().subscribe(data=>
      {
        this.quantityUnitOfMeasure = data;
      });

    this.inventoryItemCategoriesService.getInventoryItemCategory().subscribe(data=>
      {
        this.itemCategories = data;
      });

    this.inventoryItemSubCategoryService.getNewCategory().subscribe(data=>
      {
        this.inventorySubCategory = data;
      });


        this.standardInventoryForm = this.formBuilder.group({

        itemName: [this.inventoryItem.itemName,[Validators.required]],
        inventoryItemCategoryId: [this.inventoryItem.inventoryItemCategoryId,[Validators.required]],
        inventoryItemSubCategoryId:[this.inventoryItem.inventoryItemSubCategoryId,[Validators.required]],
        quantityUnitOfMesureId: [this.inventoryItem.quantityUnitOfMesureId,[Validators.required]],
        seasonality: [this.inventoryItem.seasonality,[Validators.required]],
        minimumInventory: [this.inventoryItem.minimumInventory,[Validators.required]],
      
      });

      this.standardInventoryForm.get('inventoryItemCategoryId').valueChanges.subscribe(
        (inventoryItemCategoryId) => {

          if(inventoryItemCategoryId)
          {
              this.selectedInventorySubCategory = this.inventorySubCategory.filter(r=> r.inventoryItemCategoryID==inventoryItemCategoryId);
          }
        }
    );
    
  this.ItemNameInput.focus();

  } 

  save(){
    this.standardInventoryService.getInventoryItemAvailability(this.inventoryItem.itemName, this.inventoryItem.id).subscribe(data => {
      if(data==true){
        alert("This inventory item is already aavailable in the inventory list");
      }  
      else{
        this.saveData();
      }      
    });    
  }
  
  saveData() {

    if(this.file)
    {
    let fileServerDto = new FileServerDto();
    fileServerDto.BucketName = "inventory-items";
    fileServerDto.Key = this.inventoryItem.inventoryItemCategoryId+"/"+this.inventoryItem.itemName+"/"+this.file.name;

    this.standardInventoryService.getFileUploadUrl(fileServerDto)
    .subscribe(result =>{
      this.standardInventoryService.uploadFile(result.url,this.file).subscribe(
        ()=>{
          this.saveStandardInventory(result.id);
        }
      );
    });   
  }
  else{
    this.saveStandardInventory(0);
    } 
  }

  saveStandardInventory(fileId:number): any {

    if(fileId > 0)
        this.inventoryItem.fileID = fileId;

        //Update
        if(this.inventoryItem.id > 0)
        {
   
          this.standardInventoryService.editInventory(this.inventoryItem)
            .subscribe(() =>{
            
              this.dialogRef.close(true);  
              this.snackBar.open('Successfully updated the standard inventory item', '', {
                duration: 2000  
              });
            });
        }
    
        // Insert
    else
     {
      this.standardInventoryService.addInventory(this.inventoryItem)
      .subscribe(() => {

        this.dialogRef.close(true);
        this.snackBar.open('Successfully added a new standard inventory item', '', {
                duration: 2000 
        });
      });
  }
    
  }

         
  onSelectFile(event) {
  if (event.target.files && event.target.files[0]) {
    var reader = new FileReader();

    this.file = event.target.files[0];
    reader.readAsDataURL(event.target.files[0]); // read file as data url

    reader.onload = (event:any) => { // called once readAsDataURL is completed
      this.url = event.target.result;
    }
  }
  }
}
