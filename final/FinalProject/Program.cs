using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public interface IExercise
{
    int ExerciseId { get; set; }
    string TargetArea { get; set; }
    string ExerciseName { get; set; }

    void DoExercise(int sets, int reps);
}

public abstract class ExerciseBase
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string TargetArea { get; set; }
    public DateTime WorkoutDateTime { get; set; }
    public double CaloriesBurned { get; set; }
}

public class Exercise : ExerciseBase, IExercise
{
    public virtual void DoExercise(int sets, int reps)
    {
    }
}

public class PushUpExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {ExerciseName} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class LegPressExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {ExerciseName} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class BicepCurlExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {ExerciseName} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class CustomExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing Custom Exercise - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class WorkoutData : ExerciseBase
{
    public int Sets { get; set; }
    public int Reps { get; set; }
    public string TimeOfDay { get; set; }
}

public class CustomExerciseData : ExerciseBase
{
    public int Sets { get; set; }
    public int Reps { get; set; }
}

public class WorkoutDataRepository
{
    private const string FilePath = "workout_data.xml";

    public List<ExerciseBase> LoadWorkoutData()
    {
        if (File.Exists(FilePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ExerciseBase>));
            using (TextReader reader = new StreamReader(FilePath))
            {
                return (List<ExerciseBase>)serializer.Deserialize(reader);
            }
        }
        return new List<ExerciseBase>();
    }

    public void SaveWorkoutData(ExerciseBase exerciseData)
    {
        exerciseData.WorkoutDateTime = DateTime.Now;
        exerciseData.CaloriesBurned = CalculateCaloriesBurned(exerciseData);

        List<ExerciseBase> existingData = LoadWorkoutData();
        existingData.Add(exerciseData);

        XmlSerializer serializer = new XmlSerializer(typeof(List<ExerciseBase>));

        using (TextWriter writer = new StreamWriter(FilePath))
        {
            serializer.Serialize(writer, existingData);
        }
    }

    private double CalculateCaloriesBurned(ExerciseBase exerciseData)
    {
        // Implement the actual logic for calorie calculation
        return 0.0;
    }
}

public class UserInterface
{
    public static int GetIntInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Convert.ToInt32(Console.ReadLine());
    }
}

public class WorkoutPlanner
{
    private readonly List<IExercise> exercises;
    private readonly WorkoutDataRepository dataRepository;

    public WorkoutPlanner(List<IExercise> defaultExercises)
    {
        exercises = defaultExercises;
        dataRepository = new WorkoutDataRepository();
    }

    public void DisplayExercises()
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {exercises[i].ExerciseName} - Target Area: {exercises[i].TargetArea}");
        }
    }

    public IExercise SelectExercise(int choice)
    {
        if (choice < 1 || choice > exercises.Count)
        {
            Console.WriteLine("Invalid choice.");
            return null;
        }

        return exercises[choice - 1];
    }

    public int GetIntensityChoice()
    {
        return UserInterface.GetIntInput("Select intensity: 1. Low  2. Medium  3. High  4. Custom");
    }

    public (int, int) GetDefaultSetsAndReps(int intensityChoice)
    {
        switch (intensityChoice)
        {
            case 1: return (2, 10); // Low intensity
            case 2: return (3, 15); // Medium intensity
            case 3: return (4, 20); // High intensity
            default:
                Console.WriteLine("Invalid intensity choice.");
                return (-1, -1);
        }
    }

    public (int, int) GetCustomSetsAndReps()
    {
        return (UserInterface.GetIntInput("Enter custom sets:"), UserInterface.GetIntInput("Enter custom reps:"));
    }

    private double GetUserWeight()
    {
        double weightInLbs = UserInterface.GetIntInput("Enter your body weight in pounds:");
        return ConvertPoundsToKilograms(weightInLbs);
    }

    private double ConvertPoundsToKilograms(double weightInLbs)
    {
        const double PoundsToKilogramsConversionFactor = 0.453592;
        return weightInLbs * PoundsToKilogramsConversionFactor;
    }

    private double CalculateCaloriesBurned(IExercise exercise, int durationInMinutes)
    {
        const double METLowIntensity = 3.5;
        const double METMediumIntensity = 5.0;
        const double METHighIntensity = 7.0;

        double MET;

        switch (exercise.ExerciseId)
        {
            case 1:
                MET = METLowIntensity;
                break;
            case 2:
                MET = METMediumIntensity;
                break;
            case 3:
                MET = METHighIntensity;
                break;
            default:
                Console.WriteLine("Invalid exercise intensity.");
                return 0.0;
        }

        double userWeight = GetUserWeight();

        double totalCaloriesBurned = (durationInMinutes * MET * userWeight) / 200;

        Console.WriteLine($"Calories burned for {exercise.ExerciseName}: {totalCaloriesBurned}");

        return totalCaloriesBurned;
    }

    public void StartWorkout()
    {
        Console.WriteLine("Choose an exercise:");
        DisplayExercises();

        int choice = UserInterface.GetIntInput("");
        IExercise selectedExercise = SelectExercise(choice);

        if (selectedExercise == null)
            return;

        int intensityChoice = GetIntensityChoice();
        if (intensityChoice == -1)
            return;

        int defaultSets, defaultReps;
        if (intensityChoice == 4)
        {
            (int customSets, int customReps) = GetCustomSetsAndReps();

            CustomExercise customExercise = new CustomExercise
            {
                ExerciseId = 0,
                ExerciseName = "Custom Exercise",
                TargetArea = selectedExercise.TargetArea
            };

            customExercise.DoExercise(customSets, customReps);

            dataRepository.SaveWorkoutData(new CustomExerciseData
            {
                ExerciseId = customExercise.ExerciseId,
                ExerciseName = customExercise.ExerciseName,
                TargetArea = customExercise.TargetArea,
                Sets = customSets,
                Reps = customReps,
                WorkoutDateTime = DateTime.Now,
            });
        }
        else
        {
            (defaultSets, defaultReps) = GetDefaultSetsAndReps(intensityChoice);

            int durationInMinutes = UserInterface.GetIntInput("Enter workout duration in minutes:");

            selectedExercise.DoExercise(defaultSets, defaultReps);

            dataRepository.SaveWorkoutData(new WorkoutData
            {
                ExerciseId = selectedExercise.ExerciseId,
                ExerciseName = selectedExercise.ExerciseName,
                TargetArea = selectedExercise.TargetArea,
                Sets = defaultSets,
                Reps = defaultReps,
                WorkoutDateTime = DateTime.Now,
                CaloriesBurned = CalculateCaloriesBurned(selectedExercise, durationInMinutes),
                TimeOfDay = "Morning",
            });
        }

        Console.WriteLine(GetMotivationalMessage());
    }

    private string GetMotivationalMessage()
    {
        return "Congratulations! Keep up the good work!";
    }
}