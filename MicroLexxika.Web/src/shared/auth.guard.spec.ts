import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginResult } from '../models/loginresult.model';
import { tap } from 'rxjs';
import { APIURL } from '../constants';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private http: HttpClient) { };
  ;
  login(username: string, password: string) {
    ;
    return this.http.post<HttpResponse<LoginResult>>(`${APIURL}/auth/login`, { username, password }, { observe: 'response' })
      .pipe(tap(response => {
        if (response.status === 200) {
          this.setSession(response.body as unknown as LoginResult);
        }
      }));
  }

  getExpiration() {
    return localStorage.getItem('expiry')!;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userid');
    localStorage.removeItem('expiry');
  }

  getUserId(): number | undefined {
    return Number(localStorage.getItem('userid'));
  }

  getToken() {
    return localStorage.getItem('token')!;
  }

  isLoggedIn() {
    return this.getToken() != null ? true : false;
  }

  setSession(result: LoginResult) {
    localStorage.setItem('token', result.token);
    localStorage.setItem('userid', result.userId);
    localStorage.setItem('expiry', result.expiry);
  }
}
