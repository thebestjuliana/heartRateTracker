using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HeartRateTracker
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> exercises = new List<string> { "running", "cycling", "swimming", "resting" };

            string currentExercise;
            //Heart Rate Tracker
            //I’m getting out of shape, and I want to start exercising again.
            //However, I’m also getting old, so I need to watch my heart rate.
            //Please write an application that will track my heart rate
            //every minute and ask me what activity I would like to do every time.
            //Set - Up

            Console.WriteLine("Welcome to the heart rate tracker app!");
            bool running = true;
            while (running)
            {
                bool validAnswer = false;
                int heartRate = 0;
                bool validHeartRate = false;
                while (validAnswer == false || validHeartRate == false)
                {
                    Console.WriteLine("Please enter your starting heart rate:");
                    validAnswer = int.TryParse(Console.ReadLine(), out heartRate);
                    if (validAnswer == false)
                    {
                        Console.WriteLine("I'm sorry, I don't quite understand. Please enter your heart rate in bpm (numbers only)");
                        
                        continue;
                    }
                    else if (heartRate < 120)
                    {
                        Console.WriteLine("I think that's too low, check again? (try something above 120 bpm) ");
                        validHeartRate = false;
                        continue;
                    }
                    else if (heartRate > 175)
                    {
                        Console.WriteLine("Whoa, that's a little high, I don't think you should work out right now");
                        validHeartRate = false;
                        Console.WriteLine("Would you like to double check your heart rate or exit the app");
                        string answer = Console.ReadLine();
                        switch (answer.ToLower().Trim())
                        {
                            case "exit":
                                Console.WriteLine("goodbye!!");
                                running = false;
                                break;

                            default:
                                Console.WriteLine("Ok, let's double check your heart rate");
                                continue;
                        }

                        if (running == false)
                        {
                            break;
                        }
                    }
                    else
                    {
                        validHeartRate = true;
                    }
                }
                if (running == false)
                {
                    break;
                }
                validAnswer = false;
                int exerciseDuration = 0;
                while (validAnswer == false)
                {
                    Console.WriteLine("How long would you like to exercise? (mins)");
                    validAnswer = int.TryParse(Console.ReadLine(), out exerciseDuration);
                    if (validAnswer == true)
                    {
                        Console.WriteLine($"Great! Let's exercise for {exerciseDuration} mins");
                    }
                    else
                    {
                        Console.WriteLine("I don't understand. Please try again");
                    }
                }


                int exerciseNumber = SelectExercise(exercises);
                currentExercise = exercises[exerciseNumber];
                int activityRate = ActivityRate(currentExercise);
                int caloriesBurned = 0;
                int timePassed = 1;
                List<int> heartRates = new List<int>();
                while (timePassed < exerciseDuration)
                {
                    if (heartRate < 120)
                    {
                        Console.WriteLine($"{heartRate} bpm--Your heart rate went too low! Ending exercise session. ");
                        break;
                    }
                    else if (heartRate < 140)
                    {
                        caloriesBurned += 9;
                        Console.WriteLine($"{heartRate} bpm--You're not working hard enough!");
                        Thread.Sleep(1000);
                    }
                    else if (heartRate < 160)
                    {
                        caloriesBurned += 16;
                        Console.WriteLine($"{heartRate} bpm -- You're in the ideal zone!");
                        Thread.Sleep(1000);
                    }
                    else if (heartRate < 175)
                    {
                        caloriesBurned += 20;
                        Console.WriteLine($"{heartRate} bpm --You're working too hard!");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("You died. Ending exercise. ");
                        break;
                    }
                    heartRate += activityRate;
                    if (timePassed % 5 == 0)
                    {
                        Console.WriteLine("Would you like to change activities?");
                        string answer = Console.ReadLine();
                        switch (answer.ToLower())
                        {
                            case "yes":
                            case "y":
                            case "yep":
                                exerciseNumber = SelectExercise(exercises);
                                currentExercise = exercises[exerciseNumber];
                                activityRate = ActivityRate(currentExercise);
                                break;

                            default:
                                Console.WriteLine($"Ok, let's keep {currentExercise}!");
                                break;
                        }
                    }
                    heartRates.Add(heartRate);
                    timePassed++;
                }
                Console.WriteLine("Great Job!");
                Console.WriteLine($"Total Workout: \nYou spent {timePassed} mins working out." +
                    $"\nYou burned {caloriesBurned} calories." +
                    $"\nYour max heart rate was {heartRates.Max()} bpm. " +
                    $"\nYour min heart rate was {heartRates.Min()} bpm.");
                Console.WriteLine("Goodbye!"
                    );
                running = false; 
            }


        }


        public static void ListExercises(List<string> exercises)
        {
            int i = 1;

            foreach (string exercise in exercises)
            {
                Console.WriteLine($"{i}: {exercise}");
                i++;
            }
        }
        public static int SelectExercise(List<string> exercises)
        {
            bool validSelection = false;
            int selection = 0;
            while (validSelection == false)
            {

                Console.WriteLine("Please select an exercise");
                ListExercises(exercises);
                bool numberSelection = int.TryParse(Console.ReadLine(), out selection);
                if (numberSelection == false)
                {
                    Console.WriteLine("That doesn't look like a number, Please try again");
                    continue;
                }
                else if (selection < 0 || selection > exercises.Count)
                {
                    Console.WriteLine("That's not a valid option, Please try again");
                    continue;
                }
                else
                {
                    validSelection = true;
                    return selection - 1;
                }

            }
            return selection;
        }
        public static int ActivityRate(string exercise)
        {
            int activityRate = 0;
            switch (exercise)
            {
                case "running":
                case "cycling":
                    activityRate = 1;
                    break;
                case "swimming":
                    activityRate = 2;
                    break;
                case "resting":
                    activityRate = -2;
                    break;

                default:
                    break;


            }
            return activityRate;
        }
    }
}

