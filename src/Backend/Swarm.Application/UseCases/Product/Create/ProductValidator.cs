using FluentValidation;
using Swarm.Communication.Requests;
using Swarm.Exceptions;

namespace Swarm.Application.UseCases.Product.Create;

public class ProductValidator : AbstractValidator<RequestProductJson>
{
    public ProductValidator()
    {
        RuleFor(product => product.InternalCode)
           .GreaterThan(0)
           .WithMessage(ResourceMessagesExceptions.INTERNAL_CODE_EMPTY);

        RuleFor(product => product.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(35)
            .WithMessage(ResourceMessagesExceptions.NAME_MAX_35_MIN_1);

        RuleFor(product => product.Value)
            .GreaterThan(0)
            .WithMessage("The value must be greater than 0.");

        RuleFor(product => product.UnitType)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.UNIT_TYPE_NOT_SUPPORTED);
    }
}
