import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { PaymentModel, PrePaymentModel } from "./payment.model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError } from "rxjs";
import { throwError } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class PaymentService {
    private checkoutData = new BehaviorSubject<PrePaymentModel | null>(
        JSON.parse(sessionStorage.getItem("checkoutData") || 'null')
    );
    private sessionToken = localStorage.getItem("Token");
    private checkoutApiUrl = "https://localhost:7254/api/Payment/NewTickets";
    private checkCouponUrl = "https://localhost:7254/checkCoupon/"

    constructor(private http: HttpClient) {}

    setPaymentData(data: PrePaymentModel) {
        this.checkoutData.next(data);
        sessionStorage.setItem("checkoutData", JSON.stringify(data)); // Persist to sessionStorage
    }

    getPaymentData() {
        return this.checkoutData.asObservable();
    }

    newPayment(paymentData: PaymentModel): Observable<any> {
        const headers = new HttpHeaders({
            "Authorization": `Bearer ${this.sessionToken}`,
            "Content-Type": "application/json"
        });
    
        return this.http.post<any>(this.checkoutApiUrl, paymentData, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any) => {
                console.error('Error occurred:', error);
                return throwError(() => error); // Throws error to be handled by the caller
            })
        );
    }

    checkForCoupon(code: string): Observable<any>{
        const headers = new HttpHeaders({
            "Authorization": `Bearer ${this.sessionToken}`,
            "Content-Type": "Application/json"
        });

        return this.http.get<any>(this.checkCouponUrl + `${code}`, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any): Observable<any> => {
                console.log(error);
                throw error;
            })
        );
    }
}

