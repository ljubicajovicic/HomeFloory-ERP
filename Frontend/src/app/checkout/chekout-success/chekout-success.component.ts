import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Korpa } from 'src/app/shared/models/korpa';

@Component({
  selector: 'app-chekout-success',
  templateUrl: './chekout-success.component.html',
  styleUrls: ['./chekout-success.component.scss']
})
export class ChekoutSuccessComponent {
  order?: Korpa

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.order = navigation?.extras?.state as Korpa
  }

}
