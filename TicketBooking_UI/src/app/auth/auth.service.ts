import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";

//Models
import { LoginRespModel } from "./login/loginResp.model";
import { LoginRequestModel } from "./login/loginReq.model";
import { RegisterModel } from "./register/register.model";
import { ProfilePageModel } from "./profilepage/profielpage.model";
import { EditProfileModel } from "./edit-profile/edit-profile.model";
import { ChangePasswordModel } from "./change-pass/change-pass.model";
import { AuthHelperService } from "./authHelper.service";

@Injectable({
    providedIn: 'root'
})
export class AuthService{

    private loginApi = "https://localhost:7254/api/Auth/Login";
    private registerApi = "https://localhost:7254/api/Auth/Register";
    private profileApi = "https://localhost:7254/api/user/UserProfile";
    private editProfileApi = "https://localhost:7254/api/user/updateUser";
    private changePswdApi = "https://localhost:7254/api/user/changePassword";

    constructor(private http: HttpClient, private authHelp : AuthHelperService){}

    //get the session token
    private getToken() : string | null{
        const token = localStorage.getItem("Token");
        return token;
    }

    //login method
    login(loginData : LoginRequestModel): Observable<LoginRespModel>{
        const headers = new HttpHeaders({
        'Content-Type': 'application/json'
        });
        return this.http.post<LoginRespModel>(this.loginApi, loginData, {
            headers,
            withCredentials: true
        });
    }

    //register method
    registerUser(regData: RegisterModel) : Observable<any>{
        const headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        return this.http.post<any>(this.registerApi, regData, {
            headers,
            withCredentials: true
        });
    }

    //get user data
    getUser() : Observable<ProfilePageModel>{
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.getToken()}`
        })
        return this.http.get<ProfilePageModel>(this.profileApi, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any): Observable<any> => {
                console.log("Error fetching user's profile:", error);
                throw error;
            })
        )
    }

    //change user data
    editProfile(profileData: EditProfileModel): Observable<any>{
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${this.getToken()}`
        });

        return this.http.put(this.editProfileApi, profileData, {
            headers,
            withCredentials: true
        });
    }

    //change password
    changePass(password: ChangePasswordModel) : Observable<any>{
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.getToken()}`,
            'Content-Type': 'application/json'
        });

        return this.http.put(this.changePswdApi, password, {
            headers,
            withCredentials: true
        });
    }

    //checking if the user
    isLoggedIn() : boolean {
        return this.authHelp.isLoggedIn();
    }

    logout(){
        localStorage.removeItem("Token");
        localStorage.removeItem("events");
        localStorage.removeItem("tickets");
        localStorage.removeItem("userProfileData");

        window.location.href = "/login";
    }
}