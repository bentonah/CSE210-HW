-------- WORKOUT PLANNER --------

----- Overview -----
This console application is designed for planning and tracking workouts,
and allows users to select exercises, set workout intensity, and record their workout data.
The application supports predefined exercises like push-ups, leg presses, and bicep curls, as well as custom exercises.

----- How to Use -----
1. Run the application.
2. Choose an exercise from the displayed list.
3. Select the workout intensity:
    - Low
    - Medium
    - High
| Custom
| Enter workout details:
| For custom intensity, input the number of sets and reps.
| For predefined intensities, enter the workout duration in minutes.
4. Enter your body weight in pounds when prompted.
5. View the workout summary, including calories burned.
6. Receive a motivational message.

----- Classes and Interfaces -----
- IExercise: Interface defining basic exercise properties and a method for performing exercises.
- ExerciseBase: Abstract class containing common properties for exercises.
- Exercise: Concrete class implementing the IExercise interface.
- Various exercise classes (PushUpExercise, LegPressExercise, BicepCurlExercise, CustomExercise) extending the Exercise class with specific implementations.
- WorkoutData and CustomExerciseData: Classes representing workout data and custom exercise data, respectively.
- WorkoutDataRepository: Manages loading and saving workout data to an XML file.
- UserInterface: Provides a simple method for getting integer input from the user.
- WorkoutPlanner: Main class orchestrating the workout planning and tracking process for the entire program.

----- Data Storage -----
Workout data is stored in an XML file (workout_data.xml). The WorkoutDataRepository class handles loading and saving data using XML serialization.
This can be adjusted to save to a database or different file if desired.

----- Calorie Calculation -----
Calories burned are calculated using the formula:
{ Calories=(Time x MET x Body Weightin kilos) / 200 }
where MET (Metabolic Equivalent of Task) values differ based on exercise intensity.