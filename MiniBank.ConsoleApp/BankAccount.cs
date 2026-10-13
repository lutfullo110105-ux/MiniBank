using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBank.ConsoleApp
{
    internal class BankAccount
    {
        public string AccountNumber { get; }
        public string OwnerName { get; }
        public decimal Balance { get; private set; }
        private readonly List<Transaction> transactions = new List<Transaction>();

        public IReadOnlyList<Transaction> Transactions => transactions.AsReadOnly();
        public BankAccount(string accountNumber, string ownerName)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Номер счёта не должен быть пустым");
            }
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException("Имя владельца не должно быть пустым");
            }
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = 0m;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            Balance += amount;
            transactions.Add(new Transaction("Пополнение", amount));
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            transactions.Add(new Transaction("Снятие", amount));
            return true;
        }
        public bool TransferTo(BankAccount recipient, decimal amount)
        {
            if (recipient == null || recipient == this)
            {
                return false;
            }
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            recipient.Balance += amount;
            transactions.Add(new Transaction("Исходящий перевод", amount, recipient.AccountNumber));
            recipient.transactions.Add(new Transaction("Входящий перевод", amount, AccountNumber));
            return true;
        }
    }
}
