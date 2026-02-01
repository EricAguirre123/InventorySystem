import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private apiUrl = 'https://localhost:7090/api/transactions';

  constructor(private http: HttpClient) {}

  create(transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(this.apiUrl, transaction);
  }

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl);
  }
}
