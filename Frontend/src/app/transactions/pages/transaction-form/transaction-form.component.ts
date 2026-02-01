import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TransactionService } from '../../../core/services/transaction.service';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../core/models/product.model';
import { Transaction } from '../../../core/models/transaction.model';

@Component({
  selector: 'app-transaction-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

  form!: FormGroup;
  products: Product[] = [];

  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private transactionService: TransactionService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadProducts();
  }

  private buildForm(): void {
    this.form = this.fb.group({
      productId: ['', Validators.required],
      transactionType: ['Venta', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [{ value: 0, disabled: true }],
      detail: ['']
    });
  }

  private loadProducts(): void {
    this.productService.getAll().subscribe({
      next: (products: Product[]) => this.products = products,
      error: () => this.error = 'Error al cargar productos'
    });
  }

selectedStock: number = 0;

  onProductChange(): void {
    const productId = Number(this.form.get('productId')!.value);
    const product = this.products.find(p => p.id === productId);

    if (product) {
      this.selectedStock = product.stock; //guardar el stock
      this.form.patchValue({
        unitPrice: product.price
      });
    }
  }

  submit(): void {
  if (this.form.invalid) {
    this.form.markAllAsTouched();
    return;
  }

  this.loading = true;
  this.error = null;

  const raw = this.form.getRawValue();

  // VALIDACIÓN DE STOCK EN FRONTEND
  if (
    raw.transactionType === 'Venta' &&
    raw.quantity > this.selectedStock
  ) {
    this.error = `Stock insuficiente. Disponible: ${this.selectedStock}`;
    return;
  }

  this.loading = true;
  this.error = null;

  const transaction: Transaction = {
    productId: Number(raw.productId),
    transactionType: raw.transactionType,
    quantity: Number(raw.quantity),
    unitPrice: Number(raw.unitPrice),
    detail: raw.detail
  };

  this.transactionService.create(transaction).subscribe({
    next: () => this.router.navigate(['/transactions']),
    error: err => {
      this.error = err?.error?.message || 'Error al registrar transacción';
      this.loading = false;
    }
  });
}
  cancel(): void {
    this.router.navigate(['/transactions']);
  }
}
