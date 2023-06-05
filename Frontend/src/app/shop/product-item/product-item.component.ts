import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { DodatiProizvodi } from 'src/app/shared/models/korpa';
import { Proizvod } from 'src/app/shared/models/proizvod';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Proizvod;
  quantity: number = 1;

  constructor(private basketService: BasketService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    /*this.basketService.createBasket().subscribe(
      basketId => {
        console.log('Basket created with ID:', basketId);
        // Perform any additional actions after creating the basket
      },
      error => {
        console.error('Failed to create basket:', error);
        // Handle error scenario
      }
    );*/
  }

  /*addToBasket(): void {
    const basketId = localStorage.getItem('basket_id'); // Example basket id, replace with your actual basket id
    if (this.product) { this.basketService.addProductToBasket(this.product, this.quantity)
      .subscribe(
        basket => {
          // Handle success
          console.log('Product added to the basket:', basket);
        },
        error => {
          // Handle error
          console.error('Failed to add product to the basket:', error);
        }
      );}
      else if (this.product && basketId) this.basketService.updateBasket()
  }*/

  /*addToBasket(): void {
    const basketId = Number(localStorage.getItem('basketId'));

    if (this.product && basketId) {
      // Check if the product exists in the basket
      this.basketService.getProductFromBasket(this.product.idProizvod, basketId)
        .then((existingProduct: DodatiProizvodi | undefined) => {
          if (existingProduct) {
            // Update the quantity of the existing product in the basket
            const newQuantity = existingProduct.kolicina + this.quantity;
            this.basketService.updateProductQuantityInBasket(existingProduct.idProizvod, basketId, newQuantity)
              .subscribe(
                basket => {
                  // Handle success
                  console.log('Product quantity updated in the basket:', basket);
                },
                error => {
                  // Handle error
                  console.error('Failed to update product quantity in the basket:', error);
                }
              );
          } else {
            // Add the product to the basket
            this.basketService.addProductToBasket(this.product as Proizvod, this.quantity)
              .subscribe(
                basket => {
                  // Handle success
                  console.log('Product added to the basket:', basket);
                },
                error => {
                  // Handle error
                  console.error('Failed to add product to the basket:', error);
                }
              );
          }
        })
        .catch(error => {
          console.error('Failed to retrieve product from the basket:', error);
        });
    } else {
      console.error('Basket ID not found or product is not defined');
    }
  }
*/



  addItemToBasket() {
    this.product && this.basketService.addItemToBasket(this.product);
  }
}
