import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, map, of, switchMap, take, tap } from 'rxjs';
import { AdresaIsporuke, Kor, Korisnik } from '../shared/models/korisnik';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7294/api/';
  //private currtUser = new BehaviorSubject<string | null>(null);
  private kor = new ReplaySubject<Korisnik | null>(1);
  kor$ = this.kor.asObservable();
  //currentUser$ = this.currtUser.asObservable();

  constructor(private http: HttpClient, private router: Router) { }


  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.kor.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<Korisnik>(this.baseUrl + 'Korisnik', { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.kor.next(user);
          return user
        } else {
          return null;
        }
      })
    )
  }


  login(values: any) {
    return this.http.post<any>(this.baseUrl + 'Prijava', values, { responseType: 'json' }).pipe(
      map(response => {
        const token = response?.token;
        if (token) {
          localStorage.setItem('token', token);
          this.kor.next(token);
        }
      })
    );
  }


  register(values: any) {
    return this.http.post<any>(this.baseUrl + 'Registracija', values, { responseType: 'json' }).pipe(
      map(response => {
        const token = response?.token;
        if (token) {
          localStorage.setItem('token', token);
          this.kor.next(response);
        }
      })
    );
  }


  logout() {
    localStorage.removeItem('token');
    this.kor.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/emailExists?email=' + email);
  }

  getUserAddress() {

    /*const token = localStorage.getItem('token')
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);*/

    return this.http.get<AdresaIsporuke>(this.baseUrl + 'AdresaIsporuke')
  }


  updateUser(user: Korisnik): Observable<Korisnik> {
    return this.http.put<Korisnik>(this.baseUrl + 'Korisnik', user);
  }

  getValue(): Korisnik | null {
    let currentUser: Korisnik | null = null;
    this.kor$.pipe(take(1)).subscribe(user => currentUser = user);
    return currentUser;
  }


  addUserAddress(address: AdresaIsporuke): Observable<number> {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<number>(this.baseUrl + 'AdresaIsporuke', address, { headers })/*.pipe(
      switchMap((addedAddressId: number) => {
        const currentUser = this.getValue();
        if (currentUser) {
          currentUser.idAdresaIsporuke = addedAddressId;
          return this.updateUser(currentUser).pipe(
            map(() => addedAddressId)
          );
        } else {
          return of(addedAddressId);
        }
      })
    );*/
  }






}
