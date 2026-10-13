using MiniBank.ConsoleApp;

BankAccount account = new BankAccount("100001", "Лутфулло");
BankAccount recipient = new BankAccount("100002", "Али");

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
    else if (command == "5")
    {
        if (account.Transactions.Count != 0)
        {
            foreach (Transaction transaction in account.Transactions)
            {
                Console.WriteLine(
                    $"{transaction.CreatedAt.ToLocalTime():dd.MM.yyyy HH:mm:ss} | " +
                    $"{transaction.Type} | {transaction.Amount} сомони");
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