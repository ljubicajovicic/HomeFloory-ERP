import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Proizvod } from '../shared/models/proizvod';
import { Kategorija } from '../shared/models/kategorija';
import { Proizvodjac } from '../shared/models/proizvodjac';
import { ShopParam } from '../shared/models/shopParam';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7294/api/';

  constructor(private http: HttpClient) { }

  getProizvod(shopParam: ShopParam) {
    let params = new HttpParams();

    if (shopParam.search) params = params.append('search', shopParam.search);

    if (shopParam.idKategorija > 0) {
      params = params.set('filterOn', 'idKategorija');
      params = params.set('filterQuery', shopParam.idKategorija.toString());
    }
    if (shopParam.idProizvodjac > 0) {
      params = params.set('filterOn2', 'idProizvodjac');
      params = params.set('filterQuery2', shopParam.idProizvodjac.toString());
    }
    params = params.set('sortBy', shopParam.sort)
    params = params.set('pageNumber', shopParam.pageNumber)
    params = params.set('pageSize', shopParam.pageSize)

    return this.http.get<Pagination<Proizvod[]>>(this.baseUrl + 'Proizvod', { params: params });
  }

  getIndividualProizvod(idProizvod: number) {
    return this.http.get<Proizvod>(this.baseUrl + 'Proizvod/' + idProizvod);
  }

  getKategorija() {
    return this.http.get<Kategorija[]>(this.baseUrl + 'Kategorija');
  }

  getProizvodjac() {
    return this.http.get<Proizvodjac[]>(this.baseUrl + 'Proizvodjac')
  }
}
