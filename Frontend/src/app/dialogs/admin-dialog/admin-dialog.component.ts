import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ShopService } from 'src/app/shop/shop.service';
import { Router } from '@angular/router';

export interface KorisnikAdmin {
  email: string
}

@Component({
  selector: 'app-admin-dialog',
  templateUrl: './admin-dialog.component.html',
  styleUrls: ['./admin-dialog.component.scss']
})
export class AdminDialogComponent {

  email!: string;

  constructor(public location: Location, public bsModalRef: BsModalRef, public shopService: ShopService, public router: Router) { }
  closeModal() {
    this.bsModalRef.hide();
  }

  addAdmin() {
    const admin = {
      email: this.email,
    };
    this.shopService.addAdmin(admin).subscribe(
      response => {
        this.closeModal();

        this.location.back();
      },
      error => {
        console.log('Error:', error);
        console.log(admin)
      }
    );
  }

  submitForm() {

    this.addAdmin();
    this.closeModal();
  }
}
