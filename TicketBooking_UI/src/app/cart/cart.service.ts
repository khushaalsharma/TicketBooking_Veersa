import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { CartModel } from "./cart.model";

@Injectable({
    providedIn: "root"
})
export class CartService{
    private sessionToken = localStorage.getItem("Token")
    private cartApiUrl = "https://localhost:7254/GetCart";
    private deleteItemApiUrl = "https://localhost:7254/api/cart/DeleteCartItem/"

    constructor(private http: HttpClient){}

    getCart(): Observable<any>{
        var headers = new HttpHeaders({
            "Authorization": `Bearer ${this.sessionToken}`,
            "Content-Type": "application/json"
        })


        return this.http.get<any>(this.cartApiUrl, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any): Observable<any> => {
                console.log(error);
                throw error;
            })
        )
    }

    deleteCartItem(id: string): Observable<any>{
        const headers = new HttpHeaders({
            "Authorization": `Bearer ${this.sessionToken}`,
            "Content-Type": "Application/json"
        });

        return this.http.delete<any>(this.deleteItemApiUrl + `${id}`, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any): Observable<any> => {
                console.log(error);
                throw error;
            })
        )
    }
}