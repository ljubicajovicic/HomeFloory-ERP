import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ShopService } from 'src/app/shop/shop.service';
import { Location } from '@angular/common';

import { Router } from '@angular/router';

export interface KategorijaAdd {
  idKategorija: number
  nazivKategorije: string
}

@Component({
  selector: 'app-category-dialog',
  templateUrl: './category-dialog.component.html',
  styleUrls: ['./category-dialog.component.scss']
})
export class CategoryDialogComponent implements OnInit {

  idKategorija!: number;
  nazivKategorije!: string;
  isUpdate = false;

  constructor(public location: Location, public bsModalRef: BsModalRef, public shopService: ShopService, public router: Router) { }

  ngOnInit() {

    if (this.isUpdate) {
      this.nazivKategorije = this.nazivKategorije;
    } else {
      this.clearFormFields();
    }
  }

  clearFormFields() {
    this.nazivKategorije;
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  addCategory() {
    const kategorija = {
      nazivKategorije: this.nazivKategorije,
    };
    this.shopService.addCategory(kategorija).subscribe(
      response => {
        this.closeModal();

        this.location.back();
      },
      error => {
        console.log('Error:', error);
        console.log(kategorija)
      }
    );
  }

  updateCategory() {
    if (this.idKategorija) {
      const kategorija = {
        nazivKategorije: this.nazivKategorije,
      };
      this.shopService.updateCategory(kategorija, this.idKategorija).subscribe(
        response => {
          this.closeModal();

          this.location.back();
        },
        error => {
          console.log('Greska:', error);
          console.log(kategorija)
        }
      );
    }
  }

  submitForm() {
    if (this.isUpdate) {
      this.updateCategory();
    } else {
      this.addCategory();
    }
    this.closeModal();
  }


}
