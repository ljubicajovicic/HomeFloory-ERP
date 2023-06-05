import { Component } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent {
  totals: any = {}

  constructor(public basketService: BasketService) { }

  ngOnInit() {
    // Subscribe to the basket source and update the 'basket' object
    this.basketService.basketTotalSource$.subscribe(totals => {
      this.totals = totals || {}; // Assign the received basket or an empty object
    });
  }
}
