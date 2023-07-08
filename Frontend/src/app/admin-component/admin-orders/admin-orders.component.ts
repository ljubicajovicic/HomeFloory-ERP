import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Korpa } from 'src/app/shared/models/korpa';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent {
  dataSource: Korpa[] = []
  subscription!: Subscription
  baseUrl = 'https://localhost:7294/api/';

  constructor(private shopService: ShopService, private http: HttpClient) { }


  ngOnInit(): void {
    this.loadData();
    //console.log(this.dataSource);
  }

  public loadData() {
    this.subscription = this.shopService.getKorpa().subscribe(
      (response: Korpa[]) => {
        this.dataSource = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
