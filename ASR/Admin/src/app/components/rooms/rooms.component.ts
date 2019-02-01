import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { RoomService,Room } from '../../services/room.service';


@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  rooms: Room[];
  roomForm = new FormControl('');

  constructor(private roomService: RoomService) { }

  ngOnInit() {
    this.getRooms();
  }

  getRooms(): void {
    this.roomService.getRooms()
      .subscribe(rooms => this.rooms = rooms);
  }

  createRoom(): void {
    let room = new Room (this.roomForm.value)
    this.roomService.createRoom(room)
      .subscribe((data) =>this.ngOnInit());
  }
}
