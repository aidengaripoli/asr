import { Component, OnInit } from '@angular/core';

import { Slot } from '../../slot';
import { SlotService } from '../../services/slot.service';

@Component({
  selector: 'app-slots',
  templateUrl: './slots.component.html',
  styleUrls: ['./slots.component.css']
})
export class SlotsComponent implements OnInit {
  slots: Slot[];

  constructor(private slotService: SlotService) { }

  ngOnInit() {
    this.getSlots();
  }

  getSlots(): void {
    this.slotService.getSlots()
      .subscribe(slots => this.slots = slots
      );
  }

}