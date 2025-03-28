import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  standalone: false,
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent {

  searchResult: FlightRm[] = [
    {
      airline: "American Airlines",
      remainingNumberOfSeats: 500,
      departure: { time: Date.now().toString(), place: "LosAngeles" },
      arrival: { time: Date.now().toString(), place: "Istanbul" },
      price: "350"
    },
    {
      airline: "Deutsche BA",
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: "Munchen" },
      arrival: { time: Date.now().toString(), place: "Schipol" },
      price: "600"
    },
    {
      airline: "British Airwaays",
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: "London, England" },
      arrival: { time: Date.now().toString(), place: "Vizzola-Ticino" },
      price: "600"
    }
  ]
}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
