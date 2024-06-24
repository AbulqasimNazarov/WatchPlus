using FluentValidation;
using WatchPlus.Models;

namespace WatchPlus.Validators;

public class TvShowValidator : AbstractValidator<TvSHow>
{
    public TvShowValidator()
    {
        

        base.RuleFor(tvShow => tvShow.Name)
                          .NotEmpty()
                          .Length(5, 30);
 
        base.RuleFor(tvShow => tvShow.Presentation)
        .NotEmpty()
        .Length(30, 1500);

        base.RuleFor(tvShow => tvShow.Category)
        .NotEmpty()
        .NotNull();

        base.RuleFor(tvShow => tvShow.TrailerVideo)
        .NotEmpty();

        base.RuleFor(tvShow => tvShow.presentationDate)
        .NotEmpty();

        base.RuleFor(tvShow => tvShow.Star)
        .NotEmpty();

    }
}