import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Proizvod, ProizvodAdd } from '../shared/models/proizvod';
import { Kategorija, KategorijaAdd } from '../shared/models/kategorija';
import { Proizvodjac, ProizvodjacAdd } from '../shared/models/proizvodjac';
import { ShopParam } from '../shared/models/shopParam';
import { Pagination } from '../shared/models/pagination';
import { Korpa } from '../shared/models/korpa';
import { Korisnik, KorisnikAdmin } from '../shared/models/korisnik';

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

  getProductsNoParams() {
    return this.http.get<Proizvod[]>(this.baseUrl + 'Proizvod/NoParam');
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

  getKorpa() {
    return this.http.get<Korpa[]>(this.baseUrl + 'Korpa')
  }

  getKorisnik() {
    return this.http.get<Korisnik[]>(this.baseUrl + 'Korisnik')
  }

  addCategory(category: KategorijaAdd) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<KategorijaAdd>(this.baseUrl + 'Kategorija', category, { headers });
  }

  updateCategory(category: KategorijaAdd, idKategorija: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put<KategorijaAdd>(this.baseUrl + 'Kategorija/' + idKategorija, category, { headers });
  }

  deleteCategory(categoryId: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.delete<KategorijaAdd>(this.baseUrl + 'Kategorija/' + categoryId, { headers })
  }

  addProducer(producer: ProizvodjacAdd) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<ProizvodjacAdd>(this.baseUrl + 'Proizvodjac', producer, { headers });
  }

  updateProducer(producer: ProizvodjacAdd, idProizvodjac: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put<ProizvodjacAdd>(this.baseUrl + 'Proizvodjac/' + idProizvodjac, producer, { headers });
  }

  deleteProducer(idProizvodjac: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.delete<ProizvodjacAdd>(this.baseUrl + 'Proizvodjac/' + idProizvodjac, { headers })
  }

  addProduct(product: ProizvodAdd) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<ProizvodAdd>(this.baseUrl + 'Proizvod', product, { headers });
  }

  updateProduct(product: ProizvodAdd, idProizvod: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put<ProizvodAdd>(this.baseUrl + 'Proizvod/' + idProizvod, product, { headers });
  }

  deleteProduct(idProizvod: number) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.delete<ProizvodAdd>(this.baseUrl + 'Proizvod/' + idProizvod, { headers })
  }

  addAdmin(admin: KorisnikAdmin) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<KorisnikAdmin>(this.baseUrl + 'admin', admin, { headers });
  }


}
