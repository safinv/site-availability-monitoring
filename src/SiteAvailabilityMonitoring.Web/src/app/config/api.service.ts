import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, pipe, throwError } from 'rxjs';
import { catchError, observeOn, retry, tap } from 'rxjs/operators';
import { Website, GetWebsitesResponse } from '../website/website.component';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) { }

  baseUrl: string = 'http://localhost:5000';
  errorMessage: string;

  getWebsites() {
    const url = `${this.baseUrl}/api/website`;
    return this.http.get<GetWebsitesResponse>(url);
  }

  insertWebsite(address: any): Observable<Website> {
    const url = `${this.baseUrl}/api/website`;
    return this.http.post<Website>(url, address);
  }

  updateWebsite(id:number, address: any): Observable<Website> {
    const url = `${this.baseUrl}/api/website`;
    return this.http.put<Website>(url, {id, address});
  }

  deletewebsite(id: number): Observable<unknown> {
    const url = `${this.baseUrl}/api/website/${id}`;
    return this.http.delete<any>(url);
  }

  checkWebsites(): Observable<any> {
    const url = `${this.baseUrl}/api/website/check`;
    return this.http.post<any>(url, null);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      this.errorMessage = error.error;
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}