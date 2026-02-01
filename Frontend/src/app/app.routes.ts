import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'products',
    loadComponent: () =>
      import('./products/pages/product-list/product-list.component')
        .then(m => m.ProductListComponent)
  },
  {
    path: 'products/new',
    loadComponent: () =>
      import('./products/pages/product-form/product-form.component')
        .then(m => m.ProductFormComponent)
  },
  {
    path: 'products/edit/:id',
    loadComponent: () =>
      import('./products/pages/product-form/product-form.component')
        .then(m => m.ProductFormComponent)
  },
  {
    path: '',
    redirectTo: 'products',
    pathMatch: 'full'
  },
  {
  path: 'transactions',
  loadComponent: () =>
    import('./transactions/pages/transaction-list/transaction-list.component')
      .then(m => m.TransactionListComponent)
},
{
  path: 'transactions/new',
  loadComponent: () =>
    import('./transactions/pages/transaction-form/transaction-form.component')
      .then(m => m.TransactionFormComponent)
},
{
    path: '',
    redirectTo: 'products',
    pathMatch: 'full'
  }

];
