using System;
using System.Collections.Generic;
using System.Linq;

class FinanceTracker
{
    private List<Expense> expenses;

    public FinanceTracker()
    {
        expenses = new List<Expense>();
    }

    public void AddExpense(string category, double amount, string description)
    {
        expenses.Add(new Expense(category, amount, description));
        Console.WriteLine($"Added expense: {description} - ${amount} in category '{category}'.");
    }

    public void ViewExpenses()
    {
        Console.WriteLine("\n--- Expenses ---");
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
            return;
        }

        foreach (var expense in expenses)
        {
            Console.WriteLine(expense);
        }
    }

    public void ViewTotalExpenses()
    {
        double total = expenses.Sum(e => e.Amount);
        Console.WriteLine($"\nTotal Expenses: ${total}");
    }

    public void ViewCategorySummary()
    {
        var summary = expenses.GroupBy(e => e.Category)
                              .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) });

        Console.WriteLine("\n--- Category Summary ---");
        foreach (var category in summary)
        {
            Console.WriteLine($"{category.Category}: ${category.Total}");
        }
    }

    public static void Main(string[] args)
    {
        FinanceTracker tracker = new FinanceTracker();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. View Total Expenses");
            Console.WriteLine("4. View Category Summary");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter category: ");
                    string category = Console.ReadLine();
                    Console.Write("Enter amount: ");
                    double amount;
                    if (double.TryParse(Console.ReadLine(), out amount))
                    {
                        Console.Write("Enter description: ");
                        string description = Console.ReadLine();
                        tracker.AddExpense(category, amount, description);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a numeric value.");
                    }
                    break;

                case "2":
                    tracker.ViewExpenses();
                    break;

                case "3":
                    tracker.ViewTotalExpenses();
                    break;

                case "4":
                    tracker.ViewCategorySummary();
                    break;

                case "5":
                    exit = true;
                    Console.WriteLine("Thank you for using the Finance Tracker. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}

class Expense
{
    public string Category { get; }
    public double Amount { get; }
    public string Description { get; }

    public Expense(string category, double amount, string description)
    {
        Category = category;
        Amount = amount;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Description}: ${Amount} (Category: {Category})";
    }
}
