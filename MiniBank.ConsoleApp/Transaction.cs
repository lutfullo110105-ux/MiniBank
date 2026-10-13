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
        public string? RelatedAccountNumber { get; }

        public Transaction(string type, decimal amount, string? relatedAccountNumber = null)
        {
            Type = type;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
            RelatedAccountNumber = relatedAccountNumber;
        }
    }
}
