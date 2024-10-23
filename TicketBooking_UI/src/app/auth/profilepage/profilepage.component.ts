import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar.component";
import { EditProfileComponent } from "../edit-profile/edit-profile.component";
import { ChangePassComponent } from "../change-pass/change-pass.component";
import { AuthService } from '../auth.service';
import { ProfilePageModel } from './profielpage.model';
import { ChangeProfilePicComponent } from '../change-profile-pic/change-profile-pic.component';


@Component({
  selector: 'app-profilepage',
  standalone: true,
  imports: [ NavbarComponent, EditProfileComponent, ChangePassComponent, ChangeProfilePicComponent],
  providers: [AuthService],
  templateUrl: './profilepage.component.html',
  styleUrl: './profilepage.component.css'
})
export class ProfilepageComponent {

  editprofile : boolean = false;
  changePswd : boolean = false;
  changePhoto : boolean = false;

  userData : ProfilePageModel = {
    name: "",
    email: "",
    phoneNumber: "",
    preferredCurr: "",
    preferredLang: "",
    url: ""
  }

  constructor(private authservice: AuthService){}

  setUserData(){
    const data = localStorage.getItem("userProfileData");
    if(data){
      this.userData = JSON.parse(data);
    }
  }

  ngOnInit(): void {
    this.authservice.getUser()
        .subscribe(response => {
          //console.log(response);
          this.userData = response;
          localStorage.setItem("userProfileData", JSON.stringify(response));
        });
    
    this.setUserData();
  }

  editProfile(){
    this.editprofile = !this.editprofile;
    this.setUserData();
  }

  changePassword(){
    this.changePswd = !this.changePswd;
  }

  changeProfilePhoto(){
    this.changePhoto = !this.changePhoto;
    this.setUserData();
  }
}
