using MiniBank.ConsoleApp;

BankAccount account = new BankAccount("100001", "Лутфулло");
BankAccount recipient1 = new BankAccount("100002", "Али");
BankAccount recipient2 = new BankAccount("100003", "Амин");

Dictionary<string, BankAccount> accounts = new()
{
    [account.AccountNumber] = account,
    [recipient1.AccountNumber] = recipient1,
    [recipient2.AccountNumber] = recipient2
};

Dictionary<int, string> commands = new()
{
    [1] = "Баланс",
    [2] = "Пополнение",
    [3] = "Снятие",
    [4] = "Перевод",
    [5] = "История",
    [0] = "Выход"
};

while (true)
{
    Console.WriteLine("\nМеню:");
    foreach (var com in commands)
    {
        Console.WriteLine($"{com.Key} — {com.Value}");
    }

    Console.Write("Выберите пункт: ");
    string? command = Console.ReadLine();

    if (command == null || command == "0")
    {
        break;
    }
    else if (command == "1")
    {
        Console.WriteLine($"Баланс: {account.Balance} сомони");
    }
    else if (command == "2")
    {
        Console.Write("Введите сумму пополнения: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            bool success = account.Deposit(amount);
            Console.WriteLine(success
                ? "Счёт успешно пополнен"
                : "Сумма должна быть больше нуля");
        }
        else
        {
            Console.WriteLine("Введите корректное число");
        }
    }
    else if (command == "3")
    {
        Console.Write("Введите сумму снятия: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            bool success = account.Withdraw(amount);
            Console.WriteLine(success
                ? "Деньги успешно сняты"
                : "Сумма должна быть больше нуля и не превышать баланс");
        }
        else
        {
            Console.WriteLine("Введите корректное число");
        }
    }
    else if (command == "4")
    {
        Console.WriteLine("Введите номер счёта получателя: ");

        string? accountNumber = Console.ReadLine();
        if (accountNumber == null)
        {
            break;
        }

        if (accounts.TryGetValue(accountNumber, out BankAccount? recipient))
        {
            Console.WriteLine($"Получатель: {recipient.OwnerName}, счёт {recipient.AccountNumber}");

            Console.Write("Введите сумму перевода: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bool success = account.TransferTo(recipient, amount);
                Console.WriteLine(success
                    ? "Перевод выполнен"
                    : "Перевод отклонён: проверьте сумму и баланс");
            }
            else
            {
                Console.WriteLine("Введите корректное число");
            }
        }
        else
        {
            Console.WriteLine("Счёт получателя не найден");
            continue;
        }
    }
    else if (command == "5")
    {
        Console.WriteLine($"{account.OwnerName} - {account.Balance} сомони");
        if (account.Transactions.Count != 0)
        {
            foreach (Transaction transaction in account.Transactions)
            {
                if (transaction.RelatedAccountNumber == null)
                {
                    Console.WriteLine(
                    $"{transaction.CreatedAt.ToLocalTime():dd.MM.yyyy HH:mm:ss} | " +
                    $"{transaction.Type} | {transaction.Amount} сомони");
                }
                else
                {
                    Console.WriteLine(
                    $"{transaction.CreatedAt.ToLocalTime():dd.MM.yyyy HH:mm:ss} | " +
                    $"{transaction.Type} | {transaction.Amount} сомони | Связанный счёт: {transaction.RelatedAccountNumber}");
                }
            }
        }
        else
        {
            Console.WriteLine("История операций пуста");
        }
    }
    else
    {
        Console.WriteLine("Неизвестный пункт меню");
    }
}