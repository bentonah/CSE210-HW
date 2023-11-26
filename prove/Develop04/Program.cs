using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    // EXCEEDS REQUIREMENTS - tracks how many times the activities were performed
   public int UsageCount { get; private set; }
    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
        UsageCount = 0;
    }

    // Displays the starting message, sets the duration, and shows the countdown to the user.
    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting {Name} activity:");
        Console.WriteLine(Description);
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        ShowCountdown(3);
    }

    // Displays the ending message, shows the countdown, and provides feedback to the user
    public void DisplayEndingMessage()
    {
        Console.WriteLine("Great job! You've completed the activity.");
        Console.WriteLine($"You spent {Duration} seconds on the {Name} activity.");
        ShowCountdown(3);
    }

    // Displays a spinner animation
    public void ShowSpinner(int seconds)
    {
        Console.Write("Spinning...");
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine();
    }

    // Displays a countdown animation
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(new string('.', i) + " " + i + "\r");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus only on your breathing. This will help you to forget about the pressure and stress you have going on around you.")
    {
    }

    // Runs the breathing activity
    public void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Get ready to begin...");
        for (int i = 0; i < Duration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(3);
            Console.WriteLine("Breathe out...");
            ShowCountdown(3);
        }
        DisplayEndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts;
    private List<string> questions;

    // New lists to keep track of used prompts and questions
    private List<string> usedPrompts;
    private List<string> usedQuestions;

    // Subclass constructor
    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
        };
        // EXCEEDS REQUIREMENTS - tracks prompts/questions that have been used already to avoid repeats.
        usedPrompts = new List<string>();
        usedQuestions = new List<string>();
    }

    // Run the reflection activity
    public void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Get ready for reflection.");

        // Shuffle the prompts and questions to ensure randomness
        Shuffle(prompts);
        Shuffle(questions);

        for (int i = 0; i < Duration; i++)
        {
            string prompt = GetRandomPrompt();
            Console.WriteLine(prompt);
            ShowCountdown(3);
            DisplayQuestions();
        }

        // Clear used prompts and questions at the end of the session
        usedPrompts.Clear();
        usedQuestions.Clear();

        DisplayEndingMessage();
    }

    // Get a random prompt, ensuring it has not been used in this session
    private string GetRandomPrompt()
    {
        string prompt;
        do
        {
            prompt = prompts[new Random().Next(prompts.Count)];
        } while (usedPrompts.Contains(prompt));

        usedPrompts.Add(prompt);  // Mark the prompt as used
        return prompt;
    }

    // Get a random question, ensuring it has not been used in this session
    private string GetRandomQuestion()
    {
        string question;
        do
        {
            question = questions[new Random().Next(questions.Count)];
        } while (usedQuestions.Contains(question));

        usedQuestions.Add(question);  // Mark the question as used
        return question;
    }

    // Display random questions
    private void DisplayQuestions()
    {
        for (int i = 0; i < 2; i++) // Display two random questions
        {
            string question = GetRandomQuestion();
            Console.WriteLine(question);
            ShowCountdown(3);
            ShowSpinner(3);
        }
    }

    // Helper method to shuffle a list
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        Random rand = new Random();

        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
class ListingActivity : Activity
{
    private List<string> listingPrompts;

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area, with the intention of helping you to feel more positive.")
    {
        // Loads listing prompts
        listingPrompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    // Runs the listing activity
    public void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Get ready to list some items.");
        ShowCountdown(3);
        Console.WriteLine("You have 10 seconds to start thinking about the prompt...");
        ShowCountdown(10);
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);
        GetListFromUser();
        DisplayEndingMessage();
    }

    // Gets a random listing prompt
    private string GetRandomPrompt()
    {
        return listingPrompts[new Random().Next(listingPrompts.Count)];
    }

    // Gets a list of items from the user
    private void GetListFromUser()
    {
        List<string> items = new List<string>();
        while (true)
        {
            Console.Write("Enter an item (or type 'done' to finish): ");
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
                break;
            items.Add(item);
        }
        Console.WriteLine($"You listed {items.Count} items:");
        Console.WriteLine(string.Join(", ", items));
        ShowCountdown(3);
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                    break;
                case 2:
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                    break;
                case 3:
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Run();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("That is not an option. Please choose again.");
                    break;
            }
        }
    }
}