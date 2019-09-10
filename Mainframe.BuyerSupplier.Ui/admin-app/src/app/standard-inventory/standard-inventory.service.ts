import { Injectable } from '@angular/core';
import{StandardInventoryDto} from './StandardInventoryDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileServerDto } from './FileServerDto';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StandardInventoryService {

  url=environment.apiBaseUrl+"StandardInventory";
  
 
  constructor(private http:HttpClient) { }

  getStandardInventory():Observable<StandardInventoryDto[]>
  {
    return this.http.get<StandardInventoryDto[]>(this.url);
  }

  addInventory(inventory:StandardInventoryDto):Observable<number>{
    return this.http.post<number>(this.url,inventory)
  }

  delete(id:number):Observable<StandardInventoryDto[]>
  {
  return this.http.delete<StandardInventoryDto[]>(this.url + '/' + id)
  }

  editInventory(inventory:StandardInventoryDto){
    return this.http.put (this.url, inventory)
  }

  getInventoryByID(id:number){
    return this.http.get<StandardInventoryDto>(this.url + '/' +id)
  }

  getFileUploadUrl(fileserverDto:FileServerDto):Observable<any>
  {
    return this.http.post<string>(environment.apiBaseUrl+"FileOperation/",fileserverDto)
  }

  uploadFile(url:string, file:any)
  {
    return this.http.put (url, file)
  }  

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.url + '/' + itemName + '/' + itemID)
  }
 

}
