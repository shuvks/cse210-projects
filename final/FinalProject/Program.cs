using System;
using System.Collections.Generic;

class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Transaction(decimal amount, string description)
    {
        Amount = amount;
        Date = DateTime.Now;
        Description = description;
    }

    public virtual void PrintDetails()
    {
        Console.WriteLine("Transaction Details:");
        Console.WriteLine("Amount: $" + Amount);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Date: " + Date);
    }
}

class Income : Transaction
{
    public string Source { get; set; }

    public Income(decimal amount, string source, string description) : base(amount, description)
    {
        Source = source;
    }

    public override void PrintDetails()
    {
        Console.WriteLine("Income Details:");
        Console.WriteLine("Amount: $" + Amount);
        Console.WriteLine("Source: " + Source);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Date: " + Date);
    }
}

class Expense : Transaction
{
    public string Category { get; set; }

    public Expense(decimal amount, string category, string description) : base(amount, description)
    {
        Category = category;
    }

    public override void PrintDetails()
    {
        Console.WriteLine("Expense Details:");
        Console.WriteLine("Amount: $" + Amount);
        Console.WriteLine("Category: " + Category);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Date: " + Date);
    }
}

abstract class Account
{
    public string Name { get; set; }
    protected decimal Balance { get; set; }
    protected List<Transaction> Transactions { get; set; }

    public Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
        Transactions = new List<Transaction>();
    }

    public void PrintBalance()
    {
        Console.WriteLine("Account Balance for " + Name + ": $" + Balance);
    }

    public void PrintTransactionList()
    {
        Console.WriteLine("Transaction List for " + Name + ":");
        foreach (Transaction transaction in Transactions)
        {
            transaction.PrintDetails();
            Console.WriteLine();
        }
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
        UpdateBalance(transaction.Amount);
    }

    protected abstract void UpdateBalance(decimal amount);
}

class BankAccount : Account
{
    public BankAccount(string name, decimal balance) : base(name, balance)
    {
    }

    protected override void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}

class CreditCardAccount : Account
{
    public CreditCardAccount(string name, decimal balance) : base(name, balance)
    {
    }

    protected override void UpdateBalance(decimal amount)
    {
        Balance -= amount;
    }
}

class FinancialManager
{
    private List<Account> accounts;

    public FinancialManager()
    {
        accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public void AddTransaction(Account account, Transaction transaction)
    {
        if (accounts.Contains(account))
        {
            account.AddTransaction(transaction);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void PrintAccountBalances()
    {
        Console.WriteLine("Account Balances:");
        foreach (Account account in accounts)
        {
            account.PrintBalance();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        FinancialManager financialManager = new FinancialManager();

        Console.WriteLine("Expense Tracker");

        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();

        Console.Write("Enter the initial balance for your bank account: ");
        decimal bankBalance = Convert.ToDecimal(Console.ReadLine());
        BankAccount bankAccount = new BankAccount(userName, bankBalance);
        financialManager.AddAccount(bankAccount);

        Console.Write("Enter the initial balance for your credit card account: ");
        decimal creditCardBalance = Convert.ToDecimal(Console.ReadLine());
        CreditCardAccount creditCardAccount = new CreditCardAccount(userName, creditCardBalance);
        financialManager.AddAccount(creditCardAccount);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. View Account Balances");
            Console.WriteLine("4. View Transaction List");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the amount of income: ");
                    decimal incomeAmount = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter the source of income: ");
                    string incomeSource = Console.ReadLine();

                    Console.Write("Enter a description of the income: ");
                    string incomeDescription = Console.ReadLine();

                    Income income = new Income(incomeAmount, incomeSource, incomeDescription);
                    financialManager.AddTransaction(bankAccount, income);

                    Console.WriteLine("Income added successfully!");
                    Console.WriteLine();
                    break;

                case "2":
                    Console.Write("Enter the amount of expense: ");
                    decimal expenseAmount = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter the category of expense: ");
                    string expenseCategory = Console.ReadLine();

                    Console.Write("Enter a description of the expense: ");
                    string expenseDescription = Console.ReadLine();

                    Expense expense = new Expense(expenseAmount, expenseCategory, expenseDescription);
                    financialManager.AddTransaction(creditCardAccount, expense);

                    Console.WriteLine("Expense added successfully!");
                    Console.WriteLine();
                    break;

                case "3":
                    financialManager.PrintAccountBalances();
                    Console.WriteLine();
                    break;

                case "4":
                    bankAccount.PrintTransactionList();
                    creditCardAccount.PrintTransactionList();
                    Console.WriteLine();
                    break;

                case "5":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine();
                    break;
            }
        }

        Console.WriteLine("Thank you for using the Expense Tracker, " + userName + "!");
    }
}
