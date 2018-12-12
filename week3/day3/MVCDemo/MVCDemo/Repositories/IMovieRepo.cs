using System.Collections.Generic;
using MVCDemo.Models;

namespace MVCDemo.Repositories
{
    public interface IMovieRepo
    {
        void CreateMovie(Movie movie);
        bool DeleteMovie(int id);
        void EditMovie(Movie movie);
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
    }
}