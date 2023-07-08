import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  registerForm = this.fb.group({
    ime: ['', Validators.required],
    prezime: ['', Validators.required],
    datumRodjenja: ['', [Validators.required]],
    kontakt: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    lozinka: ['', Validators.required],
  })

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: (error) => {
        console.log('Registration failed:', error);
      }
    })
  }


}
