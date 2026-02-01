import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../core/models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = null;

    this.productService.getAll().subscribe({
      next: (data: Product[]) => {
        this.products = data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar productos';
        this.loading = false;
      }
    });
  }

  createProduct(): void {
    this.router.navigate(['/products/new']);
  }

  editProduct(id: number): void {
    this.router.navigate(['/products/edit', id]);
  }

  deleteProduct(id: number): void {
    if (!confirm('¿Seguro que deseas eliminar este producto?')) return;

    this.productService.delete(id).subscribe({
      next: () => this.loadProducts(),
      error: () => alert('Error al eliminar producto')
    });
  }

  // ✅ MÉTODO QUE FALTABA
  goToTransactions(): void {
    this.router.navigate(['/transactions']);
  }
}
