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
        public BankAccount(string accountNumber, string ownerName)
        {
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
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            return true;
        }
        public bool TransferTo(BankAccount recipient, decimal amount)
        {
            if (recipient == null || recipient == this)
            {
                return false;
            }
            if (!Withdraw(amount))
            {
                return false;
            }
            recipient.Deposit(amount);
            return true;
        }
    }
}
