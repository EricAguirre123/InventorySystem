import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../core/models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  form!: FormGroup;
  isEditMode = false;
  productId!: number;

  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.productId = +id;
      this.loadProduct();
    }
  }

  private buildForm(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      category: [''],
      imageUrl: [''],
      price: [0, [Validators.required, Validators.min(0)]],
      stock: [0, [Validators.required, Validators.min(0)]]
    });
  }

  private loadProduct(): void {
    this.loading = true;

    this.productService.getById(this.productId).subscribe({
      next: (product) => {
        this.form.patchValue(product);
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar el producto';
        this.loading = false;
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = null;

    const product: Product = this.form.value;

    if (this.isEditMode) {
      this.productService.update(this.productId, product).subscribe({
        next: () => this.router.navigate(['/products']),
        error: () => {
          this.error = 'Error al actualizar producto';
          this.loading = false;
        }
      });
    } else {
      this.productService.create(product).subscribe({
        next: () => this.router.navigate(['/products']),
        error: () => {
          this.error = 'Error al crear producto';
          this.loading = false;
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }

  get productForm(): FormGroup {
    return this.form;
  }

  save(): void {
    this.submit();
  }
}


