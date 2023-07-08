import { Component, OnInit } from '@angular/core';
import { Korpa } from '../shared/models/korpa';
import { OrdersService } from '../orders/orders.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
  order?: Korpa;

  constructor(private orderService: OrdersService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    id && this.orderService.getIndividualOrder(+id).subscribe({
      next: order => {
        this.order = order;
      }
    })
  }

}
