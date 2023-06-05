import { NgModule } from '@angular/core';
import { CheckoutComponent } from './checkout.component';
import { RouterModule, Routes } from '@angular/router';
import { ChekoutSuccessComponent } from './chekout-success/chekout-success.component';

const routes: Routes = [
  { path: '', component: CheckoutComponent },
  { path: 'success', component: ChekoutSuccessComponent }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ], exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }
