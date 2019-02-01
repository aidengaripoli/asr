import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RoomsComponent } from './components/rooms/rooms.component';
import { RoomDetailComponent } from './components/room-detail/room-detail.component';

import { SlotsComponent } from './components/slots/slots.component';

const routes: Routes = [
  //{ path: '', redirectTo: '', pathMatch: 'full' },
  { path: 'rooms/:id', component: RoomDetailComponent },
  { path: 'rooms', component: RoomsComponent },
  { path: 'slots', component: SlotsComponent }
];

@NgModule({
  declarations: [],
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
