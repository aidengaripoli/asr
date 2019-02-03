import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { RoomService } from '../../services/room.service';
import { Room } from '../../room';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  rooms: Room[];
  roomForm = new FormControl('', [Validators.maxLength(10)]);

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
      .subscribe(() => this.getRooms());
  }

  createRoom(): void {
    if (this.roomForm.valid) {
      let room = new Room(this.roomForm.value)

      this.roomService.createRoom(room)
        .subscribe(() => this.getRooms());

      this.roomForm.setValue('');
    }
  }
}
