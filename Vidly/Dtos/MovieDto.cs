using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie must have title")]
        public string Name { get; set; }

        [Range(10d, 35d, ErrorMessage = "Number in stock needs to be between 10 and 35.")]
        public int InStock { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Movie must have a genre")]
        public int MovieGenreId { get; set; }
        public GenreTypeDto MovieGenre { get; set; }

        [Required(ErrorMessage = "Movie must have a release date")]
        public DateTime? ReleaseDate { get; set; }
    }
}