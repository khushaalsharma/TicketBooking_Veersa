import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { TicketComponent } from "./ticket/ticket.component";
import { HttpClientModule } from '@angular/common/http';
import { TicketService } from './tickets.service';
import { TicketModel } from './ticket/ticket.model';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [HttpClientModule, NavbarComponent, TicketComponent],
  providers: [TicketService],
  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css'
})
export class TicketsComponent {
  ticketData : TicketModel[] = [];

  constructor(private ticketService : TicketService){}

  setTickets(){
    const data = localStorage.getItem("tickets");
    if(data){
      this.ticketData = JSON.parse(data);
    }
  }

  ngOnInit(): void{
    this.ticketService.getTickets()
        .subscribe(response => {
          if(response){
            console.log(response);
            localStorage.setItem("tickets", JSON.stringify(response));
            this.setTickets();
          }
        })
  }
}
