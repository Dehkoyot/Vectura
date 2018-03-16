import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { AuthGuard } from './auth/auth.guard';
import { GoogleMapComponent } from './google-map/google-map.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { GetUserComponent } from './get-user/get-user.component';

export const appRoutes: Routes =[
    { path : 'home', component : UserProfileComponent, canActivate: [AuthGuard]},
    { 
        path : 'signup', component: UserComponent,
        children: [{path: '', component: SignUpComponent}]
    },
    { 
        path : 'login', component: UserComponent,
        children: [{path: '', component: SignInComponent}]
    },
    {
        path: 'home/map', component: GoogleMapComponent,canActivate:[AuthGuard]

    },
    {
        path : 'home/all-users', component : AllUsersComponent, canActivate:[AuthGuard]
    },
    {
        path : 'home/user/:userName', component : GetUserComponent, canActivate : [AuthGuard]
    },
    { 
        path : '', redirectTo:'/login', pathMatch: 'full'
    }

];