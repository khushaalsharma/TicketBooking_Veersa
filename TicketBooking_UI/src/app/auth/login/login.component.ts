import { Component, inject } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { LoginRequestModel } from './loginReq.model';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [  FormsModule, NavbarComponent],
  providers: [AuthService],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] // Fixed this line
})
export class LoginComponent {
  enteredEmail = "";
  enteredPassword = "";

  constructor(private authservice : AuthService){}

  onSubmit(){
    const loginData : LoginRequestModel = {
      username: this.enteredEmail,
      password: this.enteredPassword
    };

    this.authservice.login(loginData)
        .subscribe(response => {
          if(response){
            console.log(response);
            localStorage.setItem('Token', response.jwTtoken);
            alert("Log In successful");
            window.location.href = "/home";
          }
          else{
            alert("Error in logging in");
          }
        });
  }
}
