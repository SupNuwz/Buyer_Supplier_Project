import { Injectable } from '@angular/core';
import { WatchListDto } from './WatchListDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WatchListService {

  watchListUrl=environment.apiBaseUrl+"WatchList"
  constructor(private http:HttpClient) { }

  getWatchList(supplierBaseId:number):Observable<WatchListDto[]>
  {
    return this.http.get<WatchListDto[]>(this.watchListUrl + '/' +supplierBaseId);
  }

}
