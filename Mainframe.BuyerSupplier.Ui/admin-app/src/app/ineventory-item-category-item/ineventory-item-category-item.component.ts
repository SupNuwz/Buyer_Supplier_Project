import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { InventoryItemCategoriesService } from '../inventory-item-categories/inventory-item-categories.service'
import { InventoryItemCategoriesDto } from '../inventory-item-categories/InventoryItemCategoriesDto';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, MatInput } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';//

@Component({
  selector: 'app-ineventory-item-category-item',
  templateUrl: './ineventory-item-category-item.component.html',
  styleUrls: ['./ineventory-item-category-item.component.css']
})

export class IneventoryItemCategoryItemComponent implements OnInit {
  categoryItem : InventoryItemCategoriesDto;
  header:string;
  categoryForm: FormGroup;          //
 

  validationMessages = {                                                   //from thiss line to
     'name': [{ type: 'required', message: 'Category Name is required' }],
     'description': [{ type: 'required', message: 'Category Description is required' }],
  };                                                                        //this line

  constructor(private router:Router, private categoryService:InventoryItemCategoriesService,
               public dialogRef: MatDialogRef<IneventoryItemCategoryItemComponent>,
               @Inject(MAT_DIALOG_DATA) public data: InventoryItemCategoriesDto, 
               public snackBar: MatSnackBar, private formBuilder: FormBuilder/***/) 
  {

    if(data)
    {
      this.categoryItem = data;
      this.header= "Edit an Inventory Category Item";
    }
    else{
      this.categoryItem = new InventoryItemCategoriesDto();
      this.header= "Add an Inventory Category Item";
    }
  }
 
  @ViewChild('categoryName') categoryNameInput: MatInput;

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({                       
      name: [this.categoryItem.name, [Validators.required]],
      description: [this.categoryItem.description, [Validators.required]],
      });

    this.categoryNameInput.focus();  
  }

  save(){
    this.categoryService.getInventoryItemAvailability(this.categoryItem.name, this.categoryItem.id).subscribe(data => {
      if(data==true){
        alert("This inventory item category item is already available in the inventory item category list");
      }  
      else{
        this.saveData();
      }      
    });    
  }
  
  saveData() {
    //Update
    if(this.categoryItem.id> 0)
    {
      this.categoryService.editInventoryItemCategory(this.categoryItem)
        .subscribe(() => {
            this.dialogRef.close(true);  
            this.snackBar.open('Successfully updated the inventory item category ', '', {
              duration: 2000
            });
        });
    }

    // Insert
    else{
      this.categoryService.addInventoryItemCategory(this.categoryItem)
        .subscribe(() => {
            this.dialogRef.close(true);    
            this.snackBar.open('Successfully added a new inventory item category ', '', {
              duration: 2000    
            });
        });
    }
  }
}

