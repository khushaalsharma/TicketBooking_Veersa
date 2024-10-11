import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class AuthHelperService{
    isLoggedIn() : boolean {
        return localStorage.getItem("Token") !== null;
    }
}