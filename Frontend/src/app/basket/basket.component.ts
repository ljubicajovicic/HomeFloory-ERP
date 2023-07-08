import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { DodatiProizvodi } from '../shared/models/korpa';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  basket: any = {};

  constructor(public basketService: BasketService) {
  }

  incrementQuantity(item: DodatiProizvodi) {
    this.basketService.addItemToBasket(item)
  }

  removeItem(event: { idProizvod: number, kolicina: number }) {
    this.basketService.removeItemFromBasket(event.idProizvod, event.kolicina)
  }

  deleteItem() {
    this.basketService.deleteBasket(this.basket)
  }

  ngOnInit() {
    this.basketService.basketSource$.subscribe(basket => {
      this.basket = basket || {}; // Assign the received basket or an empty object
    });
  }
}
