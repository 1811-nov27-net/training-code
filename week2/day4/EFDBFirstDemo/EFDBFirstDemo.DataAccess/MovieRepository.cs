using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFDBFirstDemo.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        public MoviesDBContext Db { get; }

        public MovieRepository(MoviesDBContext db)
        {
            Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // the ID should be left at default zero
        // so the context knows that's new, and so the DB can auto-generated that IDENTITY value
        public void CreateMovie(Movie movie, string withGenre)
        {
            Genre trackedGenre = Db.Genre.First(g => g.Name == withGenre);
            movie.Genre = trackedGenre;
            Db.Add(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            // Find is the fastest way to get an entity by primary key
            // especially if it's already in memory.
            Movie trackedMovie = Db.Movie.Find(movie.Id);
            if (trackedMovie == null)
            {
                throw new ArgumentException("no such movie id", nameof(movie.Id));
            }
            Db.Remove(trackedMovie);
        }

        // based on the movie's ID, we're going to update the DB's movie
        // with this one's property values.
        public void EditMovie(Movie movie)
        {
            // would add it if it didn't exist
            Db.Update(movie);

            // (or this way... there can be issues with having multiple tracked entities of the same ID
            // so we use Find to get the currently tracked one if there is one
            Movie trackedMovie = Db.Movie.Find(movie.Id);
            Db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public IList<Movie> GetAllMovies()
        {
            // we don't want to use the EF tracking behavior outside this class,
            // so AsNoTracking will allow that and also remove the performance overhead of it
            return Db.Movie.AsNoTracking().ToList();
        }

        public IList<Movie> GetAllMoviesWithGenres()
        {
            return Db.Movie.Include(m => m.Genre).AsNoTracking().ToList();
        }

        public Movie GetMovieById(int id)
        {
            return Db.Movie.Find(id);
        }

        public Movie GetMovieByName(string name)
        {
            return Db.Movie.First(m => m.Name == name);
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }
    }
}
