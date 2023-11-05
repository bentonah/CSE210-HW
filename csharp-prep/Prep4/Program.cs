using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        int userNumber = -1;
        while (userNumber != 0)
        {
            Console.Write("Enter a number, or enter 0 to quit: ");
            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);

            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }

        int sum = CalculateSum(numbers);
        float average = CalculateAverage(numbers);
        int max = FindMax(numbers);
        int smallestPositive = FindSmallestPositive(numbers);
        List<int> sortedNumbers = SortNumbers(numbers);

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The max is: {max}");
        Console.WriteLine($"The smallest positive number closest to zero is: {smallestPositive}");

        Console.WriteLine("Sorted Numbers:");
        foreach (int number in sortedNumbers)
        {
            Console.WriteLine(number);
        }
    }

    static int CalculateSum(List<int> numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    static float CalculateAverage(List<int> numbers)
    {
        int sum = CalculateSum(numbers);
        float average = ((float)sum) / numbers.Count;
        return average;
    }

    static int FindMax(List<int> numbers)
    {
        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        return max;
    }

    static int FindSmallestPositive(List<int> numbers)
    {
        int smallestPositive = int.MaxValue;
        foreach (int number in numbers)
        {
            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number;
            }
        }
        return smallestPositive;
    }
    
    static List<int> SortNumbers(List<int> numbers)
    {
        List<int> sortedNumbers = new List<int>(numbers);
        sortedNumbers.Sort();
        return sortedNumbers;
    }
}