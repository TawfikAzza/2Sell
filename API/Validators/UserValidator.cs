using API.DTOs;
using FluentValidation;

namespace API.Validators;

public class UserValidator : AbstractValidator<RegisterDTO>
{
    public UserValidator()
    {
        RuleFor(r => r.userName).NotEmpty();
        RuleFor(r => r.Email).NotEmpty()
        .Matches("^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$")
        .WithMessage("Mail format should be respected");
        RuleFor(r => r.Password).NotEmpty();
        RuleFor(r => r.FirstName).NotEmpty();
        RuleFor(r => r.LastName).NotEmpty();
        RuleFor(r => r.PhoneNumber).NotEmpty();
        RuleFor(r => r.Address).NotEmpty();
        RuleFor(r => r.PostalCode).NotEmpty();
   }
}