using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    public void AddEntry(string prompt, string response, string date)
    {
        JournalEntry entry = new JournalEntry
        {
            Prompt = prompt,
            Response = response,
            Date = date
        };
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

// This exceeds requirements by saving the journal entries to a separate JSON file
    public void SaveToJsonFile(string fileName)
    {
        string json = JsonConvert.SerializeObject(entries);
        File.WriteAllText(fileName, json);
        Console.WriteLine("Journal entries saved to JSON file successfully.");
    }

    public void LoadFromJsonFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            entries = JsonConvert.DeserializeObject<List<JournalEntry>>(json);
            Console.WriteLine("Journal entries loaded from JSON file successfully.");
        }
        else
        {
            Console.WriteLine("JSON file not found. Creating a new journal.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Random random = new Random();
        string[] prompts = {
            "Who was the most exciting thing that happened to me today?",
            "How many people did I see smile today?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "Was today what I would consider a good day?",
            "When and how did I feel love today?",
            "If I had one thing I could do over today, what would it be?"
        };

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string prompt = prompts[random.Next(prompts.Length)];
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Response: ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToString("MM-dd--yyyy HH:mm:ss");
                    journal.AddEntry(prompt, response, date);
                    break;
                case 2:
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.Write("Enter file name to save (entries.json): ");
                    string saveFileName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(saveFileName))
                    {
                        saveFileName = "entries.json";
                    }
                    journal.SaveToJsonFile(saveFileName);
                    break;
                case 4:
                    Console.Write("Enter file name to load (entries.json): ");
                    string loadFileName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(loadFileName))
                    {
                        loadFileName = "entries.json";
                    }
                    journal.LoadFromJsonFile(loadFileName);
                    break;
                case 5:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("That was an invalid choice. Please select one of the listed options.");
                    break;
            }
        }
    }
}