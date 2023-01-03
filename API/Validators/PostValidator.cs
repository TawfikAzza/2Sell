using API.DTOs;
using FluentValidation;

namespace API.Validators;

public class PostValidator : AbstractValidator<PostDTO>
{
    public PostValidator()
    {
        RuleFor(p => p.Address).NotEmpty();
        RuleFor(p => p.Authority).LessThan(3);
        RuleFor(p => p.Authority).GreaterThan(-1);
        RuleFor(p => p.Category).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Email).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.UserName).NotEmpty();
    }
}

public class CreatePostValidator : AbstractValidator<CreatePostDTO>
{
    public CreatePostValidator()
    {
        RuleFor(c => c.Category).GreaterThan(0);
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Price).GreaterThan(0);
        RuleFor(c => c.Title).NotEmpty();
    }
}