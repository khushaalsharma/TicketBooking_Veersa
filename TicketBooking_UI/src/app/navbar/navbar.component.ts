import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthHelperService } from '../auth/authHelper.service';
import { AuthService } from '../auth/auth.service';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(private authHelp : AuthHelperService, private authService : AuthService){}

  checkLogIn(){
    return this.authHelp.isLoggedIn();
  }

  signin(){
    window.location.href = "/login";
  }

  signup(){
    window.location.href = "/login";
  }

  logout(){
    this.authService.logout();
  }
}
