using System;
using System.Threading;

public abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public abstract void Run();
}

public class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Starting {name} activity");
        Console.WriteLine(description);

        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(2000);

        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000); // Delay for 3 seconds for deep breath

            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000); // Delay for 3 seconds for breath out
        }

        Console.WriteLine($"You have completed the {name} activity for {duration} seconds");
        Thread.Sleep(2000);
    }
}

public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Starting {name} activity");
        Console.WriteLine(description);

        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(2000);

        Random random = new Random();
        int promptIndex = random.Next(prompts.Length);

        Console.WriteLine(prompts[promptIndex]);
        Thread.Sleep(2000);

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int questionIndex = 0;

        while (DateTime.Now < endTime)
        {
            string question = questions[questionIndex];

            Console.WriteLine(question);
            Thread.Sleep(2000);

            questionIndex = (questionIndex + 1) % questions.Length;
        }

        Console.WriteLine($"You have completed the {name} activity for {duration} seconds");
        Thread.Sleep(2000);
    }
}

public class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        Console.WriteLine($"Starting {name} activity");
        Console.WriteLine(description);

        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(2000);

        Random random = new Random();
        int promptIndex = random.Next(prompts.Length);

        Console.WriteLine(prompts[promptIndex]);
        Thread.Sleep(2000);

        Console.WriteLine("Start listing items...");

        DateTime endTime = DateTime.Now.AddSeconds(duration);

        int itemCount = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write("Item: ");
            string item = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(item))
            {
                itemCount++;
            }
        }

        Console.WriteLine($"You have listed {itemCount} items");
        Thread.Sleep(2000);
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to the Mindfulness App!");

        while (true)
        {
            Console.WriteLine("\nSelect an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            Activity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
                    break;
                case 2:
                    activity = new ReflectionActivity("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                    break;
                case 3:
                    activity = new ListingActivity("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                    break;
                case 4:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }

            if (activity != null)
            {
                activity.Run();
            }
        }
    }
}
