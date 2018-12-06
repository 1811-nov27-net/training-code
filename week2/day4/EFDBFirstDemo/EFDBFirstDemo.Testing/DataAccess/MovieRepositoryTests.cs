using EFDBFirstDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFDBFirstDemo.Testing.DataAccess
{
    public class MovieRepositoryTests
    {
        // we want to use a new database for each test
        // and a new dbcontext for each of the three steps.
        [Fact]
        public void SaveChangesWithNoChangesDoesntThrowException()
        {
            // arrange
            var options = new DbContextOptionsBuilder<MoviesDBContext>()
                .UseInMemoryDatabase("no_changes_test").Options;
            using (var db = new MoviesDBContext(options))
            {
                // do nothing
                // (just creating the DbContext class with in-memory options (for the first time)
                // automatically creates the in-memory DB too.)
            }

            // act
            using (var db = new MoviesDBContext(options))
            {
                var repo = new MovieRepository(db);
                repo.SaveChanges();
            }

            // assert
            // (no exception should have been thrown)
        }

        [Fact]
        public void CreateMovieWorks()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<MoviesDBContext>()
                .UseInMemoryDatabase("createmovie_test").Options;
            using (var db = new MoviesDBContext(options))
            {
                db.Genre.Add(new Genre { Name = "a" });
                db.SaveChanges();
            }

            // act (for act, only use the repo, to test it)
            using (var db = new MoviesDBContext(options))
            {
                var repo = new MovieRepository(db);
                Movie movie = new Movie { Name = "b" };
                repo.CreateMovie(movie, "a");
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new MoviesDBContext(options))
            {
                Movie movie = db.Movie.Include(m => m.Genre).First(m => m.Name == "b");
                Assert.Equal("b", movie.Name);
                Assert.NotNull(movie.Genre);
                Assert.Equal("a", movie.Genre.Name);
                Assert.NotEqual(0, movie.Id); // should get some generated ID
            }
        }

        [Fact]
        public void CreateMovieWithoutSaveChangesDoesntWork()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<MoviesDBContext>()
                .UseInMemoryDatabase("createmovienosavechanges_test").Options;
            using (var db = new MoviesDBContext(options))
            {
                db.Genre.Add(new Genre { Name = "a" });
                db.SaveChanges();
            }

            // act (for act, only use the repo, to test it)
            using (var db = new MoviesDBContext(options))
            {
                var repo = new MovieRepository(db);
                Movie movie = new Movie { Name = "b" };
                repo.CreateMovie(movie, "a");
                // not calling repo.SaveChanges
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new MoviesDBContext(options))
            {
                // FirstOrDefault returns null if not found.
                Movie movie = db.Movie.Include(m => m.Genre).FirstOrDefault(m => m.Name == "b");
                Assert.Null(movie);
            }
        }
    }
}
