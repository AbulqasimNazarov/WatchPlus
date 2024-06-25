using FluentValidation;
using WatchPlus.Models;

namespace WatchPlus.Validators;

public class FilmValidator : AbstractValidator<Film>
{
    public FilmValidator()
    {
        

        RuleFor(film => film.Name)
            .NotEmpty()
            .Length(5, 30);

        RuleFor(film => film.Presentation)
            .NotEmpty()
            .Length(30, 1500);

        RuleFor(film => film.Category)
            .NotEmpty()
            .NotNull();

        RuleFor(film => film.TrailerVideo)
            .NotEmpty();

        RuleFor(film => film.Star)
            .NotEmpty();

        RuleFor(film => film.PresentationDate)
            .NotEmpty()
            .WithMessage("Presentation Date must not be empty");
    }
}

