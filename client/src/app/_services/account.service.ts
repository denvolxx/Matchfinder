import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { IUser } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<IUser | null>(null);

  login(model: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          window.localStorage.setItem('user', JSON.stringify(user))
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          window.localStorage.setItem('user', JSON.stringify(user))
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  logout() {
    window.localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
