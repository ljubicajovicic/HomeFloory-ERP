import { HttpClient } from '@angular/common/http';
import { Component, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { ProducerDialogComponent } from 'src/app/dialogs/producer-dialog/producer-dialog.component';
import { Proizvodjac } from 'src/app/shared/models/proizvodjac';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-admin-producer',
  templateUrl: './admin-producer.component.html',
  styleUrls: ['./admin-producer.component.scss']
})
export class AdminProducerComponent {
  dataSource: Proizvodjac[] = []
  subscription!: Subscription
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  selected: Proizvodjac | undefined;
  baseUrl = 'https://localhost:7294/api/';

  constructor(private shopService: ShopService, private http: HttpClient, private modalService: BsModalService) {

  }

  openModal() {
    this.bsModalRef = this.modalService.show(ProducerDialogComponent);
  }

  openEditModal(item: Proizvodjac) {
    this.bsModalRef = this.modalService.show(ProducerDialogComponent, {
      initialState: {
        idProizvodjac: item.idProizvodjac,
        nazivProizvodjaca: item.nazivProizvodjaca,
        isUpdate: true
      }
    });
  }

  ngOnInit(): void {
    this.loadData();
    //console.log(this.dataSource);
  }

  public loadData() {
    this.subscription = this.shopService.getProizvodjac().subscribe(
      (response: Proizvodjac[]) => {
        this.dataSource = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  confirmDelete(element: Proizvodjac) {
    this.selected = element;
    this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
  }
  deleteProducer() {
    if (this.selected) {
      this.shopService.deleteProducer(this.selected.idProizvodjac).subscribe(
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

