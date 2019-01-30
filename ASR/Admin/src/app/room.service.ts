import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Room } from './room';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private roomsURL = 'https://localhost:44300/api/room'

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomsURL)
      .pipe(
        tap(_ => this.log('fetched rooms')),
        catchError(this.handleError('getRooms', []))
      );
  }

  getRoom(id: string): Observable<Room> {
    const url = `${this.roomsURL}/${id}`;

    return this.http.get<Room>(url).pipe(
      tap(_ => this.log(`fetched room id=${id}`)),
      catchError(this.handleError<Room>(`getRoom id=${id}`))
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

  /** Log a RoomService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`RoomService: ${message}`);
  }
}
