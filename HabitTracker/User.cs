using System;

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
                // case "2":
                //     break;
                // case "3":
                //     break;
                // case "4":
                //     break;
                default:
                    Console.WriteLine("\nInvalid input.");
                    break;
            }
        }
    }
}
