import { Injectable } from '@angular/core';
import { SupplierInventoryDto } from './SupplierInventoryDto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})

export class SupplierInventoryService {

  getSupplierInventoryUrl=environment.apiBaseUrl+"SupplierInventory";

  constructor(private http:HttpClient) { }

  addSupplierInventory(supplierInventory:SupplierInventoryDto):Observable<number>{
    return this.http.post<number>(this.getSupplierInventoryUrl, supplierInventory);
  }

  getSupplierInventories(supplierId:number):Observable<SupplierInventoryDto[]>
  {
    return this.http.get<SupplierInventoryDto[]>(this.getSupplierInventoryUrl + '/' + 'standardInventory'+'/'+ supplierId);
  }
  
  getAllSupplierInventories():Observable<SupplierInventoryDto[]>
  {
    return this.http.get<SupplierInventoryDto[]>(this.getSupplierInventoryUrl);
  }
}
