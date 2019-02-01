import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'filterByStaffOrStudentID'})
export class filterByStaffOrStudentID implements PipeTransform {
  transform(slots: any[], ID: string): any[] {
    if (!ID) return slots;
    return slots.filter((x:any) => x.staff.schoolID == ID ||  (x.student != null && x.student.schoolID == ID))
  } 
}
