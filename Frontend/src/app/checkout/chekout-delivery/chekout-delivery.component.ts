import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dostava } from 'src/app/shared/models/dostava';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-chekout-delivery',
  templateUrl: './chekout-delivery.component.html',
  styleUrls: ['./chekout-delivery.component.scss']
})
export class ChekoutDeliveryComponent implements OnInit {

  @Input() checkoutForm?: FormGroup;
  deliveryMethods: Dostava[] = [];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next: dm => this.deliveryMethods = dm
    })
  }

  setShippingPrice(deliveryMethod: Dostava) {
    this.basketService.setShippingPrice(deliveryMethod);
  }

}
