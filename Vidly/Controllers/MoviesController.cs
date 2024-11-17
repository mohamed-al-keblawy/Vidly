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

        // GET: Movies/Edit/1
        public ActionResult Edit(int id)
        {
            return Content("Id = " + id);
        }

        // GET: Movies/Index?pageIndex=1,sortBy='Name'
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrEmpty(sortBy))
                sortBy = "Name";

            return Content(String.Format("Page Index = {0} & sortBy = {1}", pageIndex, sortBy));
        }

        // Get: Movies/released/2015/01
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(String.Format("Year = {0} , Month = {1}", year, month));
        }
    }
}