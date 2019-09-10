import { Injectable } from '@angular/core';
import { InventoryItemCategoriesDto } from './InventoryItemCategoriesDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InventoryItemCategoriesService {
  url=environment.apiBaseUrl+"InventoryItemCategories";

  constructor(private http:HttpClient) { }

  getInventoryItemCategory():Observable<InventoryItemCategoriesDto[]>
  {
    return this.http.get<InventoryItemCategoriesDto[]>(this.url);
  }

  addInventoryItemCategory(category:InventoryItemCategoriesDto):Observable<number>{
    return this.http.post<number>(this.url, category)
  }

  deleteInventoryItemCategory(id:number):Observable<InventoryItemCategoriesDto[]>
  {
  return this.http.delete<InventoryItemCategoriesDto[]>(this.url + '/' + id)
  }

  editInventoryItemCategory(category:InventoryItemCategoriesDto){
    return this.http.put (this.url, category)
  }

  InventoryItemCategoryById(id:number){
    return this.http.get<InventoryItemCategoriesDto>(this.url + '/' +id)
  }

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.url + '/' + itemName + '/' + itemID)
  }
}
