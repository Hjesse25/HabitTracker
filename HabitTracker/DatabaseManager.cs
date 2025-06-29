using System.Data;
using Microsoft.Data.Sqlite;

namespace HabitTracker;

public class DatabaseManager
{
    readonly private string FileName;
    readonly private string ConnectionString;

    public DatabaseManager(string fileName)
    {
        FileName = fileName;
        ConnectionString = $"Data source={FileName}";
        CreateDatabaseIfNotExist();
    }

    public void CreateDatabaseIfNotExist()
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
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public void Insert()
    {
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
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
