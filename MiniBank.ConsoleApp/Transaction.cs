using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBank.ConsoleApp
{
    internal class Transaction
    {
        public string Type { get; }
        public decimal Amount { get; }
        public DateTime CreatedAt { get; }

        public Transaction(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
