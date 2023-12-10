using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public interface IExercise
{
    void DoExercise(int sets, int reps);
}

public class ExerciseType
{
    private string typeName;

    public string TypeName
    {
        get { return typeName; }
        set { typeName = value; }
    }
}

public class ExerciseIntensity
{
    private string intensityName;
    private int intensityLevel;

    public string IntensityName
    {
        get { return intensityName; }
        set { intensityName = value; }
    }

    public int IntensityLevel
    {
        get { return intensityLevel; }
        set { intensityLevel = value; }
    }
}

public abstract class Exercise : IExercise
{
    private string name;
    private string targetArea;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string TargetArea
    {
        get { return targetArea; }
        set { targetArea = value; }
    }

    public abstract void DoExercise(int sets, int reps);
}

public class ExerciseIntensity
{
    private string intensityName;
    private int intensityLevel;

    public string IntensityName
    {
        get { return intensityName; }
        set { intensityName = value; }
    }

    public int IntensityLevel
    {
        get { return intensityLevel; }
        set { intensityLevel = value; }
    }
}

public class ExerciseType
{
    private string typeName;

    public string TypeName
    {
        get { return typeName; }
        set { typeName = value; }
    }
}

public class PushUpExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {Name} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class LegPressExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {Name} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public class BicepCurlExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing {Name} - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}


public class CustomExercise : Exercise
{
    public override void DoExercise(int sets, int reps)
    {
        Console.WriteLine($"Performing Custom Exercise - Target Area: {TargetArea} - Sets: {sets}, Reps: {reps}");
    }
}

public abstract class ExerciseBase
{
    public string ExerciseName { get; set; }
    public string TargetArea { get; set; }
}

public class WorkoutData : ExerciseBase
{
    public int Sets { get; set; }
    public int Reps { get; set; }
    public DateTime WorkoutDateTime { get; set; }
    public double CaloriesBurned { get; set; }
    public string TimeOfDay { get; set; }
}

public class CustomExerciseData : ExerciseBase
{
    public int Sets { get; set; }
    public int Reps { get; set; }
    public DateTime WorkoutDateTime { get; set; }
}

public class WorkoutDataRepository
{
    private const string FilePath = "workout_data.xml";

    public List<ExerciseBase> LoadWorkoutData()
    {
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
            Console.WriteLine($"{i + 1}. {exercises[i].Name} - Target Area: {exercises[i].TargetArea}");
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
                Name = "Custom Exercise",
                TargetArea = selectedExercise.TargetArea
            };

            customExercise.DoExercise(customSets, customReps);

            dataRepository.SaveWorkoutData(new CustomExerciseData
            {
                ExerciseName = customExercise.Name,
                TargetArea = customExercise.TargetArea,
                Sets = customSets,
                Reps = customReps,
                WorkoutDateTime = DateTime.Now,
            });
        }
        else
        {
            (defaultSets, defaultReps) = GetDefaultSetsAndReps(intensityChoice);

            selectedExercise.DoExercise(defaultSets, defaultReps);

            dataRepository.SaveWorkoutData(new WorkoutData
            {
                ExerciseName = selectedExercise.Name,
                TargetArea = selectedExercise.TargetArea,
                Sets = defaultSets,
                Reps = defaultReps,
                WorkoutDateTime = DateTime.Now,
                CaloriesBurned = CalculateCaloriesBurned(selectedExercise, defaultSets, defaultReps),
                TimeOfDay = "Morning",
            });
        }

        Console.WriteLine(GetMotivationalMessage());
    }

    private double CalculateCaloriesBurned(IExercise exercise, int sets, int reps)
    {
        return 0.0;
    }

    private string GetMotivationalMessage()
    {
        return "Congratulations! Keep up the good work!";
    }
}