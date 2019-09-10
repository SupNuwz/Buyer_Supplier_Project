import { Injectable } from '@angular/core';
import { TimeWavesDto } from './TimeWavesDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TimeWavesService {

  url=environment.apiBaseUrl+"TimeWaves";
 
  constructor(private http:HttpClient) { }

  getTimeWaves():Observable<TimeWavesDto[]>
  {
    return this.http.get<TimeWavesDto[]>(this.url);
  }

  addTimeWaves(wave:TimeWavesDto):Observable<number>{
    return this.http.post<number>(this.url,wave)
  }

  delete(id:number):Observable<TimeWavesDto[]>
  {
  return this.http.delete<TimeWavesDto[]>(this.url + '/' + id)
  }

  editWaves(wave:TimeWavesDto){
    return this.http.put (this.url, wave)
  }

  getWavesByID(id:number){
    return this.http.get<TimeWavesDto>(this.url + '/' +id)
  }

  getInventoryItemAvailability(itemName: string, itemID: number)
  {
    return this.http.get<Boolean>(this.url + '/' + itemName + '/' + itemID)
  }
}