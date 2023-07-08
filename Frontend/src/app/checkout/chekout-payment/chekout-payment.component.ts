import { Component, ElementRef, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { Korpa } from 'src/app/shared/models/korpa';
import { AdresaIsporuke } from 'src/app/shared/models/korisnik';
import { NavigationExtras, Router } from '@angular/router';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-chekout-payment',
  templateUrl: './chekout-payment.component.html',
  styleUrls: ['./chekout-payment.component.scss']
})
export class ChekoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService, private router: Router) { }

  ngOnInit(): void {
    loadStripe('pk_test_51NFNx8CcFmpFo8zehRwaN3hQiolPV18b74KYcgQt5wAxnyJpEgJXf8jBHls9tTs4Jq16w4PnDxghOgbZKkrukjsd00lKvR17wh').then(
      stripe => {
        this.stripe = stripe;
        const elements = stripe?.elements();
        if (elements) {
          this.cardNumber = elements.create('cardNumber')
          this.cardNumber.mount(this.cardNumberElement?.nativeElement);

          this.cardExpiry = elements.create('cardExpiry')
          this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);

          this.cardCvc = elements.create('cardCvc')
          this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        }
      }
    )
  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) return;

    const orderToCreate = this.getOrderToCreate(basket);
    console.log("order to create", orderToCreate)
    if (!orderToCreate) return;
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: order => {
        console.log(order)
        this.stripe?.confirmCardPayment(basket.clientSecret!, {
          payment_method: {
            card: this.cardNumber!,
            billing_details: {
              name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
            }
          }
        }).then(result => {
          console.log(result);
          if (result.paymentIntent) {

            //this.basketService.deleteBasket(basket);
            const navigationExtras: NavigationExtras = { state: order };
            this.router.navigate(['checkout/success'], navigationExtras)
          }
        })
      }
    })
  }

  private getOrderToCreate(basket: Partial<Korpa>) {
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('idDostava')?.value;

    const basketValue = this.basketService.getCurrentBasketValue();
    if (!basketValue) return;

    const deliveryPrice = this.basketService.getShipping();
    const subtotal = this.basketService.getTotal();
    const korisnik = Number(localStorage.getItem('idKorisnika'))
    const dodatiProizvodi = basketValue.dodatiProizvodi

    console.log('idDostave: ', deliveryMethodId)
    console.log('cenaDostave: ', deliveryPrice)
    console.log('ukupna: ', subtotal)
    return {
      cenaDostave: deliveryPrice as number,
      ukupnaCena: subtotal as number,
      status: 'shipping',
      datum: new Date().toISOString().substring(0, 10),
      idDostava: Number(deliveryMethodId),
      idKorisnik: korisnik,
      dodatiProizvodi: dodatiProizvodi
    }

  }

}
