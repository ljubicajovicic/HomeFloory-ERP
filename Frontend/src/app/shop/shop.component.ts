import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Proizvod } from '../shared/models/proizvod';
import { ShopService } from './shop.service';
import { Kategorija } from '../shared/models/kategorija';
import { Proizvodjac } from '../shared/models/proizvodjac';
import { ShopParam } from '../shared/models/shopParam';
import { CountParam } from '../shared/models/countParam';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  proizvod: Proizvod[] = [];
  kategorija: Kategorija[] = [];
  proizvodjac: Proizvodjac[] = [];
  shopParam = new ShopParam();
  sortOptions = [
    { name: 'sortBy', value: 'Cena-rastuce' },
    { name: 'sortBy', value: 'Cena-opadajuce' }
  ]
  total = 0
  constructor(private shopService: ShopService) {

  }

  ngOnInit(): void {
    this.getProizvod();
    this.getKategorija();
    this.getProizvodjac();
  }

  getProizvod() {
    this.shopService.getProizvod(this.shopParam).subscribe({
      next: response => {
        this.proizvod = response.data;
        this.total = response.total;
      },
      error: error => console.log(error)
    })
  }

  getKategorija() {
    this.shopService.getKategorija().subscribe({
      next: response => this.kategorija = [{ idKategorija: 0, nazivKategorije: 'Svi' }, ...response],
      error: error => console.log(error)
    })
  }

  getProizvodjac() {
    this.shopService.getProizvodjac().subscribe({
      next: response => this.proizvodjac = [{ idProizvodjac: 0, nazivProizvodjaca: 'Svi' }, ...response],
      error: error => console.log(error)
    })
  }

  onProizvodjacSelected(idProizvodjac: number) {
    this.shopParam.idProizvodjac = idProizvodjac;
    this.shopParam.pageNumber = 1;
    this.getProizvod();
  }

  onKategorijaSelected(idKategorija: number) {
    this.shopParam.idKategorija = idKategorija;
    this.shopParam.pageNumber = 1;
    this.getProizvod();
  }

  onSortSelected(event: any) {
    this.shopParam.sort = event.target.value;
    this.getProizvod();
  }

  onPageChanged(event: any) {
    if (this.shopParam.pageNumber !== event.page) {
      this.shopParam.pageNumber = event.page;
      this.getProizvod();
    }
  }

  onSearch() {
    this.shopParam.search = this.searchTerm?.nativeElement.value;
    this.shopParam.pageNumber = 1;
    this.getProizvod();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParam = new ShopParam();
    this.getProizvod();
  }

}
