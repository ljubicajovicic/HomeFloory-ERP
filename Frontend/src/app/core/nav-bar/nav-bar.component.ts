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

}
