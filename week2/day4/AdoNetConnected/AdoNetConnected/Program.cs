using System;
using System.Data.SqlClient;

namespace AdoNetConnected
{
    class Program
    {
        static void Main(string[] args)
        {
            // ADO.NET is technically MS's umbrella name for all data-related libraries
            // including Entity Framework
            // but when we say "ADO.NET" we typically are talking about the older
            // ways of doing things with DataReader, DataAdapter objects.

            // in various GUIs, you need the server URL, login, password.
            // in code, we have developed a convention to use what we call a
            // "connection string" which will jam all that data into
            // one string to connect to some kind of data source, in our case, SQL Server.

            // never commit your connection strings to source control like git.
            // they're basically passwords

            var connectionString = SecretConfiguration.ConnectionString;

            Console.WriteLine("Enter name of movie: ");
            var input = Console.ReadLine();
            var commandString = $"SELECT * FROM Movies.Movie WHERE Name = '{input}';";
            // allows "SQL injection"
            // un-sanitized user input must not be used to construct SQL queries
            // directly, or else hackers can access or destroy things

            // connected architecture - we're going to receive the whole result
            // have it waiting in the network buffer
            // and use a "cursor"/iterator to read it in row by row.

            using (var connection = new SqlConnection(connectionString))
            {
                // connected architecture
                
                // step 1: open the connection
                connection.Open();
                // step 2: execute the query
                using (var command = new SqlCommand(commandString, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // command.ExecuteReader for SELECT queries that return things
                    //    (returns a DataReader)
                    // command.ExecuteNonQuery for all other commands (that don't return things)
                    //    (returns an int for number of rows affected)

                    // step 3: process results
                    if (reader.HasRows)
                    {
                        // for each row...
                        while (reader.Read())
                        {
                            object id = reader["ID"]; // access values by column name
                            object name = reader["Name"];
                            Console.WriteLine($"ID: {id}, Name: {name}");
                        }
                    }
                }
                // step 4: close the connection
                connection.Close();
            }
        }
    }
}
