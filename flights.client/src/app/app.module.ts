import { provideHttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { AppRoutingModule } from './app-routing.module';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    SearchFlightsComponent,
    BookFlightComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavMenuComponent
],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
