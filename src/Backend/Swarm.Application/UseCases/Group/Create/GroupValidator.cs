using FluentValidation;
using Swarm.Communication.Requests;

namespace Swarm.Application.UseCases.Group.Create;

public class GroupValidator : AbstractValidator<RequestGroupJson>
{
    public GroupValidator()
    {
        RuleFor(group => group.Name)
            .NotEmpty().WithMessage("The group name is required.")
            .MinimumLength(3).WithMessage("The minimum name length is 3 characters.")
            .MaximumLength(15).WithMessage("The maximum name length is 15 characters.")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("The group name can only contain letters, numbers, and spaces.");
    }
}