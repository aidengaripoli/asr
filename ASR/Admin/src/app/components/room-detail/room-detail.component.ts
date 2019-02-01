import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Room } from '../../room';
import { RoomService } from '../../services/room.service';

@Component({
  selector: 'app-room-detail',
  templateUrl: './room-detail.component.html',
  styleUrls: ['./room-detail.component.css']
})
export class RoomDetailComponent implements OnInit {
  @Input() room: Room;

  constructor(
    private roomService: RoomService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.getRoom();
  }

  getRoom(): void {
    const id = this.route.snapshot.paramMap.get('id');

    this.roomService.getRoom(id)
      .subscribe(room => this.room = room);
  }
}
