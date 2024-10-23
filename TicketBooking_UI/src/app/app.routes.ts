import { Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ProfilepageComponent } from './auth/profilepage/profilepage.component';
import { TicketsComponent } from './tickets/tickets.component';
import { authGuard } from './auth.guard';
import { SpecificEventComponent } from './events/specific-event/specific-event.component';

export const routes: Routes = [
  {
    path: "",
    component: HomepageComponent
  },
  {
    path: "profile",
    component: ProfilepageComponent,
    canActivate: [authGuard]
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
    path: "event/:id",
    component: SpecificEventComponent,
    canActivate: [authGuard]
  },
  {
    path: "home",
    component: HomepageComponent,
    canActivate: [authGuard]
  },
  {
    path: "bookings",
    component: TicketsComponent,
    canActivate: [authGuard]
  }
];
