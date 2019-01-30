import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RoomsComponent } from './rooms/rooms.component';
import { RoomDetailComponent } from './room-detail/room-detail.component';

const routes: Routes = [
  //{ path: '', redirectTo: '', pathMatch: 'full' },
  { path: 'rooms/:id', component: RoomDetailComponent },
  { path: 'rooms', component: RoomsComponent }
];

@NgModule({
  declarations: [],
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
