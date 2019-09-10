import { Component, OnInit,Inject} from '@angular/core';
import { InventoryItemSubCategoryDto } from '../inventory-item-sub-category/InventoryItemSubCategoryDto';
import { FormControl, Validators,FormBuilder, FormGroup  } from '@angular/forms';
import { Router } from '@angular/router';
import { InventoryItemCategoriesService } from '../inventory-item-categories/inventory-item-categories.service';
import {MatDialogRef,MatDialog,MAT_DIALOG_DATA, MatSnackBar} from '@angular/material';
import {InventoryItemSubCategoryService} from '../inventory-item-sub-category/inventory-item-sub-category.service';
import { InventoryItemCategoriesDto } from '../inventory-item-categories/InventoryItemCategoriesDto';

@Component({
  selector: 'app-inventory-item-sub-category-item',
  templateUrl: './inventory-item-sub-category-item.component.html',
  styleUrls: ['./inventory-item-sub-category-item.component.css']
})
export class InventoryItemSubCategoryItemComponent implements OnInit {

  inventoryItemSubCategoryItem:InventoryItemSubCategoryDto;
  header:string;
  inventoryItemCategoryList:InventoryItemCategoriesDto[];
  formControl = new FormControl('', [
    Validators.required,
  ]);

  inventoryItem:FormGroup;

  
  validation_messages = {

    'inventoryItemCategoryID': [{ type: 'required', message: 'Inventory Item Category Name is required' }],
    
    'name': [{ type: 'required', message: 'Name is required' }],

    'description': [{ type: 'required', message: 'Description is required' }],
    
    };


     constructor(private inventoryItemSubCategory: InventoryItemSubCategoryService,private inventoryItemCategories:InventoryItemCategoriesService ,private router:Router, 
      public dialogRef: MatDialogRef<InventoryItemSubCategoryItemComponent>,@Inject(MAT_DIALOG_DATA) public data: InventoryItemSubCategoryDto,
       public snackBar: MatSnackBar, private formBuilder: FormBuilder) { 
  
    if(data)
    {
      this.inventoryItemSubCategoryItem = data;
      this.header="Edit Inventory Item Sub Category";
    }
      else{
        this.inventoryItemSubCategoryItem = new InventoryItemSubCategoryDto();
        this.header="Add an item to Inventory Item Sub Category";
      }
    }
   
  ngOnInit() {

    this.inventoryItem = this.formBuilder.group({

      inventoryItemCategoryID: [this.inventoryItemSubCategoryItem.inventoryItemCategoryID,[Validators.required]],
      
      name: [this.inventoryItemSubCategoryItem.name,[Validators.required]],

      description: [this.inventoryItemSubCategoryItem.description,[Validators.required]],
      
      });

    this.inventoryItemCategories.getInventoryItemCategory().subscribe(data =>
      {
        this.inventoryItemCategoryList = data;
      });

    }
  
    save() {
  
      //update 
      if(this.inventoryItemSubCategoryItem.id > 0)
      {
        this.inventoryItemSubCategory.update(this.inventoryItemSubCategoryItem).subscribe(() => {
         this.dialogRef.close(true);
         this.snackBar.open('Successfully updated the inventory item sub category', '', {
          duration: 2000
        });
        });
  
      } 
      //insert
      else {
        this.inventoryItemSubCategory.add(this.inventoryItemSubCategoryItem).subscribe(() => {
          this.dialogRef.close(true);
          this.snackBar.open('Successfully added a new inventory item sub category', '', {
            duration: 3000
          });
         });
      }  
   }
}
