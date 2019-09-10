
import { Injectable } from '@angular/core';
import{UserDto} from './UserDto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserCreationInitialDataDto } from './UserCreationInitialDataDto';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url=environment.apiBaseUrl+"Users";

  constructor(private http:HttpClient) { }

  getuser():Observable<UserDto[]>
  {
    return this.http.get<UserDto[]>(this.url);
  }
  
  getInitialUserData(userId:number):Observable<UserCreationInitialDataDto>
  {
    return this.http.get<UserCreationInitialDataDto>(this.url+"/initialuserdata/"+userId);
  }

  addUser(user:UserDto):Observable<number>{
    return this.http.post<number>(this.url,user)
  }

  editUser(user:UserDto):Observable<number>{
    return this.http.put<number> (this.url, user)
  }

  deleteUser(id:number):Observable<UserDto[]>
  {
  return this.http.delete<UserDto[]>(this.url + '/' + id)
  }

  getUserByType(userType:string):Observable<UserDto[]>
  {
    return this.http.get<UserDto[]>(this.url+"/userType/"+userType);
  }
}