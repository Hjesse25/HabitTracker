using Microsoft.Data.Sqlite;

namespace HabitTracker;

public class DatabaseManager
{
    readonly private string ConnectionString;

    public DatabaseManager(string fileName)
    {
        ConnectionString = $"Data source={fileName}";
        CreateDatabaseIfNotExist();
    }

    private void CreateDatabaseIfNotExist()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS drinking_water (
                Id INTEGER PRIMARY KEY,
                Date TEXT,
                Quantity INTEGER
            )";

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"Database error: {e.Message}");
            }
        }
    }

    public void ViewRecords()
    {
        // Fetches data from the database and displays it on the console
        using (var connection = new SqliteConnection(ConnectionString))
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM drinking_water";

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\n\tId\tDate\t\tQuantity");
                    while (reader.Read())
                    {
                        Console.WriteLine($"\t{reader[0]}\t{reader[1]}\t{reader[2]}");
                    }
                    reader.Close();

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\nThere are no records.\n");
                }
                connection.Close();
                User.PressEnter();
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"Database error: {e.Message}");
            }
        }
    }

    public void Insert()
    {
        // Gets the date and the quantity (of whatever) and stores in the database
        string date = User.GetDate();
        int quantity = User.GetQuantity();

        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                $"INSERT INTO drinking_water (Date, Quantity) VALUES ('{date}', {quantity})";

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"Database error: {e.Message}");
            }
        }
    }

    public void Delete()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM drinking_water";

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Store valid IDs for validation
                    var validIds = new List<int>();

                    Console.WriteLine("\n\tId\tDate\t\tQuantity");
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[0]);
                        validIds.Add(id);
                        Console.WriteLine($"\t{reader[0]}\t{reader[1]}\t{reader[2]}");
                    }
                    reader.Close();

                    Console.WriteLine("\nSelect the row to delete by Id\n");
                    string input = Console.ReadLine() ?? "";

                    if (int.TryParse(input, out int selectedId))
                    {
                        // Validate that the entered ID exists in the displayed records
                        if (validIds.Contains(selectedId))
                        {
                            // Proceed with deletion
                            var deleteCommand = connection.CreateCommand();
                            deleteCommand.CommandText = "DELETE FROM drinking_water WHERE Id = @id";
                            deleteCommand.Parameters.AddWithValue("@id", selectedId);

                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine($"\nRecord with ID {selectedId} has been deleted successfully.\n");
                            }
                            else
                            {
                                Console.WriteLine($"\nNo record found with ID {selectedId}.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nInvalid ID. Please select an ID from the list above.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Please enter a valid number.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThere are no records.\n");
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"Database error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        User.PressEnter();
    }

    public void Update()
    {
        string input;

        using (var connection = new SqliteConnection(ConnectionString))
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM drinking_water";

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Store valid IDs for validation
                    var validIds = new List<int>();

                    Console.WriteLine("\n\tId\tDate\t\tQuantity");
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[0]);
                        validIds.Add(id);
                        Console.WriteLine($"\t{reader[0]}\t{reader[1]}\t{reader[2]}");
                    }
                    reader.Close();

                    Console.WriteLine("\nSelect the row to update by Id\n");
                    input = Console.ReadLine() ?? "";

                    if (int.TryParse(input, out int selectedId))
                    {
                        if (validIds.Contains(selectedId))
                        {
                            // Gives the user an option on what they would like to update
                            Console.WriteLine("\nWhat would you like to update");
                            Console.WriteLine("Type D for the date.");
                            Console.WriteLine("Type Q for the quantity.");
                            Console.WriteLine("Type B for both date and quantity.\n");

                            input = Console.ReadLine() ?? "";

                            string newDate;
                            int newQuantity;

                            switch (input.ToUpper())
                            {
                                case "D":
                                    // Updates only the date
                                    newDate = User.GetDate();

                                    var newDateCmd = connection.CreateCommand();
                                    newDateCmd.CommandText = $@"UPDATE drinking_water 
                                        SET 'Date' = {newDate}
                                        WHERE Id = @id
                                    ";
                                    newDateCmd.Parameters.AddWithValue("@id", selectedId);

                                    Console.WriteLine($"\nThe date {newDate} with id {selectedId} has been updated successfully.\n");
                                    break;
                                case "Q":
                                    // Updates only the quantity
                                    newQuantity = User.GetQuantity();

                                    var newQuantityCmd = connection.CreateCommand();
                                    newQuantityCmd.CommandText = $@"UPDATE drinking_water
                                        SET Quantity = {newQuantity}
                                        WHERE Id = @id
                                    ";

                                    Console.WriteLine($"\nThe quantity of {newQuantity} with id {selectedId} has been updated sucessfully.\n");
                                    break;
                                case "B":
                                    // Updates both date and quantity
                                    newDate = User.GetDate();
                                    newQuantity = User.GetQuantity();

                                    var newUpdateCmd = connection.CreateCommand();
                                    newUpdateCmd.CommandText = $@"UPDATE drinking_water
                                        SET 'Date' = {newDate},
                                        SET Quantity = {newQuantity}
                                        WHERE Id = @id
                                    ";

                                    Console.WriteLine($"\nThe date {newDate} and quantity of {newQuantity} with id {selectedId} has been updated successfully.\n");
                                    break;
                                default:
                                    Console.WriteLine("Invalid input. Please select the correct option.\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nInvalid ID. Please select an ID from the list above.\n");
                        }

                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Please enter a valid number.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThere are no records\n");
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        User.PressEnter();
    }
}
