import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-get-user',
  templateUrl: './get-user.component.html',
  styleUrls: ['./get-user.component.css']
})
export class GetUserComponent implements OnInit {

  userName : string;
  userClaims: any;
  userData : any;
  sub: any;

  constructor(private userService : UserService,private route : ActivatedRoute, private toastr : ToastrService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params =>{
      this.userName = params['userName'];

    });
      this.getUserDetails();
  }

  getUserDetails(){
    this.userService.getUserInfo(this.userName)
    .subscribe(
      (data : any) => {
        if(data != null)
        {
          this.userData = data;     
        }
        else
        {
          this.toastr.error(data.Errors[0]);
        }

      }
    );
  }

}
