using System;

class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

class Income : Transaction
{
    public string Source { get; set; }

    public virtual decimal CalculateTax()
    {
        // Perform tax calculation logic
        return 0;
    }
}

class Expense : Transaction
{
    public string Category { get; set; }

    public virtual decimal CalculateTax()
    {
        // Perform tax calculation logic
        return 0;
    }
}

class Saving : Transaction
{
    public string Type { get; set; }

    public virtual void AddInterest()
    {
        // Perform interest calculation logic
    }
}

class Account
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
}

class BankAccount : Account
{
    public string AccountNumber { get; set; }
    public string BankName { get; set; }
}

class CreditCardAccount : Account
{
    public string CardNumber { get; set; }
    public string CardIssuer { get; set; }
}

class FinancialManager
{
    private BankAccount bankAccount;
    private CreditCardAccount creditCardAccount;

    public FinancialManager(BankAccount bankAccount, CreditCardAccount creditCardAccount)
    {
        this.bankAccount = bankAccount;
        this.creditCardAccount = creditCardAccount;
    }

    public void TrackIncome(Income income)
    {
        bankAccount.Balance += income.Amount;
    }

    public void TrackExpense(Expense expense)
    {
        bankAccount.Balance -= expense.Amount;
    }

    public void TrackSaving(Saving saving)
    {
        bankAccount.Balance -= saving.Amount;
    }

    public decimal CalculateTotalIncome()
    {
        // Calculate and return the total income
        return bankAccount.Balance;
    }

    public decimal CalculateTotalExpense()
    {
        // Calculate and return the total expense
        return -bankAccount.Balance;
    }

    public decimal CalculateTotalSaving()
    {
        // Calculate and return the total saving
        return -bankAccount.Balance;
    }

    public void MakeCreditCardPayment(decimal amount)
    {
        creditCardAccount.Balance -= amount;
        bankAccount.Balance -= amount;
    }

    public void DisplayAccountSummary()
    {
        Console.WriteLine("Bank Account: " + bankAccount.Name);
        Console.WriteLine("Account Number: " + bankAccount.AccountNumber);
        Console.WriteLine("Bank Name: " + bankAccount.BankName);
        Console.WriteLine("Balance: $" + bankAccount.Balance);
        Console.WriteLine();

        Console.WriteLine("Credit Card Account: " + creditCardAccount.Name);
        Console.WriteLine("Card Number: " + creditCardAccount.CardNumber);
        Console.WriteLine("Card Issuer: " + creditCardAccount.CardIssuer);
        Console.WriteLine("Balance: $" + creditCardAccount.Balance);
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount bankAccount = new BankAccount
        {
            Name = "John Doe",
            AccountNumber = "123456789",
            BankName = "ABC Bank",
            Balance = 5000
        };

        CreditCardAccount creditCardAccount = new CreditCardAccount
        {
            Name = "John Doe",
            CardNumber = "987654321",
            CardIssuer = "XYZ Bank",
            Balance = 2000
        };

        FinancialManager manager = new FinancialManager(bankAccount, creditCardAccount);

        // Example usage
        Income salary = new Income { Amount = 5000, Source = "Job" };
        Expense rent = new Expense { Amount = 1000, Category = "Housing" };
        Saving emergencyFund = new Saving { Amount = 2000, Type = "Emergency" };

        manager.TrackIncome(salary);
        manager.TrackExpense(rent);
        manager.TrackSaving(emergencyFund);

        decimal totalIncome = manager.CalculateTotalIncome();
        decimal totalExpense = manager.CalculateTotalExpense();
        decimal totalSaving = manager.CalculateTotalSaving();

        Console.WriteLine("Total Income: $" + totalIncome);
        Console.WriteLine("Total Expense: $" + totalExpense);
        Console.WriteLine("Total Saving: $" + totalSaving);

        manager.DisplayAccountSummary();

        Console.ReadLine();
    }
}
