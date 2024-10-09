import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { EditProfileModel } from './edit-profile.model';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [HttpClientModule, FormsModule],
  providers: [AuthService],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
  @Output() close = new EventEmitter<void>();

  editProfileData : EditProfileModel = {
    name: "",
    email: "",
    phoneNumber: ""
  };

  private authservice = inject(AuthService);

  ngOnInit() : void{
    const data = localStorage.getItem("userProfileData");
    if(data){
      this.editProfileData = JSON.parse(data);
    }
  }

  onEdit(){
    this.authservice.editProfile(this.editProfileData)
        .subscribe(response => {
          if(response){
            localStorage.setItem("userProfileData", JSON.stringify(this.editProfileData));
            this.close.emit();
          }
        })
  }
}
