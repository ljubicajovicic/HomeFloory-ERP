import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { Subscription } from 'rxjs';
import { ShopService } from 'src/app/shop/shop.service';
import { HttpClient } from '@angular/common/http';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ProductDialogComponent } from 'src/app/dialogs/product-dialog/product-dialog.component';


@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.scss']
})
export class AdminProductComponent implements OnInit {
  dataSource: Proizvod[] = []
  subscription!: Subscription
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  selected: Proizvod | undefined;
  baseUrl = 'https://localhost:7294/api/';

  constructor(private shopService: ShopService, private http: HttpClient, private modalService: BsModalService) { }

  openModal() {
    this.bsModalRef = this.modalService.show(ProductDialogComponent);
  }

  openEditModal(item: Proizvod) {
    this.bsModalRef = this.modalService.show(ProductDialogComponent, {
      initialState: {
        idProizvod: item.idProizvod,
        naziv: item.naziv,
        opis: item.opis,
        cenaPoM2: item.cenaPoM2,
        paketPoM2: item.paketPoM2,
        dimenzija: item.dimenzija,
        nijansa: item.nijansa,
        urlSlike: item.urlSlike,
        idKategorija: item.idKategorija,
        idProizvodjac: item.idProizvodjac,
        isUpdate: true
      }
    });
  }

  ngOnInit(): void {
    this.loadData();
    //console.log(this.dataSource);
  }


  public loadData() {
    this.subscription = this.shopService.getProductsNoParams().subscribe(
      (response: Proizvod[]) => {
        this.dataSource = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  confirmDelete(element: Proizvod) {
    this.selected = element;
    this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
  }
  deleteProduct() {
    if (this.selected) {
      this.shopService.deleteProduct(this.selected.idProizvod).subscribe(
        response => {
          this.loadData();
        },
        error => {
          console.log("Error")
        }
      );
    }

    this.bsModalRef?.hide();
  }
  closeDeleteConfirmationModal() {
    this.bsModalRef?.hide();
  }
  closeModal() {
    this.bsModalRef?.hide();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
