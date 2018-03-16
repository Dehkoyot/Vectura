import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {

  users : any;
  userName : string;

  constructor( private toastr : ToastrService, private router : Router, private userService : UserService
  ) { }

  ngOnInit() {

    this.userService.getUserClaims().subscribe(
      (data : any) => {
        this.userName = data.UserName;
      }
    );

      this.getUsers();
  }

  getUsers(){

    this.userService.getAllUsers()
    .subscribe(
      (data : any)=>{
        if(data != null)
        {
          this.users = data;
        }
        else
        {
          this.toastr.error(data.Errors[0]);
        }
      }
    );
  }

    RedirectToUser(userName : string){
      this.router.navigate(['/home/user/' + userName]);
    }
  

}
