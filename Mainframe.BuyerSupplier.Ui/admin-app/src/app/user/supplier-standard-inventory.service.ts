import { Injectable } from '@angular/core';
import{SupplierStandardInventoryDto} from './SupplierStandardInventoryDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SupplierStandardInventoryService {
  url=environment.apiBaseUrl+"SupplierStandardInventory";

  constructor(private http:HttpClient) { }

  getSupplierStandardInventoryBySupplier(supplierId:number):Observable<SupplierStandardInventoryDto[]>
  {
    return this.http.get<SupplierStandardInventoryDto[]>(this.url + '/' + 'user'+'/'+ supplierId);
  }

  getSelectedSupplierStandardInventoryBySupplier(supplierId:number):Observable<SupplierStandardInventoryDto[]>
  {
    return this.http.get<SupplierStandardInventoryDto[]>(this.url + '/' + 'supplierwiseselected'+'/'+ supplierId);
  }

  addSupplierStandardInventory(inventory:SupplierStandardInventoryDto[]):Observable<number>{
    return this.http.post<number>(this.url,inventory)
  }

  editSupplierStandardInventory(inventory:SupplierStandardInventoryDto[]):Observable<number>{
    return this.http.put<number> (this.url, inventory)
  }

  deleteSupplierStandardInventory(supplierId:number,id:number):Observable<SupplierStandardInventoryDto[]>
  {
  return this.http.delete<SupplierStandardInventoryDto[]>(this.url + '/supplier' + '/' + supplierId + id)
  }


}