import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { RegisterModel } from './register.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ FormsModule],
  providers:[AuthService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  enteredName = "";
  enteredEmail = "";
  enteredPhone = "";
  enteredPassword = "";
  confimPassword = "";

  constructor(private authservice: AuthService){}

  checkPassword(){
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if(passwordRegex.test(this.enteredPassword)){
      return true;
    }

    return false;
  }

  onRegister(){
    if(!this.checkPassword()){
      alert("Password must be 8 characters long and contain an uppercase character, a lowercase character, a digit and a special character.")
    }
    else if(this.enteredPassword !== this.confimPassword){
      alert("Password doesn't match");
    }
    else{
      const regData : RegisterModel = {
        name: this.enteredName,
        email: this.enteredEmail,
        phoneNumber: this.enteredPhone,
        password: this.enteredPassword  
      };

      this.authservice.registerUser(regData)
          .subscribe((response) => {
            console.log(response);
            if(response){
              alert("User Created, Proceed to Log In");
              window.location.href = "/login";
            }
            else{
              alert("Error creating user");
            }
          }, (error) => {
            alert(error.error.message);
            window.location.href = "/login";
          });
    }
  }
}
