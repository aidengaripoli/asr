import { Component, OnInit } from '@angular/core';

import { SlotService, Slot } from '../../services/slot.service';
import { UserService, User } from '../../services/user.service';
import { filterByStaffOrStudentID } from '../../pipes/custom.pipes';

@Component({
  selector: 'app-slots',
  templateUrl: './slots.component.html',
  styleUrls: ['./slots.component.css']
})
export class SlotsComponent implements OnInit {
  slots: Slot[];
  students: User[];
  staffs: User[];
  staffID:string = '';

  constructor(private slotService: SlotService,private userService: UserService) { }

  ngOnInit() {
    this.getSlots();
    this.getUsers();
  }

  getSlots(): void {
    this.slotService.getSlots()
      .subscribe(slots => this.slots = slots
      );
  }

  getUsers(): void {
    this.userService.getStudents()
      .subscribe(students => this.students = students
      );
    this.userService.getStaffs()
      .subscribe(staffs => this.staffs = staffs
      );
  }

}


