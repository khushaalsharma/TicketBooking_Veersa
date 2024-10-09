import { Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ProfilepageComponent } from './auth/profilepage/profilepage.component';
import { TicketsComponent } from './tickets/tickets.component';
import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
  {
    path: "",
    component: LoginComponent
  },
  {
    path: "profile",
    component: ProfilepageComponent,
    //HcanActivate: [AuthGuard]
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "register",
    component: RegisterComponent
  },
  {
    path: "home",
    component: HomepageComponent,
    //canActivate: [AuthGuard]
  },
  {
    path: "bookings",
    component: TicketsComponent,
    //canActivate: [AuthGuard]
  }
];
