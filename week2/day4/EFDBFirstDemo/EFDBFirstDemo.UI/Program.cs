using EFDBFirstDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDBFirstDemo.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<MoviesDBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var options = optionsBuilder.Options;

            var movies = new List<Movie>();
            using (var db = new MoviesDBContext(options))
            {
                movies = db.Movie.ToList();
            }

            foreach (var item in movies)
            {
                Console.WriteLine($"movie ID {item.Id}, name {item.Name}");
            }
        }
    }
}
