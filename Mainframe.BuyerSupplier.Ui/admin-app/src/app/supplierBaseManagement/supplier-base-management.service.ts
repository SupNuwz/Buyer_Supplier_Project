import { Injectable } from '@angular/core';
import { SupplierBaseDto } from './supplierBaseDto';
import { Observable } from 'rxjs';
import { HttpClient,} from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SupplierBaseManagementService {

  supplierUrl=environment.apiBaseUrl+"supplier";
  deleteUrl=environment.apiBaseUrl+"supplier";
  updateUrl=environment.apiBaseUrl+"supplier/Update";

 
  constructor(private http:HttpClient) { }

  getSuppliers():Observable<SupplierBaseDto[]>
  {
    return this.http.get<SupplierBaseDto[]>(this.supplierUrl);
  }

  add(supplier:SupplierBaseDto):Observable<number>{
    return this.http.post<number>(this.supplierUrl,supplier)

  }
  delete(supplierBaseId:number):Observable<SupplierBaseDto[]>
  {
    return this.http.delete<SupplierBaseDto[]>(this.deleteUrl + '/' + supplierBaseId)

  }
  update(supplier:SupplierBaseDto){
  return this.http.post(this.updateUrl,supplier)
  }

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.supplierUrl + '/' + itemName + '/' + itemID)
  }
}
