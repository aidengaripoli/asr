import { Component, OnInit } from '@angular/core';
import { FormControl,Validators } from '@angular/forms';
import { RoomService,Room } from '../../services/room.service';


@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  rooms: Room[];
  roomForm = new FormControl('',[Validators.maxLength(20)]);

  constructor(private roomService: RoomService) { }

  ngOnInit() {
    this.getRooms();
  }

  getRooms(): void {
    this.roomService.getRooms()
      .subscribe(rooms => this.rooms = rooms);
  }
  
  deleteRoom(room: Room): void {
    this.roomService.deleteRoom(room)
      .subscribe((data) =>this.ngOnInit());
  }

  createRoom(): void {
    if (this.roomForm.valid) {
      let room = new Room (this.roomForm.value)
      this.roomService.createRoom(room)
        .subscribe((data) =>this.ngOnInit());
    }
  }
}
