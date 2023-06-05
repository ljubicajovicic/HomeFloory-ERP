import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-chekout-adress',
  templateUrl: './chekout-adress.component.html',
  styleUrls: ['./chekout-adress.component.scss']
})
export class ChekoutAdressComponent {
  @Input() checkoutForm?: FormGroup;

  @Output() submitForm: EventEmitter<void> = new EventEmitter<void>();

  onSubmit() {
    this.submitForm.emit();
  }
}
