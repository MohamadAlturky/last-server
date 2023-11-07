interface AccountTransactions {
  size: number;
  totalBalance: number;
  accountTransactions: AccountTransaction[];
}
interface AccountTransaction {
  type: string;
  value: number;
  createdAtDay: string;
  date: string;
  by:string
}
