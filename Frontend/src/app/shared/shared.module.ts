import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputsComponent } from './components/text-inputs/text-inputs.component';
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import { StepperComponent } from './components/stepper/stepper.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { CarouselModule } from 'ngx-bootstrap/carousel';

@NgModule({
  declarations: [
    TextInputsComponent,
    OrderTotalsComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule,
    RouterModule,
    CarouselModule.forRoot()
  ],
  exports: [
    PaginationModule,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputsComponent,
    OrderTotalsComponent,
    StepperComponent,
    CdkStepperModule,
    BasketSummaryComponent,
    CarouselModule
  ]
})
export class SharedModule { }
