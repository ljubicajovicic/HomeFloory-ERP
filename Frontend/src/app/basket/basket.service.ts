import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';
import { BasketTotal, DodatiProizvodi, Korpa } from '../shared/models/korpa';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Proizvod } from '../shared/models/proizvod';
import axios from 'axios';
import { Dostava } from '../shared/models/dostava';

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
    this.shipping = deliveryMehod.cenaUsluge;
    /*if (basket) {
      basket.idDostava = deliveryMehod.idDostava;
      this.setBasket(basket)
    }*/
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
    const idKorpa = Number(localStorage.getItem('idKorpa'))

    const existingItem = basket.dodatiProizvodi.find(
      x => x.idProizvod === item.idProizvod && x.idKorpa === idKorpa,
      console.log(idKorpa)
    );


    if (existingItem) {
      // Item already exists in the basket, update the quantity
      existingItem.kolicina += kolicina;
      await this.updateProductQuantityInBasket(existingItem.idProizvod, existingItem.kolicina);
    } else {
      // Item does not exist in the basket, add a new item
      basket.dodatiProizvodi = this.addOrUpdateItem(basket.dodatiProizvodi, item, kolicina);
      this.addBasketItem(item);
    }

    this.calculateTotals();
  }

  updateProductQuantityInBasket(productId: number, quantity: number) {
    const idKorpa = Number(localStorage.getItem('idKorpa'));
    const url = `${this.baseUrl}DodatiProizvodi/${productId}/${idKorpa}`;

    // Create an object with the updated quantity
    const updatedProduct: Partial<DodatiProizvodi> = {
      kolicina: quantity
    };

    return this.http.put<DodatiProizvodi>(url, updatedProduct).subscribe({
      next: basket => {
        this.basketItemSource.next(basket);
        this.calculateTotals();
      }
    });
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
      if (basket.dodatiProizvodi.length > 0) this.updateProductQuantityInBasket(idProizvod, kolicina)
      else this.deleteBasket(basket);
    }
  }

  deleteBasket(basket: Korpa) {
    return this.http.delete(this.baseUrl + 'Korpa/' + basket.idKorpa).subscribe({
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
    const idKorpa = Number(localStorage.getItem('idKorpa'))
    const item = dodatiProizvodi.find(x => x.idProizvod === itemToAdd.idProizvod && x.idKorpa === idKorpa);
    if (item) item.kolicina += kolicina;
    else {
      itemToAdd.kolicina = kolicina;
      dodatiProizvodi.push(itemToAdd)
    }
    return dodatiProizvodi;
  }


  private async createBasket(): Promise<Korpa> {
    const basket = new Korpa();

    try {
      const response = await axios.post(this.baseUrl + 'Korpa', basket);
      const savedBasket = response.data;

      localStorage.setItem('idKorpa', savedBasket.idKorpa.toString());

      // Assuming 'setBasket' method updates 'dodatiProizvodi' property on the 'Korpa' object
      savedBasket.dodatiProizvodi = []; // Set it to an empty array or initialize it with appropriate values

      this.setBasket(savedBasket);

      return savedBasket;
    } catch (error) {
      console.error('Failed to create basket:', error);
      throw error;
    }
  }

  private mapProductItemtoBasketItem(item: Proizvod): DodatiProizvodi {
    return {
      idProizvod: item.idProizvod,
      idKorpa: Number(localStorage.getItem('idKorpa')),
      cena: item.cenaPoM2,
      kolicina: 1,
      kolicinaPoM2: 1,

      urlSlike: item.urlSlike,
      naziv: item.naziv,

    }
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue()
    if (!basket) return;
    basket.ukupnaCena = basket.dodatiProizvodi.reduce((a, b) => (b.cena * b.kolicina) + a, 0)
    const total = basket.ukupnaCena + this.shipping
    if (total > 15000) this.shipping = 0
    this.basketTotalSource.next({ shipping: this.shipping, subtotal: basket.ukupnaCena, total })
  }

  private isProduct(item: Proizvod | DodatiProizvodi): item is Proizvod {
    return (item as Proizvod).idKategorija !== undefined
  }


}
