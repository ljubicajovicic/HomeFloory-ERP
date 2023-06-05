import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-chekout-review',
  templateUrl: './chekout-review.component.html',
  styleUrls: ['./chekout-review.component.scss']
})
export class ChekoutReviewComponent {
  @Input() appStepper?: CdkStepper;

  constructor(private basketService: BasketService) { }

  createPaymentIntent() {
    this.basketService.createPaymentIntent().subscribe({
      next: () => {
        console.log('porudzbina kreirana'),
          this.appStepper?.next();
      },
      error: error => console.log(error)
    })
  }
}
