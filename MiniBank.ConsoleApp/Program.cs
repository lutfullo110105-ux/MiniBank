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
    [6] = "Создать счёт",
    [7] = "Сменить текущий счёт",
    [8] = "Список счетов",
    [9] = "История по типу операции",
    [0] = "Выход"
};

while (true)
{
    Console.WriteLine($"\nТекущий счёт: {account.OwnerName} | {account.AccountNumber} | " +
        $"Баланс: {account.Balance} сомони");

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
        if (TryReadAmount("Введите сумму пополнения: ", out decimal amount))
        {
            bool success = account.Deposit(amount);
            Console.WriteLine(success
                ? "Счёт успешно пополнен"
                : "Сумма должна быть больше нуля");
        }
    }
    else if (command == "3")
    {
        if (TryReadAmount("Введите сумму снятия: ", out decimal amount))
        {
            bool success = account.Withdraw(amount);
            Console.WriteLine(success
                ? "Деньги успешно сняты"
                : "Сумма должна быть больше нуля и не превышать баланс");
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

            if (TryReadAmount("Введите сумму перевода: ", out decimal amount))
            {
                bool success = account.TransferTo(recipient, amount);
                Console.WriteLine(success
                    ? "Перевод выполнен"
                    : "Перевод отклонён: проверьте сумму и баланс");
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
        ShowHistory(account);
    }
    else if (command == "6")
    {
        Console.Write("Введите номер нового счёта: ");
        string? newAccountNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newAccountNumber))
        {
            Console.WriteLine("Номер счёта не должен быть пустым");
            continue;
        }
        else if (accounts.ContainsKey(newAccountNumber))
        {
            Console.WriteLine("Счёт с таким номером уже существует");
            continue;
        }

        Console.Write("Введите имя владельца: ");
        string? newOwnerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newOwnerName))
        {
            Console.WriteLine("Имя владельца не должно быть пустым");
            continue;
        }

        accounts.Add(newAccountNumber, new BankAccount(newAccountNumber, newOwnerName));
        Console.WriteLine("Счёт успешно создан");
    }
    else if (command == "7")
    {
        Console.Write("Введите номер счета: ");
        string? accountNumber = Console.ReadLine();
        if (accountNumber == null)
        {
            break;
        }
        if (accounts.TryGetValue(accountNumber, out BankAccount? selectedAccount))
        {
            account = selectedAccount;
            Console.WriteLine($"Имя владельца счета: {account.OwnerName}");
        }
        else
        {
            Console.WriteLine("Счёт не найден");
        }
    }
    else if (command == "8")
    {
        foreach (var item in accounts)
        {
            Console.WriteLine($"Номер счёта: {item.Key}, имя владельца: {item.Value.OwnerName}");
        }
    }
    else if (command == "9")
    {
        Console.WriteLine("Введите тип операции:");
        string? operationType = Console.ReadLine();
        if (operationType == null)
        {
            break;
        }
        bool found = false;
        foreach (Transaction transaction in account.Transactions)
        {
            if (transaction.Type == operationType)
            {
                Console.WriteLine(
                $"{transaction.CreatedAt.ToLocalTime():dd.MM.yyyy HH:mm:ss} | " +
                $"{transaction.Type} | {transaction.Amount} сомони");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Операции такого типа не найдены");
        }
    }
    else
    {
        Console.WriteLine("Неизвестный пункт меню");
    }
}

void ShowHistory(BankAccount bankAccount)
{
    Console.WriteLine($"{bankAccount.OwnerName} - {bankAccount.Balance} сомони");
    if (bankAccount.Transactions.Count != 0)
    {
        foreach (Transaction transaction in bankAccount.Transactions)
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

bool TryReadAmount(string prompt, out decimal amount)
{
    Console.Write(prompt);
    if (decimal.TryParse(Console.ReadLine(), out amount))
    {
        return true;
    }
    else
    {
        Console.WriteLine("Введите корректное число");
        return false;
    }
}
