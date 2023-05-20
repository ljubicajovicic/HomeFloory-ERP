import { Component, OnInit } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { ShopParam } from 'src/app/shared/models/shopParam';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Proizvod;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const idProizvod = this.activatedRoute.snapshot.paramMap.get('id');

    if (idProizvod) this.shopService.getIndividualProizvod(+idProizvod).subscribe({
      next: product => this.product = product,
      error: error => console.log(error)
    })
  }

}
