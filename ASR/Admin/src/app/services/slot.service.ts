import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class SlotService {
  private slotsURL = 'https://localhost:44300/api/slot/'

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  getSlots(): Observable<Slot[]> {
    return this.http.get<Slot[]>(this.slotsURL)
      .pipe(
        tap(_ => this.log('fetched slots')),
        catchError(this.handleError('=', []))
      );
  }

  deleteSlot(slot: Slot) {
    return this.http.delete(this.slotsURL + slot.roomID + '/' + slot.startTime)
      .pipe(
        tap(_ => this.log('deleted slot')),
        catchError(this.handleError('=', []))
      );
  }

  updateSlotStudent(slot: Slot, student: string) {
    const httpOptions = {
      headers: new HttpHeaders({'Content-Type':  'application/json'})
    };
    return this.http.put(this.slotsURL + slot.roomID + '/' + slot.startTime, JSON.stringify(student), httpOptions)
      .pipe(
        tap(_ => this.log('updated slot')),
        catchError(this.handleError('=', []))
      );
  }


  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a SlotService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`SlotService: ${message}`);
  }

}

export class Slot {
  roomID: string
  startTime: Date
  staff: Object
  student: Object
}
