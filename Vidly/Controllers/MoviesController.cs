using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "The Breakfast Club" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Alecx" },
                new Customer { Name = "Tay" },
                new Customer { Name = "Ed" },
                new Customer { Name = "Michelle" },
                new Customer { Name = "Coop" },
                new Customer { Name = "Sarah"}
            };

            var viewModel = new RandomMoviewViewModel
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);
        }



        //when you are directed to movies
        // to make a value nullable attach the ? opertor after the value type
            // notice we dont have to do that for string, because in C#, a string
            // is a reference type and is nullable
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.MovieGenre).ToList().SingleOrDefault(m => m.Id == id);

            MovieDetailViewModel viewModel = new MovieDetailViewModel
            {
                Movie = movie,
                GenreType = movie.MovieGenre
            };

            return View(viewModel);
        }

        // save

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    GenreTypes = _context.GenreTypes.ToList()
                };

                return View("MovieForm", viewModel);
            }


            if(movie.Id == 0)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("Index", "Movies");
            }
            else
            {
                Movie movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.MovieGenreId = movie.MovieGenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.InStock = movie.InStock;
                _context.SaveChanges();

                return RedirectToAction("Index", "Movies");
            }
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                GenreTypes = _context.GenreTypes.ToList()
            };


            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.Single(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }

            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Movie = movie,
                GenreTypes = _context.GenreTypes.ToList()
            };


            return View("MovieForm", viewModel);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }      
    }
}