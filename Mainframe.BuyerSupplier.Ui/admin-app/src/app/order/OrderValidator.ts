import { AbstractControl, FormGroup } from "@angular/forms";
import { OrderDetailDto } from "./OrderDetailDto";


export function OrderDetailsArrayLengthValidation(controlName: string, orderDetailsArray:OrderDetailDto[])  {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];

    if(control.errors && !control.errors.noData){
      return;
    }

    // if(control.value.length==0)
    // {
    //   control.setErrors({noData:true});
    // }
    // else
    //   {
    //     control.setErrors(null);
    //   }
      if(orderDetailsArray.length==0)
      {
        control.setErrors({noData:true});
      }
      else
        {
          control.setErrors(null);
        }

  }
}

export function ValidateCartValue(controlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];   
    
    if(control.errors && !control.errors.zeroQty){
      return;
    }
    
    if(control.value <= 0)
    {
      control.setErrors({zeroQty:true});
    }
    else
    {
      control.setErrors(null);
    }
  }
}