using System;
using System.Collections.Generic;
using System.Text;

namespace EFDBFirstDemo.DataAccess
{
    public interface IMovieRepository
    {
        // the repository pattern abstracts the details of data access to its consumers
        // it will provide simple "CRUD" type methods
        //   Create, Read, Update, Delete
        // and hide the details of which database, even is it a database, are we using ADO.NET,
        // are we using EntityFramework, etc.

        IList<Movie> GetAllMovies();

        IList<Movie> GetAllMoviesWithGenres();

        Movie GetMovieById(int id);

        Movie GetMovieByName(string name);

        void DeleteMovie(Movie movie);

        void EditMovie(Movie movie);

        void CreateMovie(Movie movie, string withGenre);

        void SaveChanges();
    }
}
