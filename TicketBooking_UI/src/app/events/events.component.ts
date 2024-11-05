import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EventComponent } from "./event/event.component";
import { EventsService } from './events.service';
import type { EventModel } from './event/event.model';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, FormsModule, EventComponent],
  providers: [EventsService],
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent {
  public eventsData : EventModel[] = [];

  searchVal = "";
  fromDate = "";
  toDate = "";
  sortBy = "price";
  sortOrder = true;

  showFilters = false;

  constructor(private eventService: EventsService){} //constructor to use the service object

  getEventsFromStorage(){
    const events = localStorage.getItem("events");
    if(events){
      this.eventsData = JSON.parse(events);
    }
  }

  trackByEventId(index : number, eventData: any){
    return eventData.id;
  }

  ngOnInit(): void { //A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive. Define an ngOnInit() method to handle any additional initialization tasks.
    this.eventService.getEvent().subscribe(
      (data: EventModel[]) => {
        //console.log(data);
        //this.eventsData = data; 
        localStorage.setItem("events", JSON.stringify(data));
        this.getEventsFromStorage();
      },
      (error) => {
        console.error("Error fetching events", error);
      }
    );
  }

  onFilter(){
    // console.log(
    //   {
    //     "1": this.eventName,
    //     "2": this.eventVenue,
    //     "3": this.eventCategory,
    //     "4": this.fromDate,
    //     "5": this.toDate,
    //     "6": this.sortBy,
    //     "7": this.sortOrder,
    //   }
    // )

    this.eventService.getEventByFilter(this.searchVal, this.fromDate, this.toDate, this.sortBy, this.sortOrder).subscribe(
      (data: EventModel[]) => {
        localStorage.setItem("events", JSON.stringify(data));
        this.getEventsFromStorage();
      }, 
      (error: any) => {
        console.log(error);
        alert("Error in filtering");
      }
    )
  }
}
