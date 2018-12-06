using EFDBFirstDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDBFirstDemo.UI
{
    class Program
    {
        static DbContextOptions<MoviesDBContext> options = null;

        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<MoviesDBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            options = optionsBuilder.Options;

            PrintMovies();
            AddAMovie();
            Console.WriteLine();

            PrintMovies();
            EditSomething();
            Console.WriteLine();

            PrintMovies();
        }

        static void PrintMovies()
        {
            var movies = new List<Movie>();
            using (var db = new MoviesDBContext(options))
            {
                // at this point, we haven't connected to the db yet

                // this winds up as a "SELECT * FROM Movies.Movie" somewhere
                movies = db.Movie.ToList();
                // this does not fetch the full "entity graph" of the movie -
                // "navigation properties" like Genre are null.

                // this winds up as a SELECT with a join.
                movies = db.Movie.Include(m => m.Genre).ToList();
                // with .Include, we fetch the related properties so they are not null.

                // EF stores the result sets in the DbSets of the DbContext, so
                // this second access will use cached values. (faster)
                movies = db.Movie.Include(m => m.Genre).ToList();
            }

            foreach (var item in movies)
            {
                Console.WriteLine($"movie ID {item.Id}, name {item.Name}, genre {item.Genre.Name}");
            }
        }

        static void AddAMovie()
        {
            using (var db = new MoviesDBContext(options))
            {
                // get first alphabetical name
                // (no network access yet!)
                var firstMovieQuery = db.Movie.Include(m => m.Genre).OrderBy(m => m.Name);

                Movie movie = firstMovieQuery.First();
                // that fetched only the one movie, not all of them

                var newMovie = new Movie { Name = movie.Name + " 2", Genre = movie.Genre };

                // few different ways to add this. (aka track the entity)
                db.Add(newMovie); // this one guesses which DbSet based on the type.
                //db.Movie.Add(newMovie);

                // we can add the movie to the genre's navigation property "Movie" collection.
                //movie.Genre.Movie.Add(newMovie);

                // after doing any of those, the change to the database is recorded in the DbContext
                // but not yet applied to the DB.

                // SaveChanges applies all the tracked changes in one transaction.
                db.SaveChanges();

                // now, those extra properties on the newMovie that we didn't set (like Id)
                // have been filled in by the dbcontext.

                // (you can continue owkring and savechanges multiple times on one dbcontext)
            }

            // objects you get out of the DbSets are "tracked" by the context.
            // any changes you make to them will be noticed and  applied to the DB
            // on the next SaveChanges.
            
            // objects you create yourself are NOT tracked - track them with db.Add,
            // db.Update, or adding them to a tracked entity's navigation property.
        }

        static void EditSomething()
        {
            using (var db = new MoviesDBContext(options))
            {
                // this is going to be executed right away
                Genre actionGenre = db.Genre.FirstOrDefault(g => g.Name == "Action");
                // (First throws exceptions, FirstOrDefault just returns null when there's none)

                if (actionGenre != null)
                {
                    actionGenre.Name = "Action/Adventure";

                    // because actionGenre object is tracked, the context sees any changes and applies them.
                    db.SaveChanges();
                }
            }
        }
    }
}
