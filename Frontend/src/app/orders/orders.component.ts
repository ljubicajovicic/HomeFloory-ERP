import { Component } from '@angular/core';
import { Order } from '@stripe/stripe-js';
import { OrdersService } from './orders.service';
import { Korpa } from '../shared/models/korpa';
import { KorisnikParam } from '../shared/models/shopParam';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent {

  orders: Korpa[] = []

  constructor(private orderService: OrdersService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrdersUser().subscribe({
      next: orders => this.orders = orders
    })
  }



}
