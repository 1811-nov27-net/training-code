using System;
using System.Collections.Generic;
using System.Text;

namespace EFDBFirstDemo.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        public MoviesDBContext Db { get; }

        public MovieRepository(MoviesDBContext db)
        {
            Db = db;
        }

        public void CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        // based on the movie's ID, we're going to update the DB's movie
        // with this one's property values.
        public void EditMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IList<Movie> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public IList<Movie> GetAllMoviesWithGenres()
        {
            throw new NotImplementedException();
        }

        public Movie GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public Movie GetMovieByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
