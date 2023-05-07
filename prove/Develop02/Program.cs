using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry {
    public string Prompt;
    public string Response;
    public string Date;

    public JournalEntry(string prompt, string response, string date) {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString() {
        return $"{Date}\n{Prompt}: {Response}";
    }

    public string ToCSVString() {
        return $"{Date},{Prompt},{Response}";
    }
}

class Journal {
    private List<JournalEntry> entries;

    public Journal() {
        entries = new List<JournalEntry>();
    }

    public void AddEntry(JournalEntry entry) {
        entries.Add(entry);
    }

    public void Display() {
        foreach (JournalEntry entry in entries) {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName) {
        using (StreamWriter writer = new StreamWriter(fileName)) {
            foreach (JournalEntry entry in entries) {
                writer.WriteLine(entry.ToCSVString());
            }
        }
    }

    public void LoadFromFile(string fileName) {
        entries.Clear();

        using (StreamReader reader = new StreamReader(fileName)) {
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                string[] parts = line.Split(',');

                JournalEntry entry = new JournalEntry(parts[1], parts[2], parts[0]);
                entries.Add(entry);
            }
        }
    }
}

class Program {
    private static Journal journal = new Journal();
    private static Random random = new Random();

    private static void WriteNewEntry() {
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString();

        JournalEntry entry = new JournalEntry(prompt, response, date);
        journal.AddEntry(entry);
    }

    private static void DisplayJournal() {
        journal.Display();
    }

    private static void SaveJournalToFile() {
        Console.Write("Enter filename: ");
        string fileName = Console.ReadLine();
        journal.SaveToFile(fileName);
        Console.WriteLine($"Journal saved to {fileName}");
    }

    private static void LoadJournalFromFile() {
        Console.Write("Enter filename: ");
        string fileName = Console.ReadLine();
        journal.LoadFromFile(fileName);
        Console.WriteLine($"Journal loaded from {fileName}");
    }

    private static string GetRandomPrompt() {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something I learned today?",
            "What is something that made me smile today?",
            "What is something that challenged me today?",
            "What is something I am grateful for today?"
        };

        int index = random.Next(prompts.Length);
        return prompts[index];
    }

    static void Main(string[] args) {
        bool done = false;

        while (!done) {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                   break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    done = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    break;
            }
        }
    }
}