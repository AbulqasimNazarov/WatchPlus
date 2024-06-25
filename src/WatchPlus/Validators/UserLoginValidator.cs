using FluentValidation;
using WatchPlus.Dtos;
using WatchPlus.Models;

namespace WatchPlus.Validators;

public class UserRegistrationValidator : AbstractValidator<User>
{
    public UserRegistrationValidator()
    {


        base.RuleFor(u => u.Email)
                        .NotEmpty()
                        .EmailAddress();

        base.RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Za-z]").WithMessage("Password must contain letter.")
            .Matches(@"\d").WithMessage("Password must contain number.");


        base.RuleFor(u => u.Name)
            .NotEmpty()
            .Matches("^[A-Z]").WithMessage("The Name must begin with a capital letter.");

        base.RuleFor(u => u.Surname)
            .NotEmpty()
            .Matches("^[A-Z]").WithMessage("The Surname must begin with a capital letter.");


    }
}