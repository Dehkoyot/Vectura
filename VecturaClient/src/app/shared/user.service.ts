import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {  Response } from "@angular/http";
import {Observable} from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from './user.model';
import { headersToString } from 'selenium-webdriver/http';


@Injectable()
export class UserService {
  readonly rootUrl ='http://localhost:34167';
  constructor(private http: HttpClient) { }

  registerUser(user : User)
  {
    const body: User = {
      UserName: user.UserName,
      Password: user.Password,
      Email: user.Email,
      FirstName: user.FirstName,
      LastName: user.LastName,
      Town: user.Town
    }
    var reqHeader = new HttpHeaders({'No-Auth':'True'});
    return this.http.post(this.rootUrl + '/api/User/Register', body, {headers:reqHeader});
  }

  userAuthentification(userName, password){
    var data = "username=" + userName + "&password=" + password+"&grant_type=password"
    var reqHeader=new HttpHeaders({'Content-Type':'application/x-www-urlencoded','No-Auth':'True'});
    return this.http.post(this.rootUrl+'/token',data,{headers: reqHeader});

  }

  getUserClaims(){
    return this.http.get(this.rootUrl + '/api/GetUserClaims');
  }

  getAllUsers(){
    return this.http.get(this.rootUrl+'/api/GetAllUsersNames');
  }

  userAuthentication(userName,password){
    var data = "username="+userName+"&password="+password+"&grant_type=password";
    var requestHeader = new HttpHeaders({"Content-Type" : 'application/x-www-urlencoded', "No-Auth" : "True"});
    return this.http.post(this.rootUrl+'/token', data, {headers: requestHeader});
  }

  getUserInfo(userName : string){
    return this.http.get(this.rootUrl+'/api/GetUserInfo?userName='+userName);
  }

  getUserVisits(userName : string){
    return this.http.get(this.rootUrl+'/api/GetUserVisits?userName='+userName);
  }

}
