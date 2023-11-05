using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage? ");
        string response = Console.ReadLine();
        int percent = int.Parse(response);

        string grade = "";

        if (percent >= 90)
        {
            grade = "A";
        }
        else if (percent >= 80)
        {
            grade = "B";
        }
        else if (percent >= 70)
        {
            grade = "C";
        }
        else if (percent >= 60)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }

        Console.WriteLine($"Your grade is: {grade}");
        
        if (percent >= 70)
        {
            Console.WriteLine("Congratualations, you passed!");
        }
        else
        {
            Console.WriteLine("You didn't pass this time, but that's okay. You've got this next time!");
        }
    }
}