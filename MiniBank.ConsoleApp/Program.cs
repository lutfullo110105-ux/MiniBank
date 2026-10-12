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