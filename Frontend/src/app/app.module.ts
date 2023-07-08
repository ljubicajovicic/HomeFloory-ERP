import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { AdminProductComponent } from './admin-component/admin-product/admin-product.component';
import { ProductDialogComponent } from './dialogs/product-dialog/product-dialog.component';
import { AdminCategoryComponent } from './admin-component/admin-category/admin-category.component';
import { AdminProducerComponent } from './admin-component/admin-producer/admin-producer.component';
import { AdminCustomerComponent } from './admin-component/admin-customer/admin-customer.component';
import { AdminOrdersComponent } from './admin-component/admin-orders/admin-orders.component';
import { CategoryDialogComponent } from './dialogs/category-dialog/category-dialog.component';
import { ProducerDialogComponent } from './dialogs/producer-dialog/producer-dialog.component';
import { AdminDialogComponent } from './dialogs/admin-dialog/admin-dialog.component';
import { AccountService } from './account/account.service';
//import { JwtInterceptor } from './core/interceptors/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AdminProductComponent,
    ProductDialogComponent,
    AdminCategoryComponent,
    AdminProducerComponent,
    AdminCustomerComponent,
    AdminOrdersComponent,
    CategoryDialogComponent,
    ProducerDialogComponent,
    AdminDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    ModalModule.forRoot(),
    FormsModule
  ],
  providers: [
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
