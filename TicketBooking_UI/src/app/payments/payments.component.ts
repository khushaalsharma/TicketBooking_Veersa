import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from "../navbar/navbar.component";
import { PaymentModel, PaymentTicket, PrePaymentModel } from './payment.model';
import { PaymentService } from './payments.service';
import { CurrencyChange } from '../utils/currency.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-payments',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  providers:[PaymentService, CurrencyChange],
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css']
})
export class PaymentsComponent implements OnInit {
  checkoutData: PrePaymentModel | null = null;
  subamount: number = 0;
  totalAmount: number = 0;
  taxes: number = 0;
  creditCardNum !: string;
  cvv !: number;
  expiry !: string;
  coupon : string | null = null;

  constructor(private paymentService: PaymentService, private currency: CurrencyChange) {}

  ngOnInit(): void {
      this.paymentService.getPaymentData().subscribe((data) => {
          this.checkoutData = data ?? JSON.parse(sessionStorage.getItem("checkoutData") || 'null');
          console.log("Received checkout data in PaymentsComponent:", this.checkoutData);

          if (this.checkoutData) {
              this.subamount = this.checkoutData.amount;
              this.taxes = this.currency.calcTaxes(this.subamount);
              this.totalAmount = this.taxes + this.subamount;
          }
      });
  }

  checkCoupon(){
    if(this.coupon && this.coupon.length > 0){
      this.paymentService.checkForCoupon(this.coupon)
          .subscribe((response) => {
            if(response.status === 400){
              alert("Invalid Coupon");
            }
            else{
              console.log(response);
              this.subamount = this.subamount - ((response.discountPercentage * this.subamount) /100);
              this.taxes = this.currency.calcTaxes(this.subamount);
              this.totalAmount = this.subamount + this.taxes;
            }
          },  
          (error) => {
            alert("Invalid code");
            console.log(error);
          }
        )
    }
    else{
      alert("Enter a coupon code")
    }
  }

  getAmount(amount: number | undefined) {
    const sessionToken = localStorage.getItem("Token");
    if (sessionToken) {
      const claims = atob(sessionToken.split('.')[1]);
      console.log(claims);
      var parsedToken = JSON.parse(claims);
      var currencyByUser = parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"];

      if (currencyByUser === "INR") {
        return "₹" + amount;
      } else {
        return "$" + this.currency.inrToUsd(amount);
      }
    }
    return "₹" + amount;
  }

  luhnAlgo(){
    var sum = 0;
    for (var i = 0; i < this.creditCardNum.length; i++) {
        var intVal = parseInt(this.creditCardNum.substr(i, 1));
        if (i % 2 == 0) {
            intVal *= 2;
            if (intVal > 9) {
                intVal = 1 + (intVal % 10);
            }
        }
        sum += intVal;
    }
    return (sum % 10) == 0;
  }

  validateCard(){
    var regex = new RegExp("^[0-9]{16}$");
    console.log(this.creditCardNum);
    if (regex.test(this.creditCardNum) === false){
      alert("Please enter 16 digits of your credit card");
      return false;
    }
    //checking by luhn algorithm
    var outcome = this.luhnAlgo();
    if(!outcome){
      alert("Please enter a valid credit card number");
    }

    return outcome;
  }

  validateExpiryDate() {
    const today = new Date();
    const currentMonth = today.getMonth() + 1; // JavaScript months are 0-indexed
    const currentYear = today.getFullYear();

    var month : any = this.expiry.slice(0,2);
    var year: any = "20" + this.expiry.slice(3,5);
  
    // Convert input to numbers
    month = parseInt(month, 10);
    year = parseInt(year, 10);
  
    // Check if month is valid
    if (month < 1 || month > 12) {
      alert("Date is not valid");
      return false;
    }
  
    // Check if year is valid
    if (year < currentYear || year > currentYear + 10) { // You can adjust the max year range
      alert("Date is not valid");
      return false;
    }
  
    // Check if card is expired
    if (year < currentYear || (year === currentYear && month < currentMonth)) {
      alert("Card is expired");
      return false;
    }
  
    return true;
  }

  finalCheckout(){
    if(this.validateCard() && this.validateExpiryDate()){

      var paymentData : PaymentModel = {
        paymentMethod: "creditcard",
        methodDetail: this.creditCardNum,
        boughtAt: new Date().toISOString(),
        amount: this.totalAmount,
        couponCode: this.coupon,
        newTickets: []
      }

      this.checkoutData?.tickets.forEach((ticket) => {
        var newTicket:PaymentTicket = {
          ticketQty: ticket.qty,
          amount: ticket.amount,
          eventId: ticket.eventId,
          ticketTypeId: ticket.ticketTypeId
        }

        paymentData.newTickets.push(newTicket);
      })

      this.paymentService.newPayment(paymentData)
                         .subscribe((response) => {
                          console.log(response);
                          if(response.status === 400){
                            alert("Bookings can't be made!");
                          }
                          else{
                            alert("Payment Success!");
                            alert("Booking added");
                            window.location.href = "/bookings";
                          }
                         },
                         (error) => {
                          console.log(error);
                          alert("Error in completing bookings");
                         }
                        )
    }
  }
}
