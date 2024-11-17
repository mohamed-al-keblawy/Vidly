using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }
        // Get: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Sherk!" };
            return View(movie);
        }
    }
}