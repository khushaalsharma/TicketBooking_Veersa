import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import type { EventModel } from './event.model';

import { AuthHelperService } from '../../auth/authHelper.service';
import { CurrencyChange } from '../../utils/currency.service';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule],
  providers: [CurrencyChange],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent {
  @Input({required: true}) eventData !: EventModel;
  //@Output() bookEvent : EventEmitter<Booking> = new EventEmitter<Booking>();

  constructor(private authHelp : AuthHelperService, private currency :CurrencyChange){}

  priceAccUser(price : number) : string{
    const sessionToken = localStorage.getItem("Token");
    if(sessionToken){
      const claims = atob(sessionToken.split('.')[1]);
      console.log(claims);
      var parsedToken = JSON.parse(claims);
      var currencyByUser = parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"];

      if(currencyByUser === "INR"){
        return ("₹"+price);
      }
      else{
        return ("$"+this.currency.inrToUsd(price));
      }
    }
    return ("₹"+price);
  }

  onBook(){
    window.location.href = `/event/${this.eventData.id}`
  }
  // ngOnInit(){
  //   console.log(this.eventData);
  // }

  // onBook(){
  //   if(this.authHelp.isLoggedIn() === false){
  //     alert("Please sign in to book tickets");
  //     window.location.href = "/login";
  //   }
  //   else{
  //     const eventDetails : Booking = {
  //       id: this.eventData.id,
  //       eventName: this.eventData.eventName,
  //       ticketPrice: this.eventData.minTicketPrice
  //     };

  //     this.bookEvent.emit(eventDetails);
  //   }
  // }
}
