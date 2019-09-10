import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { StandardInventoryDto } from 'src/app/standard-inventory/StandardInventoryDto';
import { ValidateCartValue } from '../OrderValidator';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css']
})
export class CartItemComponent implements OnInit {
  @Input() standardInventoryItem:StandardInventoryDto;
  @Output() AddProduct = new EventEmitter();
  @Output() RemoveProduct = new EventEmitter();

  cartItemGroup:FormGroup;

  validation_messages = {
    'qty': [
      { type: 'required', message: 'Quantity is required' },
      { type: 'zeroQty', message:'Quantity cannot be zero or negative'}
    ]
  };

  constructor(private formBuilder: FormBuilder ) { }

  ngOnInit() {
    this.cartItemGroup = this.formBuilder.group(
      {
        id:[this.standardInventoryItem.id, ],
        itemName:[this.standardInventoryItem.itemName, ],
       // group:[this.standardInventoryItem.group, ],
        quantityUnitOfMesureId:[this.standardInventoryItem.quantityUnitOfMesureId, ],
        quantityUnitOfMeasureName:[this.standardInventoryItem.quantityUnitOfMeasureName, ],
        seasonality:[this.standardInventoryItem.seasonality, ],
        minimumInventory:[this.standardInventoryItem.minimumInventory, ],
        //subGroup:[this.standardInventoryItem.subGroup, ],
        fileID :[this.standardInventoryItem.fileID, ],
        fileUrl:[this.standardInventoryItem.fileUrl, ],
        added:[this.standardInventoryItem.added, ],
        qty: [this.standardInventoryItem.qty,[Validators.required]]
      },
      {
        validator: ValidateCartValue('qty')
      }
    );

    //this.onChanges();
  }

//   onChanges() {
//     this.cartItemGroup.get('added').valueChanges
//     .subscribe(addedStatus => {
//         if (addedStatus) {
//             this.cartItemGroup.get('qty').disable();
//         }
//         else {
//             this.cartItemGroup.get('qty').enable();
//         }
//     });
// }

DissableQty(addedStatus:boolean){
  if (addedStatus) {
    this.cartItemGroup.get('qty').disable();
}
else {    
      this.cartItemGroup.get('qty').enable();
}
}
  AddProductClick()
  {
    this.standardInventoryItem= Object.assign({}, this.cartItemGroup.value)
    this.AddProduct.emit(this.standardInventoryItem);
    this.DissableQty(true);
  }

  RemoveProductClick(){
    this.RemoveProduct.emit(this.standardInventoryItem);
    this.DissableQty(false);
  }

}
