export interface Transaction {
  id?: number;
  productId: number;
  transactionType: string;
  quantity: number;
  unitPrice: number;
  totalPrice?: number;
  transactionDate?: string;
  detail?: string;
}
