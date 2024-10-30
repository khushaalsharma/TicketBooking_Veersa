import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthHelperService } from '../auth/authHelper.service';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  
  isLoggedIn: boolean = false;
  
  constructor(private authHelp : AuthHelperService, private authService : AuthService){}

  ngOnInit(){
    this.isLoggedIn = this.authHelp.isLoggedIn();
  }

  signin(){
    window.location.href = "/login";
  }

  signup(){
    window.location.href = "/register";
  }

  logout(){
    this.authService.logout();
  }

  yourCart(){
    window.location.href = "/cart";
  }
}
