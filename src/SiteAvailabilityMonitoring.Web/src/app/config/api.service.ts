import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, observeOn, retry } from 'rxjs/operators';
import { Website } from '../website/website.component';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) { }

  baseUrl: string = 'http://localhost:5000';

  getWebsites() {
    const url = `${this.baseUrl}/api/website`;
    return this.http.get<Array<Website>>(url);
  }

  addWebsites(websites: any): Observable<Array<Website>> {
    const url = `${this.baseUrl}/api/website`;
    return this.http.post<Array<Website>>(url, websites);
  }

  deletewebsite(id: number): Observable<unknown> {
    const url = `${this.baseUrl}/api/website/${id}`;
    return this.http.delete<any>(url);
  }

  checkWebsites(): Observable<any> {
    const url = `${this.baseUrl}/api/website/check`;
    return this.http.post<any>(url, null);
  }
}