using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Repositories;

namespace MVCDemo.Controllers
{
    public class CastController : Controller
    {
        public IMovieRepo Repo { get; set; }

        // (dependency injection, based on Startup.ConfigureServices)
        public CastController(IMovieRepo repo)
        {
            Repo = repo;
        }

        // we have two basic types of routing in ASP.NET

        // routing is how asp.net decides, based on the URL (and the HTTP method)
        // which controller to construct and which action method to call.

        // (1) convention routing - defined globally in Startup.cs

        // route parameter "name" defined in my route will wind up here.
        public IActionResult Index(string name)
        {
            var movies = Repo.GetAllByCastMember(name).ToList();
            // a ToList here enables me to debug the LINQ stuff since
            // the iteration happens in my code and not the View rendering code.
            return View(movies);
        }
    }
}