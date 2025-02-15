using FluentValidation;
using Swarm.Application.UseCases.Product.Create;
using Swarm.Communication.Requests;

namespace Swarm.Application.UseCases.Group.Create;

public class GroupValidator : AbstractValidator<RequestGroupJson>
{
    public GroupValidator()
    {
        RuleFor(group => group.Name)
            .NotEmpty()
            .MaximumLength(15)
            .WithMessage("The maximum name length is 15 characters.");

        RuleFor(group => group.Products)
            .Must(products => products.Count == 0 || products.Select(p => p.InternalCode).Distinct().Count() == products.Count)
            .WithMessage("Each product must have a unique internal code.");

        RuleForEach(group => group.Products)
            .SetValidator(new ProductValidator());
    }
}