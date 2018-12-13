using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// namespace alias to get around same-name classes
using Data = MVCDemo.DataAccess;    // now, we have Data.Movie
using MVCDemo.Models;                //  and just Movie for this one
using Microsoft.EntityFrameworkCore;

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

        public void CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMovie(int id) => throw new NotImplementedException();
        public void EditMovie(Movie movie) => throw new NotImplementedException();

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
                .SelectMany(c => c.MovieJunctions.Select(j => Map(j.Movie)));
            // SelectMany is a version of Select that produces _multiple_ things from each element,
            //    then flattens the result to one overall list
            // deferred execution - no network access / iteration yet
        }

        public Movie GetById(int id) => throw new NotImplementedException();

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

        //public static Data.Movie Map(Movie ui)
        //{
        //    return new Data.Movie
        //    {
        //        Id = ui.Id,
        //        Title = ui.Title,
        //        ReleaseDate = ui.ReleaseDate,
        //        CastMembers = ui.Cast.Select(str => _db.CastMember.FirstOrDefault(c => c.Name))
        //    };
        //}
    }
}
