import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../events.service';
import { SpecificEventModel, TicketTypes } from './eventData.model';
import { NavbarComponent } from '../../navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CurrencyChange } from '../../utils/currency.service';

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
  qtyMap = new Map<number, number>(); //Ticket Quantity Map
  
  constructor(private route: ActivatedRoute, private eventService: EventsService, private currency: CurrencyChange){}

  getDataFromStorage(){
    var data = localStorage.getItem("currentEvent");
    if(data){
      this.eventData = JSON.parse(data);
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

  // Getter for ngModel to access Map values
  getQty(price: number): number {
    return this.qtyMap.get(price) || 0;
  }

  // Setter for ngModel to update Map values
  setQty(price: number, qty: number): void {
    this.qtyMap.set(price, qty);
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
}
