using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;
using MVCDemo.Repositories;

namespace MVCDemo.Controllers
{
    public class MoviesController : Controller
    {
        // we get a new Controller constructed for every request

        // making this static is the quickest way for demo
        // to get data persisted across requests
        //public static MovieRepo Repo { get; set; } = new MovieRepo();

        public IMovieRepo Repo { get; set; }

        // the parameters of this constructor will be injected automatically
        // based on what is set up in Startup.ConfigureServices.
        public MoviesController(IMovieRepo repo)
        {
            Repo = repo;
        }

        // GET: Movies
        // show a table of all the movies
        public ActionResult Index()
        {
            // "View()" is a method on the base Controller class
            // which looks for a view with the same name as the current method
            // and constructs it with the given parameter if any.
            return View(Repo.GetAll());
        }

        // GET: Movies/Details/5
        // action methods get their parameters from
        //   route parameters, query string, request body
        public ActionResult Details(int id)
        {
            var movie = Repo.GetById(id);
            return View(movie);
        }

        // GET: Movies/Create
        // for the client accessing the Create page
        public ActionResult Create()
        {
            return View(); // strongly typed to Movie
            // but we didn't pass it anything.
            // so all the corresponding fields will be empty/default.
        }

        // POST: Movies/Create
        // for the client submitting the form on the Create page
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        public ActionResult Create(Movie newMovie)
        {
            // formcollection is a loosely typed way to get form data back.
            // we can make it strongly-typed with model binding.

            // if we take a class parameter like that, ASP.NET will try to fill in
            // the fields of that object based on the request body, which is where
            // the form data is located.
            try
            {
                // any time you do model binding, check ModelState.IsValid
                // to see if there were any server-side validation errors.
                if (ModelState.IsValid)
                {
                    Repo.CreateMovie(newMovie);
                }
                else
                {
                    // get a new Create page, but with the current ModelState errors.
                    return View();
                }

                // nameof operator is just the string of whatever you give it
                // nameof(Index) == "Index"
                //     except it'll be a compile error if the name is wrong
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                // add server-side validation error message
                ModelState.AddModelError("Id", ex.Message);
                // get a new Create page, but with the current ModelState errors.
                return View();
            }
            catch
            {
                // if we get any exception, go back to Create view
                // (ideally we would provide a useful error message when the error is not in ModelState)
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = Repo.GetById(id);
            if (movie != null)
            {
                return View(movie);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // id is coming via route parameter
        // movie is coming via request body
        public ActionResult Edit(int id, Movie movie)
        {
            try
            {
                // server-side validation
                if (id != movie.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    Repo.EditMovie(movie);
                }
                else
                {
                    // get a new Edit page, but with the current ModelState errors.
                    return View();
                }

                // redirecttoaction and view are two different ways to get to a view

                // this one would not change the URL displayed to "index", it would leave it
                // at "edit", which would be confusing for the user.
                //return View(nameof(Index));

                // this tells the browser to make a new request for the index view.
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = Repo.GetById(id);
            if (movie != null)
            {
                return View(movie);
            }
            // if id not found...
            // ideally show an error message somehow
            // but i'll just redirect to movies index.
            return RedirectToAction(nameof(Index));
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // we don't actually use that formcollection, it's really just to distinguish
        // this method from the previous one for the C# compiler. (for ASP.NET, the HttpPost attr does that)
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // server-side checks for things like this
                // never assume only one client is working with your app at a time
                // (another browser might have deleted the record since we loaded the page)
                var success = Repo.DeleteMovie(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}