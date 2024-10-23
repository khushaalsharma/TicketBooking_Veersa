import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { EditProfileModel } from './edit-profile.model';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [ FormsModule],
  providers: [AuthService],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
  @Output() close = new EventEmitter<void>();
  editProfileData : EditProfileModel = {
    name: "",
    phoneNumber: "",
    preferredLang: "",
    preferredCurr: ""
  };

  private authservice = inject(AuthService);

  ngOnInit() : void{ //fetching user data from localstorage
    const data = localStorage.getItem("userProfileData");
    if(data){
      this.editProfileData = JSON.parse(data);
      //this.oldEmail = this.editProfileData.email;
    }
  }

  onEdit(){
    this.authservice.editProfile(this.editProfileData)
        .subscribe((response) => {
          console.log(response);
          if(response){
            localStorage.setItem("userProfileData", JSON.stringify(this.editProfileData));
            this.close.emit();
          }
        }, (error) => {
          alert("Credentials already taken!");
          this.close.emit();
        })
  }
}
