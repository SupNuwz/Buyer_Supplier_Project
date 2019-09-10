import { Injectable } from '@angular/core';
import { UnitOfMeasureDto } from './UnitOfMeasureDto'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UnitofmeasureService {

  url=environment.apiBaseUrl+"UnitOfMeasure";

  constructor(private http:HttpClient) { }

  getUnitOfMeasure():Observable<UnitOfMeasureDto[]>
  {
    return this.http.get<UnitOfMeasureDto[]>(this.url);
  }

  addUnitOfMeasure(unitOfMeasure:UnitOfMeasureDto):Observable<number>{
    return this.http.post<number>(this.url,unitOfMeasure)
  }

   delete(id:number):Observable<UnitOfMeasureDto[]>
  {
  return this.http.delete<UnitOfMeasureDto[]>(this.url + '/' + id)
  } 

  editUnitOfMeasure(unitOfMeasure:UnitOfMeasureDto){
    return this.http.put (this.url, unitOfMeasure)
  }

  getUnitOfMeasureByID(id:number){
    return this.http.get<UnitOfMeasureDto>(this.url + '/' +id)
  }

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.url + '/' + itemName + '/' + itemID)
  }

}
