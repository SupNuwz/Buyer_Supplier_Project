import { Injectable } from '@angular/core';
import{ ZoneDto } from './ZoneDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ZoneService {
  url=environment.apiBaseUrl+"Zone";

  constructor(private http:HttpClient) { }

  getZone():Observable<ZoneDto[]>
  {
    return this.http.get<ZoneDto[]>(this.url);
  }

  add(zone:ZoneDto):Observable<number>{
    return this.http.post<number>(this.url, zone)
  }

  delete(id:number):Observable<ZoneDto[]>
  {
  return this.http.delete<ZoneDto[]>(this.url + '/' + id)
  }

  edit(zone:ZoneDto){
    return this.http.put (this.url, zone)
  }
}
