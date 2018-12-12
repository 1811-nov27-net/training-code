using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Repositories
{
    public class MovieRepo
    {
        private List<Movie> _movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Star Wars",
                ReleaseDate = new DateTime(1983, 1, 1)
            },
            new Movie
            {
                Id = 2,
                Title = "Lord of the Rings",
                ReleaseDate = new DateTime(2002, 1, 1),
                Cast = new List<string> { "Orlando Bloom", "Elijah Wood" }
            },
        };

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetById(int id)
        {
            return _movies.First(m => m.Id == id);
        }

        public void DeleteMovie(int id)
        {
            var movie = GetById(id);
            _movies.Remove(movie);
        }

        public void CreateMovie(Movie movie)
        {
            if (_movies.Any(m => m.Id == movie.Id))
            {
                throw new ArgumentException($"duplicate ID {movie.Id}");
            }
            _movies.Add(movie);
        }
    }
}
