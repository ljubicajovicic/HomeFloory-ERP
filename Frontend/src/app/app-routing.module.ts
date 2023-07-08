import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { AdminComponent } from './core/admin/admin.component';
import { AdminProductComponent } from './admin-component/admin-product/admin-product.component';
import { AdminCategoryComponent } from './admin-component/admin-category/admin-category.component';
import { AdminProducerComponent } from './admin-component/admin-producer/admin-producer.component';
import { AdminCustomerComponent } from './admin-component/admin-customer/admin-customer.component';
import { AdminOrdersComponent } from './admin-component/admin-orders/admin-orders.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'admin',
    canActivate: [AuthGuard],
    component: AdminComponent,
    children: [
      { path: "product", component: AdminProductComponent },
      { path: "category", component: AdminCategoryComponent },
      { path: "producer", component: AdminProducerComponent },
      { path: "customer", component: AdminCustomerComponent },
      { path: "order", component: AdminOrdersComponent }
    ]
  },
  {
    path: 'orders',
    canActivate: [AuthGuard],
    loadChildren: () => import('./orders/orders.module').then(mod => mod.OrdersModule), data: { breadcrumb: 'Orders' }
  },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule) },
  { path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule) },
  { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
  {
    path: 'checkout',
    canActivate: [AuthGuard],
    loadChildren: () => import('./checkout/checkout.module').then(m => m.CheckoutModule)
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
