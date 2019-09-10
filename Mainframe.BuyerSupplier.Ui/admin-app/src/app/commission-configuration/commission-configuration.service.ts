import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import{ CommissionConfigurationDto } from './commissionConfigurationDto'

@Injectable({
  providedIn: 'root'
})
export class CommissionConfigurationService {
  url=environment.apiBaseUrl+"CommissionConfiguration";


  constructor(private http:HttpClient) { }

  getCommission():Observable<CommissionConfigurationDto[]>
  {
    return this.http.get<CommissionConfigurationDto[]>(this.url);
  }

  add(commission:CommissionConfigurationDto):Observable<number>{
    return this.http.post<number>(this.url, commission)
  }

  delete(id:number):Observable<CommissionConfigurationDto[]>
  {
  return this.http.delete<CommissionConfigurationDto[]>(this.url + '/' + id)
  }

  edit(commission:CommissionConfigurationDto){
    return this.http.put (this.url, commission)
  }
}
