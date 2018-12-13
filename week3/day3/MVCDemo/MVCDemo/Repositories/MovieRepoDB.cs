using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;                //  and just Movie for this one
using System;
using System.Collections.Generic;
using System.Linq;
// namespace alias to get around same-name classes
using Data = MVCDemo.DataAccess;    // now, we have Data.Movie

namespace MVCDemo.Repositories
{
    public class MovieRepoDB : IMovieRepo
    {
        private readonly Data.MovieDBContext _db;

        public MovieRepoDB(Data.MovieDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        // ignores cast members
        public void CreateMovie(Movie movie)
        {
            _db.Add(Map(movie));
            _db.SaveChanges();
        }

        public bool DeleteMovie(int id)
        {
            try
            {
                var movie = _db.Movie.Include(m => m.CastMemberJunctions).First(m => m.Id == id);
                _db.Remove(movie);
                foreach (var item in movie.CastMemberJunctions)
                {
                    _db.Remove(item);
                }
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // (adds if ID is set to 0)
        public void EditMovie(Movie movie)
        {
            var mappedMovie = Map(movie);
            _db.Update(mappedMovie);
            _db.SaveChanges();
        }

        public IEnumerable<Movie> GetAll()
        {
            // used to have mapping logic in here
            // (we wound up repeating ourselves until we moved this to another method/class)
            return _db.Movie.Include(m => m.CastMemberJunctions).ThenInclude(j => j.CastMember).Select(Map);
            // deferred execution - no network access / iteration yet
        }

        public IEnumerable<Movie> GetAllByCastMember(string cast)
        {
            return _db.CastMember
                .Include(c => c.MovieJunctions)
                    .ThenInclude(j => j.Movie)  // fills in navigation property OF a navigation property
                        .ThenInclude(m => m.CastMemberJunctions)
                            .ThenInclude(j => j.CastMember)
                .Where(c => c.Name == cast)
                .ToList() // faced issue inside next call with null properties if ToList was not called here
                .SelectMany(c => c.MovieJunctions.Select(j => Map(j.Movie)));
            // SelectMany is a version of Select that produces _multiple_ things from each element,
            //    then flattens the result to one overall list
            // deferred execution - no network access / iteration yet
        }

        public Movie GetById(int id)
        {
            return Map(_db.Movie
                .Include(m => m.CastMemberJunctions)
                    .ThenInclude(j => j.CastMember)
                .First(m => m.Id == id));
        }

        // moving map logic to separate methods or class to prevent repeating myself
        public static Movie Map(Data.Movie data)
        {
            return new Movie
            {
                Id = data.Id,
                Title = data.Title,
                ReleaseDate = data.ReleaseDate,
                Cast = data.CastMemberJunctions.Select(j => j.CastMember.Name).ToList()
            };
        }

        public static Data.Movie Map(Movie ui)
        {
            return new Data.Movie
            {
                Id = ui.Id,
                Title = ui.Title,
                ReleaseDate = ui.ReleaseDate
            };
        }
    }
}
