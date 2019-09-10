import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DeliveryCostConfigurationDto } from './DeliveryCostConfigurationDto';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DeliveryCostConfigurationService {

  url=environment.apiBaseUrl+"DeliveryCostConfiguration";
  updateUrl=environment.apiBaseUrl+"DeliveryCostConfiguration/Update";
  
  
  constructor(private http:HttpClient) { }

  getDeliveryCostConfigurations():Observable<DeliveryCostConfigurationDto[]>
  {
    return this.http.get<DeliveryCostConfigurationDto[]>(this.url);
  }

  add(item:DeliveryCostConfigurationDto):Observable<number>{
    return this.http.post<number>(this.url, item)

  }
  delete(id:number):Observable<DeliveryCostConfigurationDto[]>
  {
    return this.http.delete<DeliveryCostConfigurationDto[]>(this.url + '/' + id)

  }
  update(item:DeliveryCostConfigurationDto){
  return this.http.post(this.updateUrl, item)
  }
}