import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { RegisterPassengerComponent } from './register-passenger/register-passenger.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';

const routes: Routes = [
  { path: '', component: SearchFlightsComponent, pathMatch: 'full' },
  { path: 'search-flights', component: SearchFlightsComponent },
  { path: 'book-flight/:flightId', component: BookFlightComponent },
  { path: 'register-passenger', component: RegisterPassengerComponent },
  { path: 'my-booking', component: MyBookingsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
