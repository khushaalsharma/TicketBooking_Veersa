import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormsModule } from '@angular/forms';
import { PhotoModel } from './photo.model';
@Component({
  selector: 'app-change-profile-pic',
  standalone: true,
  imports: [FormsModule],
  providers: [AuthService],
  templateUrl: './change-profile-pic.component.html',
  styleUrl: './change-profile-pic.component.css'
})
export class ChangeProfilePicComponent {
  @Output() close = new EventEmitter<void>();
  ogUrl !: string;
  newPhoto !: File;
  UploadedPhoto !: PhotoModel;

  constructor(private authService: AuthService){}

  ngOnInit(): void{
    var data = localStorage.getItem("userProfileData");
    if(data){
      var userData = JSON.parse(data);
      this.ogUrl = userData.url;
    }
  }

  setNewProfilePhoto(){
    var data = localStorage.getItem("userProfileData");
    if(data){
      var userData = JSON.parse(data);
      userData.url = this.UploadedPhoto.url;
      localStorage.setItem("userProfileData", JSON.stringify(userData));
    }
  }

  onFileSelected(event: any){
    if(event.target.files.length > 0){
      this.newPhoto = event.target.files[0];
    }
  }

  onChange(){
    var formdata = new FormData();

    formdata.append("ProfilePhoto", this.newPhoto);
    this.authService.changePhoto(formdata).subscribe(
      (response) => {
        this.UploadedPhoto = response;
        this.setNewProfilePhoto();
        this.close.emit();
      },
      (error) => {
        console.log(error);
        alert("Can't change photo. Try again later");
        this.close.emit();
      }
    )
  }
}
