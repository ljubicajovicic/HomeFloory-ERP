import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountModule } from '../account/account.module';
import { AccountService } from '../account/account.service';
import { AdresaIsporuke } from '../shared/models/korisnik';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountService: AccountService
    , private basketService: BasketService) { }

  ngOnInit(): void {
  }

  checkoutForm = this.fb.group({
    addressForm: this.fb.group({
      grad: ['', Validators.required],
      drzava: ['', Validators.required],
      ulica: ['', Validators.required],
      postanskiBroj: ['', Validators.required]
    }),
    deliveryForm: this.fb.group({
      tipDostave: ['', Validators.required]
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required]
    })
  })

  /*getAddressFormValues() {
    this.accountService.getUserAddress().subscribe({
      next: address => {
        address && this.checkoutForm.get('addressForm')?.patchValue(address)
      }
    })
  }*/

  onFormSubmit() {
    const addressForm = this.checkoutForm.get('addressForm');
    if (addressForm && addressForm.valid) {
      const address: AdresaIsporuke = {
        grad: addressForm.value.grad,
        drzava: addressForm.value.drzava,
        ulica: addressForm.value.ulica,
        postanskiBroj: addressForm.value.postanskiBroj
      };

      this.accountService.addUserAddress(address).subscribe(
        (addedAddressId: number) => {
          // Address added successfully
          console.log('Address added successfully', addedAddressId);
          // Perform any necessary actions after address is added
        },
        (error) => {
          // Handle the error appropriately
          console.error('Failed to add address:', error);
        }
      );
    }
  }


}
