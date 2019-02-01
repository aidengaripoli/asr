import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersURL = 'https://localhost:44300/api/user/'

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  getStudents(): Observable<User[]> {
    return this.http.get<User[]>(this.usersURL + 'student')
      .pipe(
        tap(_ => this.log('fetched rooms')),
        catchError(this.handleError('getStudents', []))
      );
  }

  getStaffs(): Observable<User[]> {
    return this.http.get<User[]>(this.usersURL + 'staff')
      .pipe(
        tap(_ => this.log('fetched rooms')),
        catchError(this.handleError('getStaffs', []))
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

export class User {
  name: string
  schoolID: string
}
