using System;
using System.Globalization;
using System.IO.Pipelines;

namespace HabitTracker;

public class User
{
    readonly DatabaseManager DB;
    public User(string fileName)
    {
        DB = new DatabaseManager(fileName);
    }

    public void UserMenu()
    {
        bool exitApp = false;

        while (!exitApp)
        {
            Console.Clear();

            Console.WriteLine("\n\nMain Menu");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application");
            Console.WriteLine("Type 1 to View All Records");
            Console.WriteLine("Type 2 to Insert Record");
            Console.WriteLine("Type 3 to Delete Record");
            Console.WriteLine("Type 4 to Update Record");
            Console.WriteLine("----------------------------------------\n");

            string optionInput = Console.ReadLine() ?? "";

            switch (optionInput)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    exitApp = true;
                    Environment.Exit(0);
                    break;
                // case "1":
                //     break;
                case "2":
                    DB.Insert();
                    break;
                // case "3":
                //     break;
                // case "4":
                //     break;
                default:
                    Console.WriteLine("\nInvalid input.\n");
                    PressEnter();
                    break;
            }
        }
    }

    public static string GetDate()
    {
        string? formatDate;
        var culture = new CultureInfo("en-US");
        bool validDate = false;

        while (!validDate)
        {
            Console.WriteLine("\nPlease enter the date: (Format: MM-dd-yy)\n");
            string input = Console.ReadLine() ?? "";

            if (DateOnly.TryParse(input, culture, out DateOnly date))
            {
                formatDate = date.ToString();
                validDate = true;
                return formatDate;
            }
            else
            {
                Console.WriteLine("\nInvalid date type.\n");
                PressEnter();
                continue;
            }
        }

        return string.Empty;
    }

    public static int GetQuantity()
    {
        bool success = false;

        while (!success)
        {
            Console.WriteLine("\nPlease enter the number of glasses or whatever you want to track (No decimals allowed)\n");
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int number))
            {
                success = true;
            }
            else
            {
                Console.WriteLine("\nInvalid input type.\n");
                PressEnter();
                continue;
            }

            return number;
        }

        return 0;
    }

    public static void PressEnter()
    {
        Console.WriteLine("Press enter to continue..");
        Console.ReadLine();
    }
}
