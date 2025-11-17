using OrderProcessor.OrderService.Application.Dtos;
using OrderProcessor.OrderService.Application.Validators;
using Shouldly;

namespace OrderProcessor.UnitTest.OrderService.Application.Validators;

public class CreateOrderRequestDtoValidatorTest
{
    private readonly CreateOrderRequestDtoValidator _validator = new();

    [Fact]
    public void GivenCreateOrderRequestDto_WhenValuesAreCorrect_ReturnValid()
    {
        var createOrderRequestDto = new CreateOrderRequestDto((decimal)49.09, "sample@gmail.com");

        var validationResult = _validator.Validate(createOrderRequestDto);
        
        validationResult.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void GivenCreateOrderRequestDto_WhenEmailIsIncorrect_ReturnInValid()
    {
        var createOrderRequestDto = new CreateOrderRequestDto((decimal)49.09, "gmail.com");

        var validationResult = _validator.Validate(createOrderRequestDto);
        
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(1);
        validationResult.Errors[0].ErrorMessage.ShouldBe("Invalid Customer Email");
    }
    
    [Fact]
    public void GivenCreateOrderRequestDto_WhenAmountIsCorrect_ReturnInValid()
    {
        var createOrderRequestDto = new CreateOrderRequestDto(0, "sample@gmail.com");

        var validationResult = _validator.Validate(createOrderRequestDto);
        
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(1);
        validationResult.Errors[0].ErrorMessage.ShouldBe("Amount need to be specified");
    }
}