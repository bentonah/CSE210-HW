using System;

class Program
{
    static void Main(string[] args)
    {

        // Using default constructor, initializes to 1/1
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        // Using constructor for whole numbers, initializes to 6/1
        Fraction f2 = new Fraction(6);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        // Using constructor for arbitrary fractions, initializes to 6/7
        Fraction f3 = new Fraction(6, 7);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        // Creating an instance of the fraction
        Fraction f = new Fraction();
        Console.WriteLine($"Original Fraction: {f.GetFractionString()}");

        // Using setters to modify the fraction
        f.SetTop(3);
        f.SetBottom(5);

        // Using getters to retrieve and display the modified values
        Console.WriteLine($"Modified Fraction: {f.GetFractionString()}");
        Console.WriteLine($"Decimal Value: {f.GetDecimalValue()}");
    }
}