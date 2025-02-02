using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;
        public MoviesController()
        {
                _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
        //GET: Movies/Index
        public ActionResult Index()
        {
            var movies = _dbContext.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }
        public ActionResult New()
        {
            var genres = _dbContext.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm" ,viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _dbContext.Movies.Include(c => c.Genre).FirstOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        public ActionResult Edit(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Genres = _dbContext.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _dbContext.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie {Id = 1 , Name = "Sherk" },
                new Movie { Id = 2 , Name = "Wall-e"
            }
        };
        }

        // Get: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Sherk!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer {Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        // GET: Movies/Edit/1
        //public ActionResult Edit(int id)
        //{
        //    return Content("Id = " + id);
        //}

        // GET: Movies/Index?pageIndex=1,sortBy='Name'
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (string.IsNullOrEmpty(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("Page Index = {0} & sortBy = {1}", pageIndex, sortBy));
        //}

        // Get: Movies/released/2015/01
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(String.Format("Year = {0} , Month = {1}", year, month));
        }
    }
}