import { Injectable } from '@angular/core';
import { DeliverySlotsDto } from './DeliverySlotsDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeliverySlotsService {

  getDeliverySlotsUrl=environment.apiBaseUrl+"DeliverySlots";

  constructor(private http:HttpClient) { }

  getDeliverySlot():Observable<DeliverySlotsDto[]>
  {
    return this.http.get<DeliverySlotsDto[]>(this.getDeliverySlotsUrl);
  }

  addDeliverySlot(slot:DeliverySlotsDto):Observable<number>{
    return this.http.post<number>(this.getDeliverySlotsUrl, slot)
  }

  deleteDeliverySlot(id:number){
    return this.http.delete<DeliverySlotsDto[]>(this.getDeliverySlotsUrl + '/' + id)
  }

  editDeliverySlot(slot:DeliverySlotsDto){
    return this.http.put(this.getDeliverySlotsUrl, slot)
  }

  getDeliverySlotById(id: number){
    return this.http.get<DeliverySlotsDto>(this.getDeliverySlotsUrl + '/' + id)
  }
  
  getInventoryItemAvailability(itemName: string, itemID: number)
    {
      return this.http.get<Boolean>(this.getDeliverySlotsUrl + '/' + itemName + '/' + itemID)
    }
}
