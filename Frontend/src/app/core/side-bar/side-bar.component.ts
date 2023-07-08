import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {
  sidebarlist: any = [
    {
      label: "Proizvod",
      link: "/product",
      active: true
    },
    {
      label: "Kategorija",
      link: "/category",
      active: true
    },
    {
      label: "Proizvodjac",
      link: "/producer",
      active: true
    },
    {
      label: "Korisnik",
      link: "/customer",
      active: true
    },
    {
      label: "Porudzbine",
      link: "/order",
      active: true
    }
  ]
  ngOnInit(): void {
    //throw new Error('Method not implemented.');
  }

  changeRoute(index: number) {
    this.sidebarlist.forEach((item: any, i: number) => {
      this.sidebarlist[i].active = false;
    });
    this.sidebarlist[index].active = true;
  }
}
