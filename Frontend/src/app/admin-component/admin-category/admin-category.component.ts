import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { CategoryDialogComponent } from 'src/app/dialogs/category-dialog/category-dialog.component';
import { Kategorija } from 'src/app/shared/models/kategorija';
import { ShopService } from 'src/app/shop/shop.service';


@Component({
  selector: 'app-admin-category',
  templateUrl: './admin-category.component.html',
  styleUrls: ['./admin-category.component.scss']
})
export class AdminCategoryComponent implements OnInit {
  dataSource: Kategorija[] = []
  subscription!: Subscription
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  selectedCategory: Kategorija | undefined;
  baseUrl = 'https://localhost:7294/api/';

  constructor(private shopService: ShopService, private http: HttpClient, private modalService: BsModalService) { }

  openModal() {
    this.bsModalRef = this.modalService.show(CategoryDialogComponent);
  }

  openEditModal(item: Kategorija) {
    this.bsModalRef = this.modalService.show(CategoryDialogComponent, {
      initialState: {
        idKategorija: item.idKategorija,
        nazivKategorije: item.nazivKategorije,
        isUpdate: true
      }
    });
  }

  ngOnInit(): void {
    this.loadData();
  }


  public loadData() {
    this.subscription = this.shopService.getKategorija().subscribe(
      (response: Kategorija[]) => {
        this.dataSource = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  confirmDelete(element: Kategorija) {
    this.selectedCategory = element;
    this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
  }
  deleteCategory() {
    if (this.selectedCategory) {
      this.shopService.deleteCategory(this.selectedCategory.idKategorija).subscribe(
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
