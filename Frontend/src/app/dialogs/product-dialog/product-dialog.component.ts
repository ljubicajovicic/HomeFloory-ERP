import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ShopService } from 'src/app/shop/shop.service';
import { Location } from '@angular/common';

export interface ProizvodAdd {
  idProizvod: number
  naziv: string
  opis: string
  kolicinaNaStanju: number
  cenaPoM2: number
  paketPoM2: number
  dimenzija: string
  nijansa: string
  urlSlike: string
  idKategorija: number
  idProizvodjac: number
}

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent {
  idProizvod!: number
  naziv!: string
  opis!: string
  kolicinaNaStanju!: number
  cenaPoM2!: number
  paketPoM2!: number
  dimenzija!: string
  nijansa!: string
  urlSlike!: string
  idKategorija!: number
  idProizvodjac!: number
  isUpdate = false

  constructor(public location: Location, public bsModalRef: BsModalRef, public shopService: ShopService, public router: Router) { }

  ngOnInit() {

    if (this.isUpdate) {
      this.naziv = this.naziv
      this.opis = this.opis
      this.kolicinaNaStanju = this.kolicinaNaStanju
      this.cenaPoM2 = this.cenaPoM2
      this.paketPoM2 = this.paketPoM2
      this.dimenzija = this.dimenzija
      this.nijansa = this.nijansa
      this.urlSlike = this.urlSlike
      this.idKategorija = this.idKategorija
      this.idProizvodjac = this.idProizvodjac
    } else {
      this.clearFormFields();
    }
  }

  clearFormFields() {
    this.naziv
    this.opis
    this.kolicinaNaStanju
    this.cenaPoM2
    this.paketPoM2
    this.dimenzija
    this.nijansa
    this.urlSlike
    this.idKategorija
    this.idProizvodjac
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  addProduct() {
    const proizvod = {
      naziv: this.naziv,
      opis: this.opis,
      kolicinaNaStanju: this.kolicinaNaStanju,
      cenaPoM2: this.cenaPoM2,
      paketPoM2: this.paketPoM2,
      dimenzija: this.dimenzija,
      nijansa: this.nijansa,
      urlSlike: this.urlSlike,
      idKategorija: this.idKategorija,
      idProizvodjac: this.idProizvodjac,
    };
    this.shopService.addProduct(proizvod).subscribe(
      response => {
        this.closeModal();

        this.location.back();
      },
      error => {
        console.log('Error:', error);
        console.log(proizvod)
      }
    );
  }

  updateProduct() {
    if (this.idProizvod) {
      const proizvod = {
        naziv: this.naziv,
        opis: this.opis,
        kolicinaNaStanju: this.kolicinaNaStanju,
        cenaPoM2: this.cenaPoM2,
        paketPoM2: this.paketPoM2,
        dimenzija: this.dimenzija,
        nijansa: this.nijansa,
        urlSlike: this.urlSlike,
        idKategorija: this.idKategorija,
        idProizvodjac: this.idProizvodjac,
      };
      this.shopService.updateProduct(proizvod, this.idProizvod).subscribe(
        response => {
          console.log(response)
          this.closeModal();

          this.location.back();
        },
        error => {
          console.log('Greska:', error);
          console.log(proizvod)
        }
      );
    }
  }

  submitForm() {
    if (this.isUpdate) {
      this.updateProduct();
    } else {
      this.addProduct();
    }
    this.closeModal();
  }



}
