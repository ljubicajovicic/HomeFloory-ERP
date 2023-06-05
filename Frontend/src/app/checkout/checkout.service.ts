import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dostava } from '../shared/models/dostava';
import { map } from 'rxjs';
import { Korpa, Porudzbina } from '../shared/models/korpa';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = 'https://localhost:7294/api/';

  constructor(private http: HttpClient) { }

  createOrder(order: Porudzbina) {
    const idKorpa = Number(localStorage.getItem('idKorpa'))

    return this.http.put<Korpa>(this.baseUrl + 'Korpa/' + idKorpa, order)
  }

  getDeliveryMethods() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getToken()}`
    });
    return this.http.get<Dostava[]>(this.baseUrl + "Dostava", { headers })
  }

  getToken() {
    const token = localStorage.getItem('token')
    return token
  }
}
