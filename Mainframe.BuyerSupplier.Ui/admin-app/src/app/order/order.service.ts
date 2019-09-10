import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { OrderDto } from './OrderDto ';
import { Observable } from 'rxjs';

import { OrderOptimizedPossibilityDto } from './OrderOptimizedPossibilityDto';
import { OrderPossibilitySelectionListDto } from './OrderPossibilitySelectionListDto';


@Injectable({
  providedIn: 'root'
})
export class OrderService {

  getOrderUrl=environment.apiBaseUrl+"OrderManagement";

  constructor(private http:HttpClient) { }

  addOrder(order:OrderDto):Observable<OrderOptimizedPossibilityDto[]>{
    return this.http.post<OrderOptimizedPossibilityDto[]>(this.getOrderUrl, order);
  }

  getOrders():Observable<OrderDto[]>
  {
    return this.http.get<OrderDto[]>(this.getOrderUrl);
  }

  searchPossibilities(order:OrderDto){
    return this.http.post(this.getOrderUrl + '/SearchPossibilities/', order);
  }

  getOrderById(id:number){
    return this.http.get<OrderDto>(this.getOrderUrl + '/' +id)
  }

  addOrderAssignments(orderPossibilitySelectionListDto:OrderPossibilitySelectionListDto){
    return this.http.post(this.getOrderUrl + '/OrderAssignment/', orderPossibilitySelectionListDto);
  }


  waveManagement(deliverySlot:number):Observable<boolean>{
    return this.http.post<boolean>(this.getOrderUrl + '/WaveManagement/', deliverySlot);
  }
}
