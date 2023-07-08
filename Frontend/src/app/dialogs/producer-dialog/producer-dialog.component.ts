import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ShopService } from 'src/app/shop/shop.service';
import { Router } from '@angular/router';

export interface ProizvodjacAdd {
  idProizvodjac: number
  nazivProizvodjaca: string
}

@Component({
  selector: 'app-producer-dialog',
  templateUrl: './producer-dialog.component.html',
  styleUrls: ['./producer-dialog.component.scss']
})
export class ProducerDialogComponent implements OnInit {
  idProizvodjac!: number;
  nazivProizvodjaca!: string;
  isUpdate = false;

  constructor(public location: Location, public bsModalRef: BsModalRef, public shopService: ShopService, public router: Router) { }

  ngOnInit() {

    if (this.isUpdate) {
      this.nazivProizvodjaca = this.nazivProizvodjaca;
    } else {
      this.clearFormFields();
    }
  }

  clearFormFields() {
    this.nazivProizvodjaca;
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  addProducer() {
    const proizvodjac = {
      nazivProizvodjaca: this.nazivProizvodjaca,
    };
    this.shopService.addProducer(proizvodjac).subscribe(
      response => {
        this.closeModal();

        this.location.back();
      },
      error => {
        console.log('Error:', error);
        console.log(proizvodjac)
      }
    );
  }

  updateProducer() {
    if (this.idProizvodjac) {
      const proizvodjac = {
        nazivProizvodjaca: this.nazivProizvodjaca,
      };
      this.shopService.updateProducer(proizvodjac, this.idProizvodjac).subscribe(
        response => {
          this.closeModal();

          this.location.back();
        },
        error => {
          console.log('Greska:', error);
          console.log(proizvodjac)
        }
      );
    }
  }

  submitForm() {
    if (this.isUpdate) {
      this.updateProducer();
    } else {
      this.addProducer();
    }
    this.closeModal();
  }

}
