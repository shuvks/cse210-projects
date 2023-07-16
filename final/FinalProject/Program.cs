using System;
using System.Collections.Generic;

class ExpenseTracker
{
    static void Main(string[] args)
    {
        Console.WriteLine("Expense Tracker");

        // Get user information
        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();

        Console.Write("Enter the last 4 digits of your card number: ");
        string cardNumber = Console.ReadLine();

        decimal remainingBalance = 0;
        List<string> expenseList = new List<string>();

        // Track expenses
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. Add Money to Bank Account");
            Console.WriteLine("3. View Remaining Balance");
            Console.WriteLine("4. View Expense List");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the amount spent: ");
                    decimal expenseAmount = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter a description of the expense: ");
                    string expenseDescription = Console.ReadLine();

                    // Subtract the expense amount from the remaining balance
                    remainingBalance -= expenseAmount;

                    // Add the expense and its description to the expense list
                    string expenseEntry = expenseAmount.ToString("C") + " - " + expenseDescription;
                    expenseList.Add(expenseEntry);

                    Console.WriteLine("Expense added successfully!");
                    Console.WriteLine();
                    break;

                case "2":
                    Console.Write("Enter the amount to add to your bank account: ");
                    decimal depositAmount = Convert.ToDecimal(Console.ReadLine());

                    // Add the deposit amount to the remaining balance
                    remainingBalance += depositAmount;

                    Console.WriteLine("Money added to your bank account successfully!");
                    Console.WriteLine();
                    break;

                case "3":
                    Console.WriteLine("Remaining Balance: $" + remainingBalance);
                    Console.WriteLine();
                    break;

                case "4":
                    Console.WriteLine("Expense List:");
                    foreach (string expense in expenseList)
                    {
                        Console.WriteLine(expense);
                    }
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
