using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string scriptureText = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life. (John 3:16)";
        Scripture scripture = new Scripture(scriptureText);

        while (!scripture.AreAllWordsHidden())
        {
            Console.Clear();
            Program.DisplayScripture(scripture);
            string userInput = Program.GetUserInput();

            if (userInput == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.Clear();
        Console.WriteLine("All words in the scripture are hidden. Program ended.");
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.WriteLine(scripture.GetScriptureText());
    }

    static string GetUserInput()
    {
        Console.WriteLine("Press Enter to continue or type 'quit' to end the program: ");
        return Console.ReadLine();
    }
}

class Scripture
{
    private string scriptureText;
    private List<Word> words;
    private List<Word> hiddenWords;

    public Scripture(string scriptureText)
    {
        this.scriptureText = scriptureText;
        words = new List<Word>();
        hiddenWords = new List<Word>();
        InitializeWords();
    }

    private void InitializeWords()
    {
        string[] wordArray = scriptureText.Split(" ");
        foreach (string word in wordArray)
        {
            words.Add(new Word(word));
        }
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int numWordsToHide = random.Next(1, 4);

        List<Word> availableWords = GetAvailableWords();

        for (int i = 0; i < numWordsToHide; i++)
        {
            if (availableWords.Count == 0)
                break;

            int randomIndex = random.Next(0, availableWords.Count);
            Word word = availableWords[randomIndex];
            word.Hide();
            hiddenWords.Add(word);
            availableWords.RemoveAt(randomIndex);
        }
    }

    private List<Word> GetAvailableWords()
    {
        List<Word> availableWords = new List<Word>();
        foreach (Word word in words)
        {
            if (!word.IsHidden())
                availableWords.Add(word);
        }
        return availableWords;
    }

    public bool AreAllWordsHidden()
    {
        return hiddenWords.Count == words.Count;
    }

    public string GetScriptureText()
    {
        string scriptureText = "";
        foreach (Word word in words)
        {
            if (word.IsHidden())
                scriptureText += "**** ";
            else
                scriptureText += word.GetText() + " ";
        }
        return scriptureText.Trim();
    }
}

class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        hidden = false;
    }

    public void Hide()
    {
        hidden = true;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public string GetText()
    {
        return text;
    }
}

class Reference
{
    private string referenceText;

    public Reference(string referenceText)
    {
        this.referenceText = referenceText;
    }

    public string GetReferenceText()
    {
        return referenceText;
    }
}
