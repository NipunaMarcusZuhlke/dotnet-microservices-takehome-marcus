using FluentValidation;
using OrderProcessor.OrderService.Application.Dtos;

namespace OrderProcessor.OrderService.Application.Validators;

public class CreateOrderRequestDtoValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestDtoValidator()
    {
        RuleFor(createOrderDto => createOrderDto.CustomerEmail).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid Customer Email");
        RuleFor(createOrderDto => createOrderDto.Amount).NotNull().NotEmpty().WithMessage("Amount need to be specified");
    }
}