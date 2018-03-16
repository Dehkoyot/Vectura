import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  userClaims: any;

  constructor(private router : Router,private userService: UserService) { }

  ngOnInit() {
    this.userService.getUserClaims().subscribe((data:any)=>{
      this.userClaims=data;
    });
  }

  Home(){
    this.router.navigate(['/home']);
  }

   Map(){
    this.router.navigate(['/home/map']);
  }


  GetMyTravels(){
    this.router.navigate(['/home/mytravels']);
  }

  GetAllUsers(){
    this.router.navigate(['/home/all-users']);
  }


  Logout(){
    localStorage.removeItem('userToken');
    this.router.navigate(['/login']);
  }

}
