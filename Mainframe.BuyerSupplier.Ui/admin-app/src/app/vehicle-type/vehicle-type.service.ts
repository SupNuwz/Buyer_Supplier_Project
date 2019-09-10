import { Injectable } from '@angular/core';
import { VehicleTypeDto } from './VehicleTypeDto'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleTypeService {

  url=environment.apiBaseUrl+"VehicleType";

  constructor(private http:HttpClient) { }

  getVehicleType():Observable<VehicleTypeDto[]>
  {
    return this.http.get<VehicleTypeDto[]>(this.url);
  }

  addVehicleType(vehicleType:VehicleTypeDto):Observable<number>{
    return this.http.post<number>(this.url,vehicleType)
  }

   delete(id:number):Observable<VehicleTypeDto[]>
  {
  return this.http.delete<VehicleTypeDto[]>(this.url + '/' + id)
  } 

  editVehicleType(vehicleType:VehicleTypeDto){
    return this.http.put (this.url, vehicleType)
  }

  getVehicleTypeByID(id:number){
    return this.http.get<VehicleTypeDto>(this.url + '/' +id)
  }

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.url + '/' + itemName + '/' + itemID)
  }

}