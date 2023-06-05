import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DodatiProizvodi, Korpa } from '../models/korpa';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent {
  @Output() addItem = new EventEmitter<DodatiProizvodi>()
  @Output() removeItem = new EventEmitter<{ idProizvod: number, kolicina: number }>()
  @Input() isBasket = true;

  constructor(public basketService: BasketService) { }

  addBasketitem(item: DodatiProizvodi) {
    this.addItem.emit(item);
  }

  removeBasketItem(idProizvod: number, kolicina = 1) {
    this.removeItem.emit({ idProizvod, kolicina });
  }

}
