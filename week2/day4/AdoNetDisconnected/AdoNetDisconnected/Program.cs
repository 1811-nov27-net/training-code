using System;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetDisconnected
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;

            Console.WriteLine("Enter name of movie: ");
            var input = Console.ReadLine();
            var commandString = $"SELECT * FROM Movies.Movie WHERE Name = '{input}';";
            // allows "SQL injection"

            // disconnected architecture - we're going to wait to get the whole result,
            // load it into a "DataSet" (in-memory collection), close our connection
            // and THEN process the results.

            // this has more overhead on the C# side, but it keeps the connection open
            // for less time (which is really good because the DB is usually the bottleneck)

            // dataset will hold our results
            var dataSet = new DataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                // disconnected architecture

                // step 1: open the connection
                connection.Open();
                using (var command = new SqlCommand(commandString, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    // step 2: execute the query, filling dataset
                    adapter.Fill(dataSet);
                    // (still uses DataReader object internally)
                }
                // step 3: close the connection
                connection.Close();

                // step 4: process results
                DataTable firstTable = dataSet.Tables[0];
                // watch out - foreach without generics does a cast when you assign the
                // type right here (DataRow)
                foreach (DataRow row in firstTable.Rows)
                {
                    // DataSet contains DataTable, DataColumn, DataRow, etc.
                    object id = row["ID"]; // access values by column name
                    object name = row["Name"];
                    Console.WriteLine($"ID: {id}, Name: {name}");
                }
            }
        }
    }
}
