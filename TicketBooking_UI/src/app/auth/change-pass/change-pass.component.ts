import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { ChangePasswordModel } from './change-pass.model';

@Component({
  selector: 'app-change-pass',
  standalone: true,
  imports: [ FormsModule],
  providers: [AuthService],
  templateUrl: './change-pass.component.html',
  styleUrl: './change-pass.component.css'
})
export class ChangePassComponent {
  @Output() close = new EventEmitter<void>();

  passwordData : ChangePasswordModel = {
    oldPassword: "",
    newPassword: ""
  };

  constructor(private authservice : AuthService){}

  onChange(){
    this.authservice.changePass(this.passwordData)
        .subscribe(response => {
          if(response){
            console.log(response);
            alert("Password Changed successfully");
            this.close.emit();
          }
          else{
            alert("Try again");
          }
        })
  }
}
