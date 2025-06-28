using System;
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
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS drinking_water (
                    Id INTEGER PRIMARY KEY,
                    Date TEXT,
                    Quantity INTEGER
                )";

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
