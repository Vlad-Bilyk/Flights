import { Component } from '@angular/core';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  standalone: false,
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css',
})
export class SearchFlightsComponent {
  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService) {}

  search() {
    this.flightService.searchFlight({}).subscribe({
      next: (response) => (this.searchResult = response),
      error: this.handleError,
    });
  }

  private handleError(err: any) {
    console.log('Response Error. Status: ', err.Status);
    console.log('Response error. Status Text: ', err.statusText);
    console.log(err);
  }
}
