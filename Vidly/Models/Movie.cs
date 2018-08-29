using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Movie must have title")]
        public string Name { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(10d, 35d, ErrorMessage = "Number in stock needs to be between 10 and 35.")]
        public int InStock { get; set; }
        public int NumberAvailable { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
        public GenreTypes MovieGenre { get; set; }

        [Display(Name = "Movie Genre")]
        [Required(ErrorMessage = "Movie must have a genre")]
        public int MovieGenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Movie must have a release date")]
        public DateTime? ReleaseDate { get; set; }

    }
}