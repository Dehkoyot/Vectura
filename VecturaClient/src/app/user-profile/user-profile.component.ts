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

  DeleteUser(){
    
    if(window.confirm("Are you sure to delete your account ?"))
    {
        this.userService.deleteUser(this.userClaims.UserName)
      .subscribe(
        (data : any)=>{
          if(data == true)
          {
            localStorage.removeItem("userToken");
            this.router.navigate(['/login']);
          }
          else
          {
            this.toastr.error(data.Errors[0]);
          }
        }
      );
  }
}

}
