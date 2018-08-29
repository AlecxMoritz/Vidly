using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.WebAPI
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // get all
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies
                .Include(m => m.MovieGenre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        // get by id
        public IHttpActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // add
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult Add(MovieDto movieDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //update
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public HttpStatusCode UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Movie movieInDb = _context.Movies.Single(m => m.Id == id);

            if(movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();

            return HttpStatusCode.OK;

        }

        // delete
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public HttpStatusCode DeleteMovie(int id)
        {
            Movie movieInDb = _context.Movies.Single(m => m.Id == id);

            if(movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }
    }
}
