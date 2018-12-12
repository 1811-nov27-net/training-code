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
        public static MovieRepo Repo { get; set; } = new MovieRepo();

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
            return View();
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

                // nameof operator is just the string of whatever you give it
                // nameof(Index) == "Index"
                //     except it'll be a compile error if the name is wrong
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                // add server-side validation error message
                ModelState.AddModelError("Id", ex.Message);
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
            return View();
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}