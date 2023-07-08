import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Korpa } from '../shared/models/korpa';
import { KorisnikParam } from '../shared/models/shopParam';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = 'https://localhost:7294/api/';

  constructor(private http: HttpClient) { }

  getIndividualOrder(idKorpa: number) {
    return this.http.get<Korpa>(this.baseUrl + 'Korpa/' + idKorpa);
  }

  getOrders() {
    return this.http.get<Korpa[]>(this.baseUrl + 'Korpa');
  }

  /*getOrderUser(param: KorisnikParam) {
    let params = new HttpParams();

    if (param.idKorisnik > 0) {
      params = params.set('filterOn', 'idKorisnik');
      params = params.set('filterQuery', param.idKorisnik.toString());
    }

    return this.http.get<Korpa[]>(this.baseUrl + 'Korpa/Korisnik?filterOn=',)
  }*/

  getOrdersUser() {
    const korisnikId = Number(localStorage.getItem('idKorisnika'))
    return this.http.get<Korpa[]>(this.baseUrl + 'Korpa/Korisnik?filterOn=idKorisnik&filterQuery=' + korisnikId)
  }


}
