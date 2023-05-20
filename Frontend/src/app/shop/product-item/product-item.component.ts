import { Component, Input } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Proizvod;
}
