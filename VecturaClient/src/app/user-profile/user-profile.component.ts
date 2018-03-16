import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  userClaims : any;

  constructor(    private toastr : ToastrService, private userService : UserService,private router : Router) {   
   }

  ngOnInit() {
    this.userService.getUserClaims().subscribe(
      (data : any) => {
        this.userClaims = data;
      }
    );
  }


}
