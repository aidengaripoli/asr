import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RoomsComponent } from './components/rooms/rooms.component';
import { MessagesComponent } from './components/messages/messages.component';
import { AppRoutingModule } from './app-routing.module';
import { SlotsComponent } from './components/slots/slots.component';
import { NavMenuComponent } from "./components/nav-menu/nav-menu.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { filterByStaffOrStudentID } from './pipes/custom.pipes';

@NgModule({
  declarations: [
    AppComponent,
    RoomsComponent,
    MessagesComponent,
    SlotsComponent,
    NavMenuComponent,
    filterByStaffOrStudentID
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
