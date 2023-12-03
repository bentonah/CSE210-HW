using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Base class for different types of goals
class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool Completed { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        Completed = false;
    }

    // Marks a goal as complete
    public virtual void MarkComplete()
    {
        Completed = true;
    }

    // Displays the status of a goal
    public virtual string DisplayStatus()
    {
        string status = Completed ? "[X]" : "[ ]";
        return $"{Name} {status}";
    }
}

// Class for simple goals
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points)
    {
    }
}

// Class for eternal goals
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }
}

// Class for checklist goals
class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    // Marks a checklist goal as complete
    public override void MarkComplete()
    {
        CurrentCount++;
        if (CurrentCount == TargetCount)
        {
            Points += BonusPoints;
            base.MarkComplete();
        }
    }

    // Displays the status of a checklist goal
    public override string DisplayStatus()
    {
        return $"{base.DisplayStatus()} Completed {CurrentCount}/{TargetCount} times";
    }
}

// EXCEEDS REQUIREMENTS
// Class for negative goals
class NegativeGoal : Goal
{
    public NegativeGoal(string name, int points) : base(name, points)
    {
    }

    // Marks a negative goal as complete (with the deduction of points)
    public override void MarkComplete()
    {
        Points = Math.Max(0, Points - 50);
        base.MarkComplete();
    }
}

// Main class for the program
class EternalQuestProgram
{
    private List<Goal> goals;
    private int score;
    private string jsonFilePath = "goals.json";

    public EternalQuestProgram()
    {
        goals = new List<Goal>();
        score = 0;
    }

    // Creates a new goal based on the goal type
    public void CreateGoal(string goalType, string name, int points, params int[] args)
    {
        Goal goal;
        switch (goalType)
        {
            case "Simple":
                goal = new SimpleGoal(name, points);
                break;
            case "Eternal":
                goal = new EternalGoal(name, points);
                break;
            case "Checklist":
                goal = new ChecklistGoal(name, points, args[0], args[1]);
                break;
            case "Negative":
                goal = new NegativeGoal(name, points);
                break;
            default:
                throw new ArgumentException("Invalid goal type");
        }
        goals.Add(goal);
    }

    // Records an event for a specific goal
    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count && !goals[goalIndex].Completed)
        {
            score += goals[goalIndex].Points;
            goals[goalIndex].MarkComplete();
            Console.WriteLine("Event recorded successfully.");
        }
        else
        {
            Console.WriteLine("Invalid goal index or goal already completed.");
        }
    }

    // Displays all goals
    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].DisplayStatus()}");
        }
    }

    // Displays the current score
    public void DisplayScore()
    {
        Console.WriteLine($"Current Score: {score}");
    }

    // Saves goals to a file using JSON serialization
    public void SaveGoals()
    {
        try
        {
            string json = JsonSerializer.Serialize(goals, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, json);
            Console.WriteLine("Goals saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    // Loads goals from a file using JSON serialization
    public void LoadGoals()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                goals = JsonSerializer.Deserialize<List<Goal>>(json);
                Console.WriteLine("Goals loaded successfully.");
            }
            else
            {
                Console.WriteLine("File not found. No goals loaded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }
}

// Main program
class Program
{
    static void Main()
    {
        EternalQuestProgram eternalQuest = new EternalQuestProgram();

        // Creates various types of goals
        eternalQuest.CreateGoal("Simple", "Run a Marathon", 1000);
        eternalQuest.CreateGoal("Eternal", "Read Scriptures", 100);
        eternalQuest.CreateGoal("Checklist", "Attend Temple", 50, 10, 500);
        eternalQuest.CreateGoal("Negative", "Quit Smoking", 200);

        // Displays initial goals and score
        eternalQuest.DisplayGoals();
        eternalQuest.DisplayScore();

        // Records events for some goals
        eternalQuest.RecordEvent(0);
        eternalQuest.RecordEvent(1);
        eternalQuest.RecordEvent(2);

        // Displays updated goals and score
        eternalQuest.DisplayGoals();
        eternalQuest.DisplayScore();

        // Saves goals to a file
        eternalQuest.SaveGoals();

        // Loads goals from a file
        eternalQuest.LoadGoals();
    }
}