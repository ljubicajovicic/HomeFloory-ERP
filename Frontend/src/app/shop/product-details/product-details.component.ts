import { Component, OnInit } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { ShopParam } from 'src/app/shared/models/shopParam';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Proizvod;
  kolicina = 1
  kolicinaUKorpi = 0

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private basketService: BasketService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const idProizvod = this.activatedRoute.snapshot.paramMap.get('id');

    if (idProizvod) this.shopService.getIndividualProizvod(+idProizvod).subscribe({
      next: product => {
        this.product = product;
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            const item = basket?.dodatiProizvodi.find(x => x.idProizvod === +idProizvod)
            if (item) {
              this.kolicina = item.kolicina,
                this.kolicinaUKorpi = item.kolicina
            }
          }
        })

      },
      error: error => console.log(error)
    })
  }

  incrementQuantity() {
    this.kolicina++;
  }
  decrementQuantity() {
    this.kolicina--;
  }
  updateBasket() {
    if (this.product) {
      if (this.kolicina > this.kolicinaUKorpi) {
        const itemsToAdd = this.kolicina - this.kolicinaUKorpi;
        this.kolicinaUKorpi += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd)
      } else {
        const itemsToRemove = this.kolicinaUKorpi - this.kolicina;
        this.kolicinaUKorpi -= itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.idProizvod, itemsToRemove)
      }
    }
  }

  get buttonText() {
    return this.kolicinaUKorpi === 0 ? 'Dodaj u korpu' : 'Azuriraj korpu'
  }

  public getKolicina(): number {
    return this.kolicina;
  }

}
