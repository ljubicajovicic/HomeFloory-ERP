import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ChekoutAdressComponent } from './chekout-adress/chekout-adress.component';
import { ChekoutDeliveryComponent } from './chekout-delivery/chekout-delivery.component';
import { ChekoutReviewComponent } from './chekout-review/chekout-review.component';
import { ChekoutPaymentComponent } from './chekout-payment/chekout-payment.component';
import { ChekoutSuccessComponent } from './chekout-success/chekout-success.component';



@NgModule({
  declarations: [
    CheckoutComponent,
    ChekoutAdressComponent,
    ChekoutDeliveryComponent,
    ChekoutReviewComponent,
    ChekoutPaymentComponent,
    ChekoutSuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule
  ]
})
export class CheckoutModule { }
