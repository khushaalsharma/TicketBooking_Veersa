import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { CartService } from './cart.service';
import { CurrencyChange } from '../utils/currency.service';
import { CartModel } from './cart.model';
import { CommonModule } from '@angular/common';
import { PrePaymentModel } from '../payments/payment.model';
import { PaymentService } from '../payments/payments.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  providers: [PaymentService, CartService, CurrencyChange],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  userCart: CartModel[] = [];
  totalAmount: number = 0;

  constructor(
    private cartService: CartService,
    private currency: CurrencyChange,
    private paymentService: PaymentService,
    private router: Router
  ) {}

  getCartItem() {
    var cartItems = localStorage.getItem("cart");
    if (cartItems !== null) {
      this.userCart = JSON.parse(cartItems);
      console.log(this.userCart);

      this.userCart.forEach((item) => {
        this.totalAmount += item.amount;
      });
    }
  }

  getAmount(amount: number) {
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

  ngOnInit(): void {
    this.cartService.getCart().subscribe(
      (response) => {
        console.log(response.cartItems);
        localStorage.setItem("cart", JSON.stringify(response.cartItems));
        this.getCartItem();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  checkout() {
    if(this.totalAmount > 0){
      var data: PrePaymentModel = {
        amount: this.totalAmount,
        tickets: []
      };
  
      this.userCart.forEach((cart) => {
        var ticket = {
          eventName: cart.event.eventName,
          eventId: cart.eventId,
          ticketTypeName: cart.ticketType.ticketCategory,
          ticketTypeId: cart.ticketTypeId,
          amount: cart.amount,
          qty: cart.quantity
        };
        data.tickets.push(ticket);
      });
  
      // Save data to sessionStorage
      sessionStorage.setItem("checkoutData", JSON.stringify(data));
  
      // Navigate without reloading
      this.router.navigate(['/checkout']);
    }
    else{
      alert("Add items to cart");
      window.location.href = "/home";
    }
  }

}
