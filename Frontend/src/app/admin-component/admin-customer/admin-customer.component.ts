import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { AdminDialogComponent } from 'src/app/dialogs/admin-dialog/admin-dialog.component';
import { Korisnik, KorisnikAdmin } from 'src/app/shared/models/korisnik';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-admin-customer',
  templateUrl: './admin-customer.component.html',
  styleUrls: ['./admin-customer.component.scss']
})
export class AdminCustomerComponent {
  dataSource: Korisnik[] = []
  subscription!: Subscription
  bsModalRef: BsModalRef | undefined;
  selected: KorisnikAdmin | undefined;
  baseUrl = 'https://localhost:7294/api/';

  constructor(private shopService: ShopService, private http: HttpClient, private modalService: BsModalService) { }


  openModal() {
    this.bsModalRef = this.modalService.show(AdminDialogComponent);
  }

  openEditModal(item: KorisnikAdmin) {
    this.bsModalRef = this.modalService.show(AdminDialogComponent, {
      initialState: {
        email: item.email
      }
    });
  }

  ngOnInit(): void {
    this.loadData();
    //console.log(this.dataSource);
  }

  public loadData() {
    this.subscription = this.shopService.getKorisnik().subscribe(
      (response: Korisnik[]) => {
        this.dataSource = response;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
