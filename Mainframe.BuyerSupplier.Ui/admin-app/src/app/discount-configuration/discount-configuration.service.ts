import { Injectable } from '@angular/core';
import { DiscountConfigurationDto } from './discountConfigurationDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DiscountConfigurationService {
  url=environment.apiBaseUrl+"DiscountConfiguration";

  constructor(private http:HttpClient) { }

  getDiscount():Observable<DiscountConfigurationDto[]>
  {
    return this.http.get<DiscountConfigurationDto[]>(this.url);
  }

  add(discount:DiscountConfigurationDto):Observable<number>{
    return this.http.post<number>(this.url, discount)
  }

  delete(id:number):Observable<DiscountConfigurationDto[]>
  {
  return this.http.delete<DiscountConfigurationDto[]>(this.url + '/' + id)
  }

  edit(discount:DiscountConfigurationDto){
    return this.http.put (this.url, discount)
  }
}
