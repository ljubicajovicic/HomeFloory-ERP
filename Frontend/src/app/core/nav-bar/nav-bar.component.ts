import { Component } from '@angular/core';
import { AccountModule } from 'src/app/account/account.module';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { DodatiProizvodi, Korpa } from 'src/app/shared/models/korpa';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  dodatiProizvodi: DodatiProizvodi[] = [];

  constructor(public basketService: BasketService, public accountService: AccountService) { }

  getCount(dodatiProizvodi: DodatiProizvodi[]) {
    return dodatiProizvodi.reduce((sum, item) => sum + item.kolicina, 0)
  }

  /*getCount(basket: Korpa): number {
    if (!basket || !basket.dodatiProizvodi || basket.dodatiProizvodi.length === 0) {
      return 0;
    }

    const count = basket.dodatiProizvodi.reduce((total, product) => {
      return total + product.kolicina;
    }, 0);

    return count;
  }*/

}
