using FluentValidation;
using WatchPlus.Models;

namespace WatchPlus.Validators;

public class FilmValidator : AbstractValidator<Film>
{
    public FilmValidator()
    {
            

        base.RuleFor(film => film.Name)
                          .NotEmpty()
                          .Length(5, 30);
 
        base.RuleFor(film => film.Presentation)
        .NotEmpty()
        .Length(30, 1500);

        base.RuleFor(film => film.Category)
        .NotEmpty()
        .NotNull();

        base.RuleFor(film => film.TrailerVideo)
        .NotEmpty();

        base.RuleFor(film => film.Star)
        .NotEmpty();


        base.RuleFor(film => film.presentationDate)
        .NotEmpty();


    }
}