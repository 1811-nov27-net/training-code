using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCDemo.Controllers;
using MVCDemo.Models;
using MVCDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MVCDemo.Testing.MVC.Controllers
{
    // unit testing with fakes is "correct" but cumbersome.
    // mocks to the rescue
    //public class FakeMovieRepo : IMovieRepo
    //{

    //    public void CreateMovie(Movie movie) => throw new NotImplementedException();
    //    public bool DeleteMovie(int id) => throw new NotImplementedException();
    //    public void EditMovie(Movie movie) => throw new NotImplementedException();
    //    public IEnumerable<Movie> GetAll()
    //    {
    //        return new List<Movie> { new Movie { Id = 1, Title = "Star Wars" } };
    //    }
    //    public IEnumerable<Movie> GetAllByCastMember(string cast) => throw new NotImplementedException();
    //    public Movie GetById(int id) => throw new NotImplementedException();
    //}

    public class MoviesControllerTests
    {
        // tests that when the repository has movies to give,
        // we'll get an index view with a model (ienumerable<movie>) containing those movies
        [Fact]
        public void IndexWithMoviesHasMovies()
        {
            // arrange
            //     var db = new MovieDBContext(); // in-memory
            //     var repo = new MovieRepoDB();
            //     var controller = new MoviesController(repo);
            // at this point we are not really doing a unit test.
            // this is really more like an integration test.
            // to unit test something like MVC, that uses dependency injection,
            //   or really in general, to unit test something with complex (or any) dependencies
            //  we will use mocking (with the Moq framework)

            // if we didn't have a mocking framework, we would use fakes like this.
            //      var fakeRepo = new FakeMovieRepo();
            //      var controller = new MoviesController(fakeRepo);
            // because I used dependency inversion (my controller depends on an interface,
            //  not a concrete class) I am able to provide a different implementation
            // without breaking or changing the controller.

            // dependency injection is what you call it when a framework automatically constructs
            // objects requested (e.g. as constructor parameters) instead of YOUR objects
            // constructing them themselves.

            var data = new List<Movie> { new Movie { Id = 1, Title = "Star Wars" } };

            var mockRepo = new Mock<IMovieRepo>();
            mockRepo
                .Setup(repo => repo.GetAll())
                .Returns(data);
            mockRepo
                //.Setup(repo => repo.GetById(It.IsAny<int>())) // mocking for any parameter
                .Setup(repo => repo.GetById(1))  // mocking for specific parameter
                .Returns(data[0]);
            // you mock the methods that you expect the SUT's code to call.
            // you don't need to mock the other ones unless you're truly blind-testing
            // the implmentation of the controller.

            // it's possible to setup a mock so it verifies that certain methods have been called
            // if you just want a method to be callable without throwing an exception

            mockRepo
                .Setup(repo => repo.CreateMovie(It.IsAny<Movie>()));
            // that method is now callable but won't do anything

            var controller = new MoviesController(mockRepo.Object);

            // act
            // i had to add package reference Microsoft.AspNetCore.Mvc.ViewFeatures to do this
            ActionResult result = controller.Index();

            // assert
            // simple way to cast - throws exception if failure
            //ViewResult viewResult = (ViewResult)result;

            // more self-documenting tests if you assert that the cast succeeds
            // this asserts that the cast succeeds and also returns the casted value.
            ViewResult viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            // model is just "object" type so...
            var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(viewResult.Model);
            var moviesList = movies.ToList();

            // many ways i could check that this list is correct
            Assert.Equal(data.Count, moviesList.Count);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Id, moviesList[i].Id);
            }

            // we've tested that given a repo providing one movie with ID 1,
            // the controller's Index method will return a View having as model,
            // a collection of one movie with ID 1.

            // this is a great test
        }
    }
}
