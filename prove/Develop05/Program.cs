using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; }
    public bool Completed { get; protected set; }

    public Goal(string name)
    {
        Name = name;
        Completed = false;
    }

    public abstract int GetPoints();

    public virtual void Complete()
    {
        Completed = true;
    }
}

class SimpleGoal : Goal
{
    public int Points { get; }

    public SimpleGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override int GetPoints()
    {
        return Points;
    }
}

class EternalGoal : Goal
{
    public int Points { get; }

    public EternalGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override int GetPoints()
    {
        return Points;
    }
}

class ChecklistGoal : Goal
{
    public int Points { get; }
    public int TargetCount { get; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name)
    {
        Points = points;
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    public override int GetPoints()
    {
        return Points;
    }

    public override void Complete()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount)
            base.Complete();
    }
}

class EternalQuestProgram
{
    private List<Goal> goals;
    private int score;
    private int level;
    private int experience;

    public EternalQuestProgram()
    {
        goals = new List<Goal>();
        score = 0;
        level = 1;
        experience = 0;
    }

    public void CreateGoal(string goalType)
    {
        Console.WriteLine("Enter goal name:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter points for the goal:");
        int points = int.Parse(Console.ReadLine());

        Goal goal;

        switch (goalType)
        {
            case "simple":
                goal = new SimpleGoal(name, points);
                break;
            case "eternal":
                goal = new EternalGoal(name, points);
                break;
            case "checklist":
                Console.WriteLine("Enter target completion count:");
                int targetCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter bonus points:");
                int bonusPoints = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, points, targetCount, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        goals.Add(goal);
        Console.WriteLine("Goal created successfully.");
    }

    public void RecordEvent()
    {
        Console.WriteLine("Enter goal name to record an event:");
        string goalName = Console.ReadLine();

        Goal goal = goals.Find(g => string.Equals(g.Name, goalName, StringComparison.OrdinalIgnoreCase));

        if (goal != null)
        {
            goal.Complete();
            score += goal.GetPoints();

            if (goal is ChecklistGoal checklistGoal && checklistGoal.Completed)
                score += checklistGoal.BonusPoints;

            Console.WriteLine("Event recorded successfully.");

            // Increase experience points when completing a goal
            experience += goal.GetPoints();

            // Check if player has enough experience to level up
            if (experience >= 100)
            {
                level++;
                Console.WriteLine("Congratulations! You leveled up to Level " + level);
                experience = 0;
            }
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        foreach (Goal goal in goals)
        {
            string goalStatus = goal.Completed ? "[X]" : "[ ]";

            if (goal is ChecklistGoal checklistGoal)
                goalStatus += $" Completed {checklistGoal.CurrentCount}/{checklistGoal.TargetCount} times";

            Console.WriteLine($"{goalStatus} {goal.Name}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine("Score: " + score);
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                string goalData = $"{goal.GetType().Name}|{goal.Name}|{goal.Completed}|{goal.GetPoints()}";

                if (goal is ChecklistGoal checklistGoal)
                    goalData += $"|{checklistGoal.TargetCount}";
                else
                    goalData += "|-";

                writer.WriteLine(goalData);
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals()
    {
        goals.Clear();

        try
        {
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] goalData = line.Split('|');
                    string goalType = goalData[0];
                    string goalName = goalData[1];
                    bool goalCompleted = bool.Parse(goalData[2]);
                    int goalPoints = int.Parse(goalData[3]);

                    Goal goal;

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            goal = new SimpleGoal(goalName, goalPoints);
                            break;
                        case "EternalGoal":
                            goal = new EternalGoal(goalName, goalPoints);
                            break;
                        case "ChecklistGoal":
                            int targetCount = int.Parse(goalData[4]);
                            goal = new ChecklistGoal(goalName, goalPoints, targetCount, 0);
                            break;
                        default:
                            Console.WriteLine("Invalid goal type in file.");
                            return;
                    }

                    if (goalCompleted)
                        goal.Complete();

                    goals.Add(goal);
                }
            }

            Console.WriteLine("Goals loaded successfully.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No goals found.");
        }
    }
}

class Program
{
    static void Main()
    {
        EternalQuestProgram program = new EternalQuestProgram();

        while (true)
        {
            Console.WriteLine("\nEternal Quest Program\n");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.WriteLine("Enter your choice:");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter goal type (simple/eternal/checklist):");
                    string goalType = Console.ReadLine();
                    program.CreateGoal(goalType);
                    break;
                case "2":
                    program.RecordEvent();
                    break;
                case "3":
                    program.DisplayGoals();
                    break;
                case "4":
                    program.DisplayScore();
                    break;
                case "5":
                    program.SaveGoals();
                    break;
                case "6":
                    program.LoadGoals();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
