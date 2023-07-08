import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminComponent } from './admin/admin.component';
import { SideBarComponent } from './side-bar/side-bar.component';


@NgModule({
  declarations: [
    NavBarComponent,
    AdminComponent,
    SideBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
