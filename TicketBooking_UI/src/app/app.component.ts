import { Component } from '@angular/core';

import { HomepageComponent } from './homepage/homepage.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, HomepageComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'TicketBooking_UI';
}
