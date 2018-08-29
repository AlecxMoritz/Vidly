using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MustBeOverTen : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;

            if(movie.InStock <= 0)
            {
                return new ValidationResult("Stock of movies must be greater than ten.");
            }

            if(movie.InStock > 10)
            {
                return ValidationResult.Success;
            }
            else return new ValidationResult("Stock of movies must be greater than ten.");
        }
    }
}