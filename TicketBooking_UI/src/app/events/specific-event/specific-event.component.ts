import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../events.service';
import { SpecificEventModel, TicketTypes } from './eventData.model';
import { NavbarComponent } from '../../navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CurrencyChange } from '../../utils/currency.service';
import { CartEntryModel } from './cartData.model';

@Component({
  selector: 'app-specific-event',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  providers: [EventsService, CurrencyChange],
  templateUrl: './specific-event.component.html',
  styleUrl: './specific-event.component.css'
})
export class SpecificEventComponent {

  eventId !: string;
  eventData !: SpecificEventModel;
  //ticket quantity handling for each type
  ticketQty : CartEntryModel[] = [];
  
  constructor(private route: ActivatedRoute, private eventService: EventsService, private currency: CurrencyChange){}

  getDataFromStorage(){
    var data = localStorage.getItem("currentEvent");
    if(data){
      this.eventData = JSON.parse(data);
      console.log(this.eventData);
      this.ticketQty = this.eventData.ticketTypes.map(ticketType => ({
        quantity: 0,
        price: ticketType.price,
        amount: 0,
        eventId: this.eventId,
        ticketTypeId: ticketType.id
    }));
    }
  }

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

  
  ngOnInit(): void{
    this.eventId = this.route.snapshot.paramMap.get('id') || '';
    console.log('Event ID:', this.eventId);  // Use the ID as needed

    this.eventService.getEventById(this.eventId).subscribe(
      (data: any) => {
        console.log(data);
        localStorage.setItem("currentEvent", JSON.stringify(data));
        this.getDataFromStorage();
      },
      (error: any) => {
        console.log(error);
        alert("Error fetching event details, try again");
      }
    )
  }

  calculateAmount(index: number) {
    this.ticketQty[index].amount = this.ticketQty[index].quantity * this.ticketQty[index].price;
  }

  addToCart(cartItem: CartEntryModel) {

    console.log(cartItem);

    this.eventService.updateCart(cartItem)
        .subscribe(
            (response) => {
                console.log(response); // Log entire response to check structure
                if (response && response.status === 200) {
                    alert("Tickets added to cart.");
                } else {
                    alert("Unexpected response structure.");
                }
            },
            (error) => {
                console.log(error);
                alert("Error in adding to cart, try again later.");
            }
        );
  }

  toCart(){
    window.location.href = "/cart";
  }
}
