import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient,} from '@angular/common/http';
import { InventoryItemSubCategoryDto } from './InventoryItemSubCategoryDto';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InventoryItemSubCategoryService {

  categoryUrl=environment.apiBaseUrl+"InventoryItemSubCategory";
  deleteUrl=environment.apiBaseUrl+"InventoryItemSubCategory";
  updateUrl=environment.apiBaseUrl+"InventoryItemSubCategory/Update";

 
  constructor(private http:HttpClient) { }

  getNewCategory():Observable<InventoryItemSubCategoryDto[]>
  {
    return this.http.get<InventoryItemSubCategoryDto[]>(this.categoryUrl);
  }

  add(category:InventoryItemSubCategoryDto):Observable<number>{
    return this.http.post<number>(this.categoryUrl,category)

  }
  delete(id:number):Observable<InventoryItemSubCategoryDto[]>
  {
    return this.http.delete<InventoryItemSubCategoryDto[]>(this.deleteUrl + '/' + id)

  }
  update(category:InventoryItemSubCategoryDto){
  return this.http.post(this.updateUrl,category)
  }
}
