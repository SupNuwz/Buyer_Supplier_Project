import { Injectable } from '@angular/core';
import{ VehicleDto } from './VehicleDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  url=environment.apiBaseUrl+"Vehicle";
  updateUrl=environment.apiBaseUrl+"Vehicle/Update";
  
  
  constructor(private http:HttpClient) { }

  getVehicle():Observable<VehicleDto[]>
  {
    return this.http.get<VehicleDto[]>(this.url);
  }

  add(item:VehicleDto):Observable<number>{
    return this.http.post<number>(this.url, item)

  }
  delete(id:number):Observable<VehicleDto[]>
  {
    return this.http.delete<VehicleDto[]>(this.url + '/' + id)

  }
  update(item:VehicleDto){
  return this.http.post(this.updateUrl, item)
  }
}