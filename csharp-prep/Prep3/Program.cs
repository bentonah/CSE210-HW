using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string goAgain;

        do
        {
            int number = random.Next(1, 101);
            int guesses = 0;
            int guess;

            Console.WriteLine("Welcome to the Magic Number Guessing Game!");
            Console.WriteLine("Try and guess the number between 1 and 100! ");

            do
            {
                Console.Write("What is your guess?  ");
                guess = int.Parse(Console.ReadLine());
                guesses++;

                if (guess < number)
                {
                    Console.WriteLine("Higher than that.");
                }
                else if (guess > number)
                {
                    Console.WriteLine("Lower than that.");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed the number in {guesses} guesses.");
                }

            } while (guess != number);

            Console.Write("Do you want to play again? (yes/no): ");
            goAgain = Console.ReadLine();

        } while (goAgain.ToLower() == "yes");
    }
}
