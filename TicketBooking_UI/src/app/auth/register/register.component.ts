import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';


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
  preferredCurr = "INR";
  preferredLang = "Hindi";
  profileImage !: File;

  constructor(private authservice: AuthService){}

  checkPassword(){
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if(passwordRegex.test(this.enteredPassword)){
      return true;
    }

    return false;
  }

  onFileSelected(event: any){
    if(event.target.files.length > 0){
      this.profileImage = event.target.files[0];
    }
  }

  onRegister(){
    if(!this.checkPassword()){
      alert("Password must be 8 characters long and contain an uppercase character, a lowercase character, a digit and a special character.")
    }
    else if(this.enteredPassword !== this.confimPassword){
      alert("Password doesn't match");
    }
    else{
      const formData = new FormData();

      // Append all the fields
      formData.append('Name', this.enteredName);
      formData.append('Email', this.enteredEmail);
      formData.append('PhoneNumber', this.enteredPhone);
      formData.append('Password', this.enteredPassword);
      formData.append('PreferredCurr', this.preferredCurr);
      formData.append('PreferredLang', this.preferredLang);

      // Append the profile image (file)
      if (this.profileImage) {
        formData.append('ProfileImage', this.profileImage);
      }


      //console.log(this.profileImage);

      this.authservice.registerUser(formData)
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
            console.log(error);
            if(error.status === 400){
              if(error.error.message === "User exists with email. Please proceed to Login"){
                alert("User exists! Proceed to Login");
                window.location.href = "/login";
              }
              else{
                alert("Error in form data");
              }
            }
            else if(error.status === 0){
              alert("Server not available. Try again later!");
            }
          });
    }
  }
}
