using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.WebAPI
{
    public class RentalController : ApiController
    {
        ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();

        }


        [HttpPost]  
        public IHttpActionResult CreateNewRental(NewRentalDTO newRental)
        {
            var customer = _context.Customers.SingleOrDefault
                    (c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where
                    (m => newRental.MovieIds.Contains(m.Id));

            foreach(var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest();

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }


        // return rental
        [HttpPost]
        public IHttpActionResult ReturnRental(ReturnDTO returnModel)
        {
            var customer = _context.Customers.SingleOrDefault
                   (c => c.Id == returnModel.CustomerId);

            var movies = _context.Movies.Where
                    (m => returnModel.MovieIds.Contains(m.Id));

            foreach(var item in movies)
            {
                var rental = _context.Rentals.Single(r => r.Customer.Id == customer.Id && r.Movie.Id == item.Id);
                rental.DateReturned = DateTime.Now;
                //item.NumberAvailable++;
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
