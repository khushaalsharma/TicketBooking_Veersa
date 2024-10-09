import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from "../../navbar/navbar.component";
import { EditProfileComponent } from "../edit-profile/edit-profile.component";
import { ChangePassComponent } from "../change-pass/change-pass.component";
import { AuthService } from '../auth.service';
import { ProfilePageModel } from './profielpage.model';

@Component({
  selector: 'app-profilepage',
  standalone: true,
  imports: [HttpClientModule, NavbarComponent, EditProfileComponent, ChangePassComponent],
  providers: [AuthService],
  templateUrl: './profilepage.component.html',
  styleUrl: './profilepage.component.css'
})
export class ProfilepageComponent {

  editprofile : boolean = false;
  changePswd : boolean = false;

  userData : ProfilePageModel = {
    name: "",
    email: "",
    phoneNumber: "",
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
}
