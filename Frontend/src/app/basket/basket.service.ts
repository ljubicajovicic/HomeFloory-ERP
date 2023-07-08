import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';
import { BasketTotal, DodatiProizvodi, Korpa } from '../shared/models/korpa';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Proizvod } from '../shared/models/proizvod';
import axios from 'axios';
import { Dostava, Dostavaid } from '../shared/models/dostava';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = 'https://localhost:7294/api/';
  private basketSource = new BehaviorSubject<Korpa | null>(null)
  private basketItemSource = new BehaviorSubject<DodatiProizvodi | null>(null)
  private basketTotalSource = new BehaviorSubject<BasketTotal | null>(null)
  basketSource$ = this.basketSource.asObservable()
  basketItemSource$ = this.basketItemSource.asObservable()
  basketTotalSource$ = this.basketTotalSource.asObservable()
  shipping = 0;

  constructor(private http: HttpClient) { }

  createPaymentIntent() {
    const idKorpa = Number(localStorage.getItem('idKorpa'))
    const token = localStorage.getItem('token')
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post<Korpa>(this.baseUrl + 'Payment/' + idKorpa, {}, { headers })
      .pipe(
        map(basket => {
          this.basketSource.next(basket);
          console.log(basket);
        })
      )
  }

  setShippingPrice(deliveryMehod: Dostava) {
    const basket = this.getCurrentBasketValue()
    this.shipping = deliveryMehod.cenaUsluge
    if (basket) {
      basket.idDostava = deliveryMehod.idDostava;
    }
    this.calculateTotals();
  }

  getBasket(id: string | null) {
    return this.http.get<Korpa>(this.baseUrl + 'Korpa/' + id).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }

  setBasket(basket: Korpa) {
    return this.http.post<Korpa>(this.baseUrl + 'Korpa', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }

  addBasketItem(basketItem: DodatiProizvodi) {
    return this.http.post<DodatiProizvodi>(this.baseUrl + 'DodatiProizvodi', basketItem).subscribe({
      next: basketItem => this.basketItemSource.next(basketItem)
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  async addItemToBasket(item: Proizvod | DodatiProizvodi, kolicina = 1) {
    if (this.isProduct(item)) item = this.mapProductItemtoBasketItem(item);

    const basket = this.getCurrentBasketValue() || await this.createBasket();
    basket.dodatiProizvodi = this.addOrUpdateItem(basket.dodatiProizvodi, item, kolicina);
    this.addBasketItem(item);

    this.calculateTotals();
  }




  removeItemFromBasket(idProizvod: number, kolicina = 1) {
    const basket = this.getCurrentBasketValue()
    if (!basket) return;
    const item = basket.dodatiProizvodi.find(x => x.idProizvod === idProizvod)
    if (item) {
      item.kolicina -= kolicina;
      if (item.kolicina === 0) {
        basket.dodatiProizvodi = basket.dodatiProizvodi.filter(x => x.idProizvod !== idProizvod)
      }
      if (basket.dodatiProizvodi.length === 0)
        this.deleteBasket(basket);
    }
  }


  deleteBasket(basket: Korpa) {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(this.baseUrl + 'Korpa/' + basket.idKorpa, { headers }).subscribe({
      next: () => {
        this.deleteLocalBasket();
      }
    })
  }

  deleteLocalBasket() {
    this.basketSource.next(null)
    this.basketTotalSource.next(null)
    localStorage.removeItem('idKorpa')
  }

  private addOrUpdateItem(dodatiProizvodi: DodatiProizvodi[], itemToAdd: DodatiProizvodi, kolicina: number): DodatiProizvodi[] {

    const item = dodatiProizvodi.find(x => x.idDodatiProizvodi === itemToAdd.idDodatiProizvodi);

    if (item) { item.kolicina += kolicina; }
    else {
      itemToAdd.kolicina = kolicina;
      dodatiProizvodi.push(itemToAdd)
    }
    return dodatiProizvodi;
  }


  public async createBasket(): Promise<Korpa> {
    const basket = new Korpa();

    try {
      const response = await axios.post(this.baseUrl + 'Korpa', basket);
      const savedBasket = response.data;

      localStorage.setItem('idKorpa', savedBasket.idKorpa.toString());

      savedBasket.dodatiProizvodi = [];

      this.setBasket(savedBasket);

      return savedBasket;
    } catch (error) {
      console.error('Failed to create basket:', error);
      throw error;
    }
  }

  private mapProductItemtoBasketItem(item: Proizvod): DodatiProizvodi {
    const basket = this.getCurrentBasketValue()
    const kolicina = basket?.dodatiProizvodi.reduce((sum, product) => sum + product.kolicina, 0) || 0;

    return {
      idDodatiProizvodi: item.idProizvod,
      idProizvod: item.idProizvod,
      idKorpa: Number(localStorage.getItem('idKorpa')),
      cena: item.cenaPoM2,
      kolicina: 1,

      urlSlike: item.urlSlike,
      naziv: item.naziv,

    }
  }

  private calculateTotals(): { shipping: number, subtotal: number, total: number } {
    const basket = this.getCurrentBasketValue();
    if (!basket) return { shipping: 0, subtotal: 0, total: 0 };
    const subtotal = basket.dodatiProizvodi.reduce((a, b) => (b.cena * b.kolicina) + a, 0);
    let shipping = this.shipping;

    let total = subtotal + shipping;

    if (total > 15000) {
      shipping = 0
      total = subtotal + shipping;
    }

    this.basketTotalSource.next({ shipping: shipping, subtotal, total })
    return { shipping, subtotal, total };
  }

  public getShipping(): number {
    const { shipping } = this.calculateTotals();
    return shipping;
  }

  public getTotal(): number {
    const { total } = this.calculateTotals();
    return total;
  }


  private isProduct(item: Proizvod | DodatiProizvodi): item is Proizvod {
    return (item as Proizvod).idKategorija !== undefined
  }


}
