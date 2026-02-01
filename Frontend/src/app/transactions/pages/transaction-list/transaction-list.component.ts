import { Component, OnInit } from '@angular/core';
import { CommonModule} from '@angular/common';
import { Router } from '@angular/router';
import { TransactionService } from '../../../core/services/transaction.service';
import { Transaction } from '../../../core/models/transaction.model';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {

  transactions: Transaction[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private transactionService: TransactionService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  //boton de inicio
  goHome(): void {
  this.router.navigate(['/products']);
}


  loadTransactions(): void {
    this.loading = true;
    this.error = null;

    this.transactionService.getAll().subscribe({
      next: (data: Transaction[]) => {
        this.transactions = data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar transacciones';
        this.loading = false;
      }
    });
  }

  newTransaction(): void {
    this.router.navigate(['/transactions/new']);
  }

  goToProducts(): void {
  this.router.navigate(['/products']);
}


}
