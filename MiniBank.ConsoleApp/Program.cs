using MiniBank.ConsoleApp;

BankAccount account = new BankAccount("100001", "Lutfullo");

Console.WriteLine($"Счёт: {account.AccountNumber}");
Console.WriteLine($"Владелец: {account.OwnerName}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Пополнение на 100: {account.Deposit(100m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Пополнение на -20: {account.Deposit(-20m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Пополнение на 0: {account.Deposit(0m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Снятие 30: {account.Withdraw(30m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Снятие 80: {account.Withdraw(80m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

Console.WriteLine($"Снятие 70: {account.Withdraw(70m)}");
Console.WriteLine($"Баланс: {account.Balance} TJS");

BankAccount sender = new BankAccount("100002", "Ali");
BankAccount recipient = new BankAccount("100003", "Bob");

sender.Deposit(100m);

Console.WriteLine($"Перевод 150: {sender.TransferTo(recipient, 150m)}");
Console.WriteLine($"Ali: {sender.Balance}, Bob: {recipient.Balance}");

Console.WriteLine($"Перевод 30: {sender.TransferTo(recipient, 30m)}");
Console.WriteLine($"Ali: {sender.Balance}, Bob: {recipient.Balance}");

Console.WriteLine($"Перевод себе 10: {sender.TransferTo(sender, 10m)}");
Console.WriteLine($"Ali: {sender.Balance}, Bob: {recipient.Balance}");